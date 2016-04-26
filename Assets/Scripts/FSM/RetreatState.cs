using UnityEngine;
using System.Collections;
using RTS;

public class RetreatState : FSMState
{

    public Vector3 spawnPoint;
    private bool retreating;
    private UnitScript unit;

    public RetreatState(Vector3 spawnPoint)
    {
        stateID = State.Retreat;
        retreating = false;
    }

    public override void reasoning(WorldObjectScript player, WorldObjectScript npc)
    {
        if (Vector3.Distance(npc.transform.position, player.transform.position) < 1)
        {
            retreating = false;
            npc.GetComponent<NPCControllerScript>().setTransition(Transition.ReachedSpawn);
        }
    }

    public override void acting(WorldObjectScript player, WorldObjectScript npc)
    {
        if (!retreating)
        {
            UnitScript unit = npc as UnitScript;
            if (unit)
            {
                PathRequestManagerScript.requestPath(unit.transform.position, spawnPoint, unit.onPathFound);
            }
        }
    }
}
