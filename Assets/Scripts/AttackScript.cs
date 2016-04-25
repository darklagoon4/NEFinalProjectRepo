using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool checkAtk(GameObject atker, GameObject target, float targetRange)
    {
        if (Vector3.Distance(atker.transform.position, target.transform.position)>targetRange)
        {
            return true;
        }
        return false;
    }

    public void attack(WorldObjectScript atker, WorldObjectScript target, int damage)
    {
        target.health = target.health - damage;
        if (target.health <= 0)
        {

        }
    }
}
