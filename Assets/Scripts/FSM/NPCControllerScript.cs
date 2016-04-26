using UnityEngine;
using System.Collections;

public class NPCControllerScript : MonoBehaviour {

    public WorldObjectScript player, gameObj;
    public GameObject gameState; //Needs to be implemented
    public Vector3 spawnPoint;
    private FSM fsm;

    // Use this for initialization
    void Start () {
        makeFSM();
	}

    public void setTransition(Transition t) {

        fsm.doTransition(t);

    }

    // Update is called once per frame
    void FixedUpdate () {
        fsm.currentState.reasoning(player, gameObj);
        fsm.currentState.acting(player, gameObj);
    }

    private void makeFSM()
    {
        IdleState idle = new IdleState(gameState);
        idle.addTransition(Transition.SawPlayer, State.Moving);

        MovingState move = new MovingState();
        move.addTransition(Transition.InAtkRange, State.Attack);

        AttackState atk = new AttackState();
        atk.addTransition(Transition.DeadTarget, State.Idle);
        atk.addTransition(Transition.LowHealth, State.Retreat);

        RetreatState retreat = new RetreatState(spawnPoint);
        atk.addTransition(Transition.ReachedSpawn, State.Idle);

        fsm = new FSM();
        fsm.addState(idle);
        fsm.addState(move);
        fsm.addState(atk);
        fsm.addState(retreat);
    }

    public void setPlayer(WorldObjectScript player)
    {
        this.player = player;
    }
}
