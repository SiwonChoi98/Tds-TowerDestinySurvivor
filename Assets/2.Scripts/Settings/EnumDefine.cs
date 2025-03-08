using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    ENEMY_MELEE = 1001,
    ENEMY_RANGE = 1002,
    
    HERO_BULLET = 2001,
    
    BOX_OBJECT = 3001,
    
    TEXT_DAMAGE = 10001,
    
    
}

public enum StateType
{
    ATTACK,
    MOVE,
    JUMP,
    DEAD,
    IDLE
    
}