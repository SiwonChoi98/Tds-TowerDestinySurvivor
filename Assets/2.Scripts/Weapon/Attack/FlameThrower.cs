using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlameThrower : Weapon
{
    [SerializeField] private GameObject _attackPs;

    [SerializeField] private List<ParticleSystem> _particleSystem;
    protected override void Update()
    {
        base.Update();
        if (_isAttacking)
        {
            AutoAim();
        }
    }
    
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(DamageOverTime(_weaponData.Damage, false));
    }
    
    public override void Skill()
    {
        base.Skill();
        StartCoroutine(DamageOverTime(_weaponData.Damage * 2, true));
    }
    
    [Obsolete("Obsolete")]
    private IEnumerator DamageOverTime(float damage, bool isSkill)
    {
        _attackPs.SetActive(true);
        if (isSkill) ChangeFlameColor(Color.red);
        else ChangeFlameColor(Color.white);
        
        float damageInterval = 0.3f;
        LayerMask targetLayerMask = LayerMask.GetMask(
            InGameSettings.FirstFloorObjectLayer, 
            InGameSettings.SecondFloorObjectLayer,
            InGameSettings.ThirdFloorObjectLayer
        );

        while (_isAttacking) // 유지시간 동안 반복
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
                AttackRange.bounds.center, AttackRange.size, 0f, targetLayerMask
            );

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<ActorState>()?.TakeDamage_I(damage);
            }

            yield return new WaitForSeconds(damageInterval);
        }
        _attackPs.SetActive(false);
    }
    
    public GameObject FindClosestEnemy()
    {
        LayerMask enemyLayerMask = LayerMask.GetMask(
            InGameSettings.FirstFloorObjectLayer, 
            InGameSettings.SecondFloorObjectLayer, 
            InGameSettings.ThirdFloorObjectLayer);
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 8, enemyLayerMask);

        if (hits.Length == 0) return null;

        // 가장 가까운 적을 찾아 반환
        return hits.OrderBy(h => Vector2.Distance(transform.position, h.transform.position))
            .FirstOrDefault()?.gameObject;
    }
    
    public void RotateGunToWorldPosition(Vector2 worldPosition)
    {
        if (transform == null) return;

        Vector2 direction = (worldPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    private void AutoAim()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            RotateGunToWorldPosition(closestEnemy.transform.position);
        }
    }

    [Obsolete("Obsolete")]
    private void ChangeFlameColor(Color color)
    {
        for (int i = 0; i < _particleSystem.Count; i++)
        {
            _particleSystem[i].startColor = color;
        }
    }
}
