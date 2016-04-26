using UnityEngine;
using System.Collections;

public class BarracksScript : BuildingScript {

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        actionArr = new string[] { "Soldier2", "Commander" };
    }

    protected override void Update()
    {
        base.Update();
        

    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    public override void doAction(string action)
    {
        base.doAction(action);
        makeUnit(action);
    }
}
