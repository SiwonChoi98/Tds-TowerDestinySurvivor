using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackState : State<Enemy>
{
    private float _attackTime = 0.2f;
    private float _elapsedTime = 0f;
    
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
        
    }
}
