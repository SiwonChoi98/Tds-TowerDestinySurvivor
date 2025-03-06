using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    ENEMY_MELEE = 1001,
    ENEMY_RANGE = 1002,
}

public enum StateType
{
    ATTACK,
    MOVE,
    JUMP,
    DEAD,
    IDLE
    
}