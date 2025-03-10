using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Box : MonoBehaviour
{
    [SerializeField] private int _buttonBoxIndex;

    [SerializeField] private Button Btn_Weapon0;
    [SerializeField] private Button Btn_Weapon1;
    
    private void Awake()
    {
        Btn_Weapon0.onClick.AddListener(() => Btn_SelectWeapon(0));
        Btn_Weapon1.onClick.AddListener(() => Btn_SelectWeapon(1));
    }

    private void Btn_SelectWeapon(int index)
    {
        PoolObjectType poolObjectType = PoolObjectType.WEAPON_0;
        WeaponType weaponType = WeaponType.FLAMETHROWER;
        switch (index)
        {
            case 0:
                poolObjectType = PoolObjectType.WEAPON_0;
                weaponType = WeaponType.FLAMETHROWER;
                MainUI.Instance.ShowSkillFlame();
                break;
            case 1:
                poolObjectType = PoolObjectType.WEAPON_1;
                weaponType = WeaponType.DRILL;
                MainUI.Instance.ShowSkillDrill();
                break;
        }

        WeaponData weaponData = InGameResourceManager.Instance.WeaponDataDic[weaponType];
        
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnFromPool(
            poolObjectType,
            (BasePoolObject)weaponData.Weapon,
            BattleManager.Instance.Truck.Boxes[_buttonBoxIndex].transform);
        
        basePoolObject.SetPoolObjectType(poolObjectType);

        Weapon weapon = basePoolObject as Weapon;
        if (weapon)
        {
            weapon.SetWeaponData(weaponData);
        }

        BattleManager.Instance.Truck.Boxes[_buttonBoxIndex].AddEquipWeapon(weapon);
        BattleManager.Instance.AddTruckBoxWeapon(weapon);
        
        HideWeaponButton();
        
        SoundManager.Instance.Play_SFX(SFX_SoundType.BUTTON, 0.1f);
    }

    private void HideWeaponButton()
    {
        Btn_Weapon0.gameObject.SetActive(false);
        Btn_Weapon1.gameObject.SetActive(false);
    }
}
