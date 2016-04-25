using UnityEngine;
using System.Collections;

[System.Serializable]
public class modelColor
{
    public float headRed, headGreen, headBlue, headAlpha;
}
public class CharScript : MonoBehaviour {

    public modelColor modelColor;

    public bool selected;
    public float speed;
    RaycastHit hit;

    public Renderer rend;
    public Animator anim;
    int movingHash=Animator.StringToHash("isMoving");

    
	// Use this for initialization
	void Start () {
        //rend = GetComponent<Renderer>();
        //anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Could not find char animator");
        }
        hit = new RaycastHit();
        hit.point = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButton(1) && selected != false)
        {
            Vector3 currPos = transform.position;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = new RaycastHit();
            LayerMask layerMask = 1 << 8;
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                Vector3 direction = (currPos - hit.point).normalized;
                //var lookAtDir = new Vector3(hit.point.x, 0.0f, hit.point.z);
                //transform.LookAt(lookAtDir);
                transform.LookAt(hit.point);
                
                Debug.Log(direction);
            
            }
            else
            {
                Debug.Log("No hit");
                hit.point = currPos;
            }


        }

        if (Vector3.Distance(transform.position, hit.point) > 1)
        {
            if(anim!=null)
                anim.SetBool(movingHash, true);
            transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
        }
        else
        {
            if (anim != null)
                anim.SetBool(movingHash, false);
        }
        
    }

    public void setSelected()
    { 
        selected = true;
        rend.material.color = Color.blue;
    }

    public void unsetSelected()
    {
        selected = false;
        rend.material.color = colorConvert(modelColor.headRed, modelColor.headGreen, modelColor.headBlue, modelColor.headAlpha);
    }

    public Vector4 colorConvert(float red, float green, float blue, float alpha)
    {
        Vector4 color=new Vector4(red / 255.0f, green / 255.0f, blue / 255.0f, alpha/255.0f);
        return color;
    }
}
