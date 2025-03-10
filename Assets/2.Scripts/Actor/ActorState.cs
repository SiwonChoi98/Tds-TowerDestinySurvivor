using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ActorState : MonoBehaviour, IDamage
{
    private Actor _owner;
    private void Awake()
    {
        _owner = GetComponent<Actor>();
    }

    [Header("체력")] 
    [SerializeField] private float _actorCurrentHealth;
    public float ActorCurrentHealth
    {
        get => _actorCurrentHealth;
        set
        {
            _actorCurrentHealth = Mathf.Clamp(value, 0, _actorMaxHealth);

        }
    }
    [SerializeField] private float _actorMaxHealth;
    public float ActorMaxHealth
    {
        get => _actorMaxHealth;
        set => _actorMaxHealth = value;
    }

    public Action UpdateHealthAction;
    public Action UpdateChangeBodyColorAction;
    [SerializeField] private GameObject _healthCanvasObject;
    
    [Header("방어력")]
    [SerializeField] private int _actorDefense;
    public int ActorDefense
    {
        get => _actorDefense;
        set => _actorDefense = value;
    }
    
    [Header("공격력")]
    [SerializeField] private float _actorDamage;
    public float ActorDamage
    {
        get => _actorDamage;
        set => _actorDamage = value;
    }
    
    [Header("공격속도")]
    [SerializeField] private float _actorAttackCooltime;
    public float ActorAttackCooltime
    {
        get => _actorAttackCooltime;
        set => _actorAttackCooltime = Mathf.Clamp(value, 0.1f, 10);
    }
    [Header("공격 사거리")]
    [SerializeField] private float _actorAttackDistance;
    public float ActorAttackDistance => _actorAttackDistance;

    [Header("이동속도")]
    [SerializeField] private float _actorMoveSpeed;
    public float ActorMoveSpeed => _actorMoveSpeed;
    
    [Header("점프 파워")]
    [SerializeField] private float _actorJumpPower;
    public float ActorJumpPower => _actorJumpPower;

    private void OnEnable()
    {
        _healthCanvasObject.SetActive(false);

        if (_healthCanvasObject.activeSelf)
        {
            UpdateHealthAction?.Invoke();
        }
        
    }

    public void SetHealth(float health)
    {
        _actorCurrentHealth = health;
        
        if (_healthCanvasObject.activeSelf)
        {
            UpdateHealthAction?.Invoke();
        }
    }
    
    public void TakeDamage_I(float damage)
    {
        if (_actorCurrentHealth <= 0)
            return;
        
        _actorCurrentHealth -= damage;
        ISpawnDamageText_I(damage);
        UpdateHealthCanvas();
        
        Dead();
    }

    public void ISpawnDamageText_I(float damage)
    {
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnFromPool(PoolObjectType.TEXT_DAMAGE,
            (BasePoolObject)InGameResourceManager.Instance.GetEtcData().DamageText, transform.position + new Vector3(0, 2, 0),
            Quaternion.identity);
        
        basePoolObject.SetPoolObjectType(PoolObjectType.TEXT_DAMAGE);
        
        DamageText damageText = basePoolObject as DamageText;
        if (damageText)
        {
            damageText.SetDamageText(damage);
        }

    }

    private void UpdateHealthCanvas()
    {
        if(!_healthCanvasObject.activeSelf) 
        {
            _healthCanvasObject.SetActive(true);
        }

        if (_healthCanvasObject.activeSelf)
        {
            UpdateHealthAction?.Invoke();
            UpdateChangeBodyColorAction?.Invoke();
        }
    }
    
    private void Dead()
    {
        if (_actorCurrentHealth > 0)
            return;
        
        _owner.ReturnToPool();
    }
}
