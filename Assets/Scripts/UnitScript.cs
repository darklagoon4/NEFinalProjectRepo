using UnityEngine;
using System.Collections;



public class UnitScript : WorldObjectScript
{
    

    public Transform targetPos;
    public float speed;
    Vector3[] path;
    int targetIndex;
    public float range;
    public int weaponAtk;
    

    public Renderer rend;
    public Animator anim;
    int movingHash = Animator.StringToHash("isMoving");
    int firingHash = Animator.StringToHash("isFiring");

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        //PathRequestManagerScript.requestPath(transform.position, target.position,onPathFound);

        if (anim == null)
        {
            Debug.Log("Could not find char animator");
        }

    }

    protected override void Update()
    {
        base.Update();
        if (movingToPos && targetInRange())
        {
            StopCoroutine("FollowPath");
            if (anim != null)
            {
                anim.SetBool(movingHash, false);
                moving = false;
                movingToPos = false;
            }
            targetIndex = 0;
            path = new Vector3[0];
        }
        if (attacking && !movingToPos)
        {

            if (!targetInRange() && canMove())
            {
                if (anim != null)
                {             
                    anim.SetBool(firingHash, false);

                }
                movingToPos = true;
                startAttack(targetObj);
            }
            else {
                if (anim != null)
                {
                    if (!anim.GetBool(firingHash))
                    {
                        anim.SetBool(firingHash, true);
                    }
                }
            }

        }
        if(targetObj == null)
        {
            if (anim != null)
            {
                if (anim.GetBool(firingHash))
                {
                    anim.SetBool(firingHash, false);
                }
            }
        }
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    public override void setSelected(bool selected)
    {
        this.selected = selected;
        if (selected)
        {
            rend.material.color = Color.blue;
        }
        else
        {
            rend.material.color = colorConvert(modelColor.headRed, modelColor.headGreen, modelColor.headBlue, modelColor.headAlpha);
        }
    }

    public override void rightClick(GameObject hitObj, Vector3 targetVector, PlayerScript control)
    {
        bool targetable=false;
        
        if (hitObj.transform.childCount>0)
        {
            Debug.Log("Found child "+hitObj.transform.GetChild(0).gameObject.name);
            if (hitObj.transform.GetChild(0).gameObject.layer == LayerMask.NameToLayer("Targetable"))
            {
                targetable = true;
            }
        }
        if (hitObj.layer == LayerMask.NameToLayer("Targetable") || targetable)
        {
            WorldObjectScript worldObj = hitObj.GetComponent<WorldObjectScript>();
            if (worldObj != null)
            {
                Debug.Log("Starting Attack");
                startAttack(worldObj);
            }
            else
            {
                Debug.Log("Invalid target: Unit script "+targetable);
            }
        }
        else {
            attacking = false;
            if (anim != null)
            {
                anim.SetBool(firingHash, false);
            }
            makeMove(targetVector);
        }
    }

    public void onPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            path = newPath;
            for(int i = 0; i < path.Length; i++) {
                Debug.Log(path[i]);
            }
            
            StopCoroutine("FollowPath");
            targetIndex = 0;
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWayPoint = path[0];
        while (true)
        {
            if (anim != null)
            {
                anim.SetBool(movingHash, true);
                moving = true;
            }
            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    if (anim != null)
                    {
                        anim.SetBool(movingHash, false);
                        moving = false;
                        movingToPos = false;
                    }
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed);
            transform.LookAt(currentWayPoint);
            if (anim != null)
                //anim.SetBool(movingHash, false);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i= targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    public void makeMove(Vector3 targetVector)
    {
        Debug.Log(transform.position + " : " + targetVector);
        PathRequestManagerScript.requestPath(transform.position, targetVector, onPathFound);
    }

    protected override void fireWeapon()
    {
        transform.LookAt(targetObj.transform.position);
        currWeaponCooldown = 0.0f;
        currFiringTime = 0.0f;
        //this method needs to be overloaded
        Debug.Log("Firing, Target health: " + targetObj.health);
        var audio = GetComponent<AudioSource>();
        audio.Play();
        targetObj.health = targetObj.health - weaponAtk;
    }


}
