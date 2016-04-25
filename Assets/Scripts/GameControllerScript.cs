using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    public GameObject spawn;
	// Use this for initialization
	void Start () {
        Instantiate(spawn, new Vector3(170f, 2.5f, -30f), Quaternion.identity);
        Instantiate(spawn, new Vector3(30f, 2.5f, 30f), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
