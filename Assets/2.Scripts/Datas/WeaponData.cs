using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object Asset/WeaponData")]
public class WeaponData : ScriptableObject
{
    //무기 타입
    public WeaponType WeaponType;
    //생성될 무기
    public Weapon Weapon;


    public float Damage;
    public float AttackCooltime;
    
}
