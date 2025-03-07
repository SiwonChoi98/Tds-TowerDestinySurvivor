using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : Actor
{
    private ActorState _actorState;
    
    [Header("공격")]
    [SerializeField] private Transform _gunSpriteTransform;
    [SerializeField] private HeroBullet _heroBullet;
    private const float _gunSpriteInterpolationLength = 34f;

    private void Awake()
    {
        _actorState = GetComponent<ActorState>();
    }

    public void RotateGunToTouch(Vector2 screenPosition)
    {
        if (_gunSpriteTransform == null) return;

        Vector2 touchPosition = BattleManager.Instance.MainCamera.ScreenToWorldPoint(screenPosition);
        Vector2 direction = (touchPosition - (Vector2)_gunSpriteTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _gunSpriteTransform.rotation = Quaternion.Euler(0, 0, angle - _gunSpriteInterpolationLength);
    }

    public void SpawnBullet()
    {
        // 현재 총구의 회전 값을 적용하여 총알 생성
        Quaternion bulletRotation = Quaternion.Euler(0, 0, _gunSpriteTransform.eulerAngles.z + _gunSpriteInterpolationLength);
        
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnFromPool(PoolObjectType.HERO_BULLET, (BasePoolObject)_heroBullet,
            _gunSpriteTransform.position, bulletRotation);
        
        basePoolObject.SetPoolObjectType(PoolObjectType.HERO_BULLET);
        
        HeroBullet heroBullet = basePoolObject as HeroBullet;
        if (heroBullet)
        {
            heroBullet.SetDamage(_actorState.ActorDamage);
        }
    }
}
