using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State<Enemy>
{
    public override void OnInitialized() //셋팅
    {
    }

    public override void OnEnter() //한번실행
    {
        _context.CurrentStateType = StateType.MOVE;
    }
    public override void FixedUpdate(float deltaTime) //게속업데이트
    {
        Vector2 finalDirection = Vector2.left.normalized;
        _context.Rigidbody2D.velocity = finalDirection * _context.ActorState.ActorMoveSpeed;

        _context.CheckJumpAction();
        _context.CheckAttackAction();
    }

    public override void OnExit() //나가기
    {
    }
}
