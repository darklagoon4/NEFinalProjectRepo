using UnityEngine;
using System.Collections;
using RTS;

public class AttackState : FSMState
{

    

    public AttackState()
    {
        stateID = State.Attack;
    }

    public override void reasoning(WorldObjectScript player, WorldObjectScript npc)
    {
        if (player.health <= 0)
        {
            npc.GetComponent<NPCControllerScript>().setTransition(Transition.DeadTarget);
        }
        if (npc.health <= .1 * npc.maxHealth)
        {
            npc.GetComponent<NPCControllerScript>().setTransition(Transition.LowHealth);
        }
    }
    public override void acting(WorldObjectScript player, WorldObjectScript npc)
    {
        npc.rightClick(player.GetComponent<GameObject>(),player.transform.position,null);
    }
}
