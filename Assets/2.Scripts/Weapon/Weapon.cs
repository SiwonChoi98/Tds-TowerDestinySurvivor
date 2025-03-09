using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BasePoolObject
{
    [SerializeField] private WeaponData _weaponData;

    public void SetWeaponData(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }
    
    
}
