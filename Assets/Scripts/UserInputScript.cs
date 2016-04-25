using UnityEngine;
using System.Collections;
using RTS;

public class UserInputScript : MonoBehaviour {

    private PlayerScript player;
	// Use this for initialization
	void Start () {
        player = transform.root.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(player && player.human)
        {
            moveCamera();
            mouseInput();
        }
	}

    private void moveCamera()
    {
        var mouse_x = Input.mousePosition.x;
        var mouse_y = Input.mousePosition.y;
        var cam = Camera.main.transform;

        if (mouse_x < ResourceManagerScript.scrollDist)
        {
            cam.Translate(-.33f * ResourceManagerScript.scrollSpeed, 0, .67f * ResourceManagerScript.scrollSpeed, Space.World);
        }
        else if (mouse_x > Screen.width - ResourceManagerScript.scrollDist)
        {
            cam.Translate(.33f * ResourceManagerScript.scrollSpeed, 0, -.67f * ResourceManagerScript.scrollSpeed, Space.World);
        }

        if (mouse_y < ResourceManagerScript.scrollDist)
        {

            cam.Translate(-.67f * ResourceManagerScript.scrollSpeed, 0, -.33f * ResourceManagerScript.scrollSpeed, Space.World);
        }
        else if (mouse_y > Screen.height - ResourceManagerScript.scrollDist)
        {

            cam.Translate(.67f * ResourceManagerScript.scrollSpeed, 0, .33f * ResourceManagerScript.scrollSpeed, Space.World);
        }
    }

    private void mouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseClicked();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rightMouseClicked();
        }
    }

    private void leftMouseClicked()
    {
        
        if (player.hud.mouseInBounds())
        {
            //Debug.Log("hi");
            GameObject hitObj = findHitObj();
            Vector3 hitPoint = findHitPoint();
            if(hitObj && hitPoint != ResourceManagerScript.invalidPosition)
            {
                if (player.selectedObj)
                {
                    player.selectedObj.mouseClick(hitObj, hitPoint, player);
                    
                }
                else if (hitObj.name != "Ground")
                {
                    WorldObjectScript worldObj = hitObj.transform.root.GetComponent<WorldObjectScript>();
                    if (worldObj)
                    {
                        player.selectedObj = worldObj;
                        worldObj.setSelected(true);
                    }
                }
            }
        }
    }

    private void rightMouseClicked()
    {
        GameObject hitObj = findHitObj();
        Vector3 hitPoint = findHitPoint();
        if (player.hud.mouseInBounds() && player.selectedObj)
        {
            //player.selectedObj.setSelected(false);
            //player.selectedObj = null;
            
            player.selectedObj.rightClick(hitObj,hitPoint,player);
        }
    }

    private GameObject findHitObj()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(r,out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private Vector3 findHitPoint()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            return hit.point;
        }
        return ResourceManagerScript.invalidPosition;
    }
}
