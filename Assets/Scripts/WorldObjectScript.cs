using UnityEngine;
using System.Collections;
using RTS;

public class WorldObjectScript : MonoBehaviour {

    public string objName;
    public Texture2D image;
    public int cost, health, maxHealth;
    public float atkRange;

    public modelColor modelColor;

    protected PlayerScript player;
    protected string[] actionArr;
    protected bool selected=false;
    protected Bounds selectedBounds;
    protected WorldObjectScript targetObj = null;
    protected bool attacking = false;
    protected bool moving = false;
    protected bool movingToPos = false;
    protected float currFiringTime = 0.0f;
    public float weaponCooldown = 1.0f;
    protected float currWeaponCooldown;
    public float firingTime = .5f;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        player = transform.root.GetComponentInChildren<PlayerScript>();
    }

    protected virtual void Update()
    {
        currWeaponCooldown += Time.deltaTime;
        firingTime += Time.deltaTime;
        selectedBounds = ResourceManagerScript.invalidBounds;
        calcBounds();
        if (targetObj == null && attacking == true)
        {

        }
        if (attacking && !movingToPos)
        {
            doAttack();
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        /*if (selected)
        {
            Debug.Log("Moving into pos: " + movingToPos);
        }*/
    }

    protected virtual void OnGUI()
    {

    }

    public virtual void setSelected(bool selected)
    {
        this.selected = selected;
    }

    public string[] getActions()
    {
        return actionArr;
    }

    public virtual void doAction(string action)
    {

    }

    public virtual void mouseClick(GameObject hitObj, Vector3 hitPoint, PlayerScript control)
    {
        if(selected && hitObj &&hitObj.name != "Ground")
        {
            WorldObjectScript worldObj = hitObj.transform.root.GetComponent<WorldObjectScript>();
            if (worldObj)
            {
                changeSelect(worldObj, control);
            }
        }
    }

    public virtual void rightClick(GameObject hitObj, Vector3 hitpoint, PlayerScript control)
    {

    }

    private void changeSelect(WorldObjectScript worldObj, PlayerScript control)
    {
        setSelected(false);
        if (control.selectedObj)
        {
            control.selectedObj.setSelected(false);
        }
        control.selectedObj = worldObj;
        worldObj.setSelected(true);
    }

    public Vector4 colorConvert(float red, float green, float blue, float alpha)
    {
        Vector4 color = new Vector4(red / 255.0f, green / 255.0f, blue / 255.0f, alpha / 255.0f);
        return color;
    }

    public void calcBounds()
    {
        selectedBounds = new Bounds(transform.position, Vector3.zero);
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            selectedBounds.Encapsulate(r.bounds);
        }
    }

    protected virtual void startAttack(WorldObjectScript targetObj)
    {
        this.targetObj = targetObj;
        //Debug.Log("Target name: " + targetObj.name);
        if (targetInRange())
        {
            Debug.Log("DoAttack");
            attacking = true;
            doAttack();
        }
        else {
            //Debug.Log("adjPos");
            //attacking = true;
            adjPosition();
        }
    }

    protected bool targetInRange()
    {
        Vector3 targetLoc = targetObj.transform.position;
        //Vector3 direction = targetLoc - transform.position;
        //Debug.Log(direction.sqrMagnitude);
        if (Vector3.Distance(targetLoc,transform.position) < atkRange)
        {
            return true;
        }
        return false;
    }

    private void adjPosition()
    {
        UnitScript self = this as UnitScript;
        if (self)
        {
            movingToPos = true;
            Vector3 atkPosition = findClosestAtkPosition();
            self.makeMove(atkPosition);
            attacking = true;
            //Debug.Log("moving");
        }
        else {
            attacking = false;
        }
    }


    //BUG: If closestAtkPosition is an invalid position, the unit will not move closer
    private Vector3 findClosestAtkPosition()
    {
        Vector3 targetLoc = targetObj.transform.position;
        Vector3 direction = targetLoc - transform.position;
        float targetDist = direction.magnitude;
        float distToTravel = targetDist - (0.9f * atkRange);
        return Vector3.Lerp(transform.position, targetLoc, distToTravel / targetDist);
    }

    private void doAttack()
    {
        //Debug.Log("DoAttack");
        if (!targetObj)
        {
            attacking = false;
            return;
        }
        if (!targetInRange())
        {
            adjPosition();
            //Debug.Log("Adjusting pos");
        }
        else if (canFire())
        {
            fireWeapon();
        }
    }

    private bool canFire()
    {
        if (currWeaponCooldown >= weaponCooldown)
        {
            return true;
        }
        return false;
    }

    protected virtual void fireWeapon()
    {
        currWeaponCooldown = 0.0f;
        //this method needs to be overloaded
        Debug.Log("Fired weapon");
    }

    protected bool canMove()
    {
        if (currFiringTime >= firingTime)
        {
            return true;
        }
        return false;
    }

}
