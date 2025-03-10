using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameResourceManager : Singleton<InGameResourceManager>
{
    private EtcData _etcData;
    private Dictionary<WeaponType, WeaponData> _weaponDataDic = new Dictionary<WeaponType, WeaponData>();
    public Dictionary<WeaponType, WeaponData> WeaponDataDic => _weaponDataDic;

    private Dictionary<WeaponType, int> _weaponSkillCostDataDic = new Dictionary<WeaponType, int>();
    protected override void Awake()
    {
        base.Awake();
        
        SetData();
        SetWeaponData();
    }

    private void SetData()
    {
        _etcData = Resources.Load<EtcData>("ETCData");
    }
    

    private void SetWeaponData()
    {
        WeaponData[] weaponDatas = Resources.LoadAll<WeaponData>("WeaponData/");

        foreach (var weapon in weaponDatas)
        {
            _weaponDataDic[weapon.WeaponType] = weapon;
            _weaponSkillCostDataDic[weapon.WeaponType] = weapon.SkillCost;
        }
    }

    public EtcData GetEtcData()
    {
        return _etcData;
    }

    public int GetWeaponSkillCost(WeaponType weaponType)
    {
        return _weaponSkillCostDataDic[weaponType];
    }
}
