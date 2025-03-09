using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    public string WeaponName;
    //무기 타입
    public WeaponType WeaponType;
    //생성될 무기
    public Weapon Weapon;

    public float Damage;
    public float AttackCooltime;
    public float AttackMaintenanceTime;
}
