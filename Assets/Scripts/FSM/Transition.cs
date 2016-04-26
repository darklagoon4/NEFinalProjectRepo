using UnityEngine;
using System.Collections;

public enum Transition
{
    // Non-existing transition
    NullTrans = 0,
    SawPlayer = 1,
    InAtkRange = 2,
    DeadTarget = 3,
    LowHealth = 4,
    ReachedSpawn = 5,
}