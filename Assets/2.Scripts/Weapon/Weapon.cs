using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BasePoolObject
{
    protected WeaponData _weaponData;
    
    [Header("##Attack")]
    public BoxCollider2D AttackRange;
    [SerializeField] private float _updateAttackCooltime;
    protected bool _isAttacking = false;
    public void SetWeaponData(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }

    public WeaponData GetWeaponData()
    {
        return _weaponData;
    }

    protected virtual void Attack()
    {
        if (!_isAttacking)
            return;

        StartCoroutine(AttackCoroutine());
    }

    public virtual void Skill()
    {
        _isAttacking = true;
        _updateAttackCooltime = 0;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(_weaponData.AttackMaintenanceTime);
        _isAttacking = false;
    }

    protected virtual void Update()
    {
        if (!BattleManager.Instance.IsGameStart)
            return;
        
        UpdateAttackTime();
    }

    private void UpdateAttackTime()
    {
        if (_isAttacking)
            return;

        if (_updateAttackCooltime < _weaponData.AttackCooltime)
        {
            _updateAttackCooltime += Time.deltaTime;
            if (_updateAttackCooltime >= _weaponData.AttackCooltime)
            {
                _isAttacking = true;
                _updateAttackCooltime = 0;
                Attack();
            }
        }
    }
}
