using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : Actor
{
    private ActorState _actorState;
    private HeroInput _heroInput;
    public HeroInput HeroInput => _heroInput;
    
    
    private HeroAttack _heroAttack;
    public HeroAttack HeroAttack => _heroAttack;
    
    [Header("공격")]
    [SerializeField] private Transform _gunSpriteTransform;
    [SerializeField] private HeroBullet _heroBullet;
    [SerializeField] private GameObject _gunRangeShotGameObject;
    private const float _gunSpriteInterpolationLength = 34f;

    private void Awake()
    {
        _actorState = GetComponent<ActorState>();
        _heroInput = GetComponent<HeroInput>();
        _heroAttack = GetComponent<HeroAttack>();
    }

    public void RotateGunToTouch(Vector2 screenPosition)
    {
        if (_gunSpriteTransform == null) return;

        Vector2 touchPosition = BattleManager.Instance.MainCamera.ScreenToWorldPoint(screenPosition);
        Vector2 direction = (touchPosition - (Vector2)_gunSpriteTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _gunSpriteTransform.rotation = Quaternion.Euler(0, 0, angle - _gunSpriteInterpolationLength);
    }
    public void RotateGunToWorldPosition(Vector2 worldPosition)
    {
        if (_gunSpriteTransform == null) return;

        Vector2 direction = (worldPosition - (Vector2)_gunSpriteTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _gunSpriteTransform.rotation = Quaternion.Euler(0, 0, angle - _gunSpriteInterpolationLength);
    }

    // public void SpawnBullet()
    // {
    //     // 현재 총구의 회전 값을 적용하여 총알 생성
    //     Quaternion bulletRotation = Quaternion.Euler(0, 0, _gunSpriteTransform.eulerAngles.z + _gunSpriteInterpolationLength);
    //     
    //     BasePoolObject basePoolObject = PoolManager.Instance.SpawnFromPool(PoolObjectType.HERO_BULLET, (BasePoolObject)_heroBullet,
    //         _gunSpriteTransform.position, bulletRotation);
    //     
    //     basePoolObject.SetPoolObjectType(PoolObjectType.HERO_BULLET);
    //     
    //     HeroBullet heroBullet = basePoolObject as HeroBullet;
    //     if (heroBullet)
    //     {
    //         heroBullet.SetDamage(_actorState.ActorDamage);
    //     }
    // }
    
    public void SpawnBullet()
    {
        int bulletCount = _heroAttack.BulletSpawnCount; // 총알 개수
        float angleOffset = 3f; // 각 총알마다 변경할 회전각
        
        for (int i = 0; i < bulletCount; i++)
        {
            // 첫 번째 총알 기준으로 일정한 각도를 추가
            float newAngle = _gunSpriteTransform.eulerAngles.z + _gunSpriteInterpolationLength - (i * angleOffset);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, newAngle);
        
            BasePoolObject basePoolObject = PoolManager.Instance.SpawnFromPool(
                PoolObjectType.HERO_BULLET, 
                (BasePoolObject)_heroBullet,
                _gunSpriteTransform.position, 
                bulletRotation
            );

            basePoolObject.SetPoolObjectType(PoolObjectType.HERO_BULLET);

            HeroBullet heroBullet = basePoolObject as HeroBullet;
            if (heroBullet)
            {
                heroBullet.SetDamage(_actorState.ActorDamage);
            }
        }
    }

    public void SetGunShotRange(bool enable)
    {
        _gunRangeShotGameObject.SetActive(enable);
    }
}
