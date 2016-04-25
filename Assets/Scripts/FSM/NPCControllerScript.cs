using UnityEngine;
using System.Collections;

public class NPCControllerScript : MonoBehaviour {

    public GameObject player;
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
        fsm.currentState.reason(player, gameObject);
        fsm.currentState.act(player, gameObject);
    }

    private void makeFSM()
    {
        IdleState idle = new IdleState();
        idle.AddTransition(Transition.SawPlayer, State.Moving);

        MovingState move = new MovingToPlayerState();
        move.AddTransition(Transition.InAtkRange, State.Attack);

        AttackState atk = new AttackState();
        atk.AddTransition(Transition.DeadTarget, State.Idle);
        atk.AddTransition(Transition.LowHealth, State.Retreat);

        RetreatState retreat = new RetreatState();
        atk.AddTransition(Transition.ReachedSpawn, State.Idle);

        fsm = new FSMSystem();
        fsm.AddState(idle);
        fsm.AddState(move);
        fsm.AddState(atk);
        fsm.AddState(retreat);
    }
}
