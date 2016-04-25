using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

    public int scrollDist;
    public float scrollSpeed;
    private Input mouse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        var mouse_x = Input.mousePosition.x;
        var mouse_y = Input.mousePosition.y;

        if (mouse_x < scrollDist)
        {
            transform.Translate(-.33f * scrollSpeed, 0, .67f*scrollSpeed, Space.World);
        }
        else if (mouse_x> Screen.width - scrollDist)
        {
            transform.Translate(.33f * scrollSpeed, 0, -.67f*scrollSpeed, Space.World);
        }

        if (mouse_y < scrollDist)
        {
            
            transform.Translate(-.67f *scrollSpeed, 0, -.33f * scrollSpeed, Space.World);
        }
        else if (mouse_y > Screen.height - scrollDist)
        {
            
            transform.Translate(.67f *scrollSpeed, 0, .33f * scrollSpeed, Space.World);
        }
    }
}
