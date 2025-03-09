using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameResourceManager : Singleton<InGameResourceManager>
{
    private EtcData _etcData;
    private Dictionary<WeaponType, WeaponData> _weaponDataDic = new Dictionary<WeaponType, WeaponData>();
    public Dictionary<WeaponType, WeaponData> WeaponDataDic => _weaponDataDic;
    protected override void Awake()
    {
        base.Awake();
        
        SetEtcData();
        SetWeaponData();
    }

    private void SetEtcData()
    {
        _etcData = Resources.Load<EtcData>("ETCData");
    }

    private void SetWeaponData()
    {
        WeaponData[] weaponDatas = Resources.LoadAll<WeaponData>("WeaponData/");

        foreach (var weapon in weaponDatas)
        {
            // if (!_weaponDataDic.ContainsKey(weapon.WeaponType))
            // {
            //     _weaponDataDic[weapon.WeaponType] = new();
            // }
            
            _weaponDataDic[weapon.WeaponType] = weapon;
        }
    }

    public EtcData GetEtcData()
    {
        return _etcData;
    }
}
