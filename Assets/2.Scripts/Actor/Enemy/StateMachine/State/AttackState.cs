using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<Enemy>
{
    private float _atttackTime = 0.7f;
    private float _elapsedTime = 0f;
    
    public override void OnInitialized() //셋팅
    {
        
    }

    public override void OnEnter() //한번실행
    {
        _context.CurrentStateType = StateType.ATTACK;
        AttackAction();
        _elapsedTime = 0f;
    }
    public override void FixedUpdate(float deltaTime) //게속업데이트
    {
        if (_elapsedTime >= _atttackTime)
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
