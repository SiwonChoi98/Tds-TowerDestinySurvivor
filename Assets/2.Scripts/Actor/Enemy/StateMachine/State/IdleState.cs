using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<Enemy>
{
    public override void OnInitialized() //셋팅
    {
    }

    public override void OnEnter() //한번실행
    {
        _context.CurrentStateType = StateType.IDLE;
        
    }
    public override void FixedUpdate(float deltaTime) //게속업데이트
    {
        _context.CheckAttackAction();
        _context.CheckJumpAction();
        
        _stateMachine.ChangeState<MoveState>();
    }

    public override void OnExit() //나가기
    {
    }

}
