using UnityEngine;
using System.Collections;
using RTS;
using System;

public class IdleState : FSMState
{
    GameObject gameState;
    public IdleState(GameObject gameState)
    {
        stateID = State.Idle;
        this.gameState = gameState;
    }

    public override void reasoning(WorldObjectScript player, WorldObjectScript npc)
    {
        
        /*This needs to be implemented. The goal is to create a List or Array of all current player units on the map.
        This list would be used to search for player units that are close to the enemy npc and when a player unit comes within 30 game units,
        it will trigger the MovingState and store that player unit as a target for the npc in relevant states.

        Below is the pseudo code for this search.
        */

        //foreach (WorldObjectScript playerObj in gameState.unitArray)
        {
            //if (Vector3.Distance(npc.transform.position, playerObj.transform.position) < 30)
            {
                //npc.GetComponent<NPCControllerScript>().setPlayer(playerObj);
                npc.GetComponent<NPCControllerScript>().setTransition(Transition.SawPlayer);
            }
        }
    }

    public override void acting(WorldObjectScript player, WorldObjectScript npc)
    {
        throw new NotImplementedException();
    }
}
