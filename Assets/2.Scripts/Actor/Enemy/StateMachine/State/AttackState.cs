using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackState : State<Enemy>
{
    private float _attackTime = 0.2f;
    private float _elapsedTime = 0f;

    private const string _targetBoxLayer = "Box";
    private const string _targetHeroLayer = "Hero";
    public override void OnInitialized() //셋팅
    {
        
    }

    public override void OnEnter() //한번실행
    {
        _context.CurrentStateType = StateType.ATTACK;
        _context.Rigidbody2D.velocity = Vector2.zero;
        
        AttackAction();
        _elapsedTime = 0f;
    }
    public override void FixedUpdate(float deltaTime) //게속업데이트
    {
        _elapsedTime += deltaTime;
        if (_elapsedTime >= _attackTime)
        {
            _stateMachine.ChangeState<IdleState>();
        }
    }

    public override void OnExit() //나가기
    {
    }

    private void AttackAction()
    {
        LayerMask targetLayerMask = LayerMask.GetMask(
            _targetBoxLayer, 
            _targetHeroLayer);
        
        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(_context.AttackArea.bounds.center,
            _context.AttackArea.size, 0f, targetLayerMask);
        
        foreach (Collider2D hitActor in hitTargets)
        {
            if (TargetBox(hitActor))
            {
                Debug.Log("BOX Attack");
                break;
            }
        
            if (TargetHero(hitActor))
            {
                Debug.Log("hero Attack");
                break;
            }
        }

    }

    private bool TargetBox(Collider2D hitActor)
    {
        Box boxComponent = hitActor.GetComponent<Box>();
        if (boxComponent != null)
        {
            boxComponent.TakeDamage_I(_context.ActorState.ActorDamage);
            return true;
        }

        return false;
    }

    private bool TargetHero(Collider2D hitActor)
    {
        Hero heroComponent = hitActor.GetComponent<Hero>();
        if (heroComponent != null)
        {
            heroComponent.ActorState.TakeDamage_I(_context.ActorState.ActorDamage);
            return true;
        }

        return false;
    }
}
