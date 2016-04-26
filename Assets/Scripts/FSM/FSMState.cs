using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FSMState{

    protected State stateID;
    protected Dictionary<Transition, State> map = new Dictionary<Transition, State>();
    public State id { get { return stateID; } }

    public void addTransition(Transition trans, State state)
    {
        //checks
        if (trans == Transition.NullTrans)
        {
            Debug.LogError("FSMState ERROR: NullTrans");
            return;
        }

        if (map.ContainsKey(trans))
        {
            Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                           "Cannot to assign to another state");
            return;
        }

        if (state == State.NullState)
        {
            Debug.LogError("FSMState ERROR: NullState");
            return;
        }

        map.Add(trans, state);
    }

    public void delTransition(Transition trans)
    {

        //checks
        if (trans == Transition.NullTrans)
        {
            Debug.LogError("FSMState ERROR: NullTrans");
            return;
        }

        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }

        Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                       " was not on the state's transition list");
    }

    public State getStateOutput(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return State.NullState;
    }

    //overrideable methods

    public abstract void reasoning(WorldObjectScript player, WorldObjectScript npc);

    public abstract void acting(WorldObjectScript player, WorldObjectScript npc);

    public virtual void doBeforeEntering() { }

    public virtual void doBeforeLeaving() { }

    
}
