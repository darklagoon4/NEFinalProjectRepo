using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class PlayerScript : MonoBehaviour {

    public string playerName;
    public bool human;

    public int startOil, startOilLimit, startInfluence, startInfluenceLimit;
    private Dictionary<ResourceType, int> resource, resourceLimit;

    public HUDScript hud;
    public WorldObjectScript selectedObj
    {
        get; set;
    }


    void Awake()
    {
        resource = initResourceList();
        resourceLimit = initResourceList();
    }
    // Use this for initialization
    void Start () {
        hud = GetComponentInChildren<HUDScript>();
        addStartingResourceLimit();
        addStartingResource();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private Dictionary<ResourceType,int> initResourceList()
    {
        Dictionary<ResourceType, int> dict = new Dictionary<ResourceType, int>();
        dict.Add(ResourceType.Oil, 0);
        dict.Add(ResourceType.Influence, 0);
        return dict;
    }

    private void addStartingResourceLimit()
    {
        increaseResourceLimit(ResourceType.Influence, startInfluenceLimit);
        increaseResourceLimit(ResourceType.Oil, startOilLimit);
    }

    private void addStartingResource()
    {
        increaseResource(ResourceType.Influence, startInfluence);
        increaseResource(ResourceType.Oil, startOil);

    }

    private void increaseResource(ResourceType type, int amount)
    {
        resource[type] += amount;
    }

    private void increaseResourceLimit(ResourceType type, int amount)
    {
        resourceLimit[type] += amount;
    }

    public void addUnit(string unitName, Vector3 spawnLoc, Quaternion rotation)
    {
        Debug.Log("Add unit to player");
    }


}
