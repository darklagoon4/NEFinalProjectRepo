using UnityEngine;
using System.Collections;

public class SelectionScript : MonoBehaviour {

    RaycastHit hit;
    GameObject selected;
	// Use this for initialization
	void Start () {
        selected = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (selected != null)
                {
                    selected.GetComponent<CharScript>().unsetSelected();
                }
                selected = hit.transform.gameObject;

                if (selected.CompareTag("Unit"))
                {
                    selected.GetComponent<CharScript>().setSelected();
                }
                else
                {
                    //Debug.Log("Not a unit");
                    selected = null;
                }
            }
            
            
        }

    }
}
