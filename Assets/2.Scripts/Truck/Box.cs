using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : BasePoolObject, IDamage
{
    private Truck _truck;
    [SerializeField] private Weapon _equipWeapon;
    public Weapon EquipWeapon => _equipWeapon;
    
    [SerializeField] private int _boxIndex;
    [SerializeField] private GameObject _boxCanvasObject;
    public Action UpdateBoxCanvas;
    
    [SerializeField] private float _boxCurrentHealth;
    [SerializeField] private float _boxMaxHealth;
    public float BoxCurrentHealth => _boxCurrentHealth;
    public float BoxMaxHealth => _boxMaxHealth;

    private void Awake()
    {
        SetPoolObjectType(PoolObjectType.BOX_OBJECT);

        _truck = GetComponentInParent<Truck>();
    }
    
    public void TakeDamage_I(float damage)
    {
        _boxCurrentHealth -= damage;
        
        if(!_boxCanvasObject.activeSelf) _boxCanvasObject.SetActive(true);
        UpdateBoxCanvas?.Invoke();

        Dead();
    }

    private void Dead()
    {
        if (_boxCurrentHealth > 0) return;

        _truck.SetBoxPosY(_boxIndex);
        
        BattleManager.Instance.RemoveTruckBoxWeapon(_equipWeapon);
        
        ReturnToPool();
    }
    public void ISpawnDamageText_I(float damage)
    {
    }

    public int GetBoxIndex()
    {
        return _boxIndex;
    }

    public void AddEquipWeapon(Weapon weapon)
    {
        _equipWeapon = weapon;
    }
}
