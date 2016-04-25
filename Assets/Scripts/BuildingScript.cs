using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class BuildingScript : WorldObjectScript {

    public Renderer rend;
    public float maxBuildTime;
    protected Queue<string> buildLine;
    private float currBuildTime = 0.0f;
    private Vector3 spawnLoc;

    protected override void Awake()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        buildLine = new Queue<string>();
        //Need to add real spawn point
        float spawnX = selectedBounds.center.x + transform.forward.x * selectedBounds.extents.x + transform.forward.x * 10;
        float spawnZ = selectedBounds.center.z + transform.forward.z + selectedBounds.extents.z + transform.forward.z * 10;
        spawnLoc = new Vector3(spawnX, 0.0f, spawnZ);
    }

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void Update()
    {
        base.Update();
        updateBuildQueue();
        
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
            rend.material.color = Color.cyan;
        }
        else
        {
            rend.material.color= colorConvert(modelColor.headRed, modelColor.headGreen, modelColor.headBlue, modelColor.headAlpha);
        }

    }

    protected void makeUnit(string unitName)
    {
        buildLine.Enqueue(unitName);
    }

    protected void updateBuildQueue()
    {
        if (buildLine.Count > 0)
        {
            currBuildTime += Time.deltaTime * ResourceManagerScript.buildSpeed;
            if (currBuildTime > maxBuildTime)
            {
                if (player)
                {
                    player.addUnit(buildLine.Dequeue(),spawnLoc,transform.rotation);
                }
                currBuildTime = 0.0f;
            }
        }
    }

    public string[] getBuildQueueVal()
    {
        string[] val = new string[buildLine.Count];
        int i = 0;
        foreach(string unit in buildLine)
        {
            val[i++] = unit;
        }
        return val;
    }

    public float getBuildPercent()
    {
        return currBuildTime / maxBuildTime;
    }

    protected void createUnit(string unitName)
    {
        buildLine.Enqueue(unitName);
    }
}
