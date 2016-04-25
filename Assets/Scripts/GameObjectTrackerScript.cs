using UnityEngine;
using System.Collections;
using RTS;

public class GameObjectTrackerScript : MonoBehaviour {

    public GameObject[] buildings;
    public GameObject[] units;
    public GameObject[] worldObjs;
    public GameObject player;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            ResourceManagerScript.setGameObjectTracker(this);
            created = true;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject getUnit(string name)
    {
        for (int i = 0; i < units.Length; i++)
        {
            UnitScript unit = units[i].GetComponent<UnitScript>();
            if (unit && unit.name == name) return units[i];
        }
        return null;
    }

    public GameObject getBuilding(string name)
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            BuildingScript building = buildings[i].GetComponent<BuildingScript>();
            if (building && building.name == name) return buildings[i];
        }
        return null;
    }

    public GameObject getWorldObj(string name)
    {
        for (int i = 0; i < worldObjs.Length; i++)
        {
            WorldObjectScript worldObj = worldObjs[i].GetComponent<WorldObjectScript>();
            if (worldObj && worldObj.name == name) return worldObjs[i];
        }
        return null;
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public Texture2D getImage(string name)
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            BuildingScript building = buildings[i].GetComponent<BuildingScript>();
            if (building && building.name == name) return building.image;
        }
        for (int i = 0; i < units.Length; i++)
        {
            UnitScript unit = units[i].GetComponent<UnitScript>();
            if (unit && unit.name == name) return unit.image;
        }
        return null;
    }
}
