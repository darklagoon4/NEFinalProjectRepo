using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM
{

    private List<FSMState> states;

    // Insulation for states
    private FSMState currState;
    public FSMState currentState { get { return currentState; } }

    private State currStateID;
    public State currentStateID { get { return currStateID; } }


    public FSM()
    {
        states = new List<FSMState>();
    }

    public void addState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("FSM ERROR: State is null");
        }

        if (states.Count == 0)
        {
            states.Add(state);
            currState = state;
            currStateID = state.id;
            return;
        }

        //checks

        foreach (FSMState state2 in states)
        {
            if (state2.id == state.id)
            {
                Debug.LogError("FSM ERROR: State " + state.id.ToString() +
                               "  has already been added");
                return;
            }
        }

        states.Add(state);
    }

    public void deleteState(State id)
    {
        if (id == State.NullState)
        {
            Debug.LogError("FSM ERROR: Null state id");
            return;
        }

        foreach (FSMState state in states)
        {
            if (state.id == id)
            {
                states.Remove(state);
                return;
            }
        }

        Debug.LogError("FSM ERROR: Can't find state to delete");
    }

    public void doTransition(Transition trans)
    {
        if (trans == Transition.NullTrans)
        {
            Debug.LogError("FSM ERROR: Null transition");
            return;
        }

        State id = currState.getStateOutput(trans);
        if (id == State.NullState)
        {
            Debug.LogError("FSM ERROR: Can't find transition " + trans.ToString() + " for state " + currentStateID.ToString());
            return;
        }

        currStateID = id;
        foreach(FSMState state in states)
        {
            if(state.id == currStateID)
            {
                currState.doBeforeLeaving();
                currState = state;
                currState.doBeforeEntering();
                break;
            }
        }
    }
}