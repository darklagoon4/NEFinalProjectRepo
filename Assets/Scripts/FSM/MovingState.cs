using UnityEngine;
using System.Collections;
using RTS;

public class MovingState : FSMState
{

    UnitScript target;

    public MovingState()
    {
        
        stateID = State.Moving;
    }

    public override void reasoning(WorldObjectScript player, WorldObjectScript npc)
    {
        if (Vector3.Distance(npc.transform.position, target.transform.position) < npc.atkRange)
        {
            npc.GetComponent<NPCControllerScript>().setTransition(Transition.InAtkRange);
        }
    }

    public override void acting(WorldObjectScript player, WorldObjectScript npc)
    {
        UnitScript unit = npc as UnitScript;
        if (unit)
        {
            PathRequestManagerScript.requestPath(unit.transform.position, player.transform.position, unit.onPathFound);
        }
        
    }
}
