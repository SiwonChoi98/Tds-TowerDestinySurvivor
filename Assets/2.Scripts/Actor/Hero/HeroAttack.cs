using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private Hero _hero;
    
    private void Awake()
    {
        _hero = GetComponent<Hero>();
    }

    [SerializeField] private int _bulletSpawnCount;
    public int BulletSpawnCount => _bulletSpawnCount;
    private float _updateAttackTime;
    private bool _isAttackPossible = false;

    private void Update()
    {
        UpdateAttackTime();
        Attack();
    }
    private void Attack()
    {
        if (!_isAttackPossible) return; // 쿨타임 중이면 공격 불가

        
        if (_hero.HeroInput.IsDrag || _hero.HeroInput.IsTargetReservation)
        {
            NonAutoAim();
        }
        else
        {
            AutoAim();
        }
    }

    private void UpdateAttackTime()
    {
        if (_isAttackPossible) return;

        if (_hero.ActorState.ActorAttackCooltime > _updateAttackTime)
        {
            _updateAttackTime += Time.deltaTime;
            if (_hero.ActorState.ActorAttackCooltime <= _updateAttackTime)
            {
                _updateAttackTime = 0;
                _isAttackPossible = true;
            }
        }
    }
    public GameObject FindClosestEnemy()
    {
        LayerMask enemyLayerMask = LayerMask.GetMask(
            InGameSettings.FirstFloorObjectLayer, 
            InGameSettings.SecondFloorObjectLayer, 
            InGameSettings.ThirdFloorObjectLayer);
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _hero.ActorState.ActorAttackDistance, enemyLayerMask);

        if (hits.Length == 0) return null;

        // 가장 가까운 적을 찾아 반환
        return hits.OrderBy(h => Vector2.Distance(transform.position, h.transform.position))
            .FirstOrDefault()?.gameObject;
    }
    
    private void AutoAim()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            _hero.RotateGunToWorldPosition(closestEnemy.transform.position);
            _hero.SpawnBullet();
            _isAttackPossible = false;
        }
    }

    private void NonAutoAim()
    {
        if (_hero.HeroInput.IsTargetReservation)
        {
            Vector2 targetPosition = _hero.HeroInput.GetReservationTargetPos();
            
            _hero.RotateGunToTouch(targetPosition);
            _hero.HeroInput.SetTargetReservation(false);
            
            _hero.SpawnBullet();
            _isAttackPossible = false;
        }
        else if (_hero.HeroInput.IsDrag)
        {
            _hero.SpawnBullet();
            _isAttackPossible = false;
        }
    }
    
}
