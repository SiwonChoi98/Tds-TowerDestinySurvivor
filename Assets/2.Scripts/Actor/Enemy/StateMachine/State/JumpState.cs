using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State<Enemy>
{
    private float _jumpTime = 0.7f; // 점프 유지 시간
    private float _elapsedTime = 0f;

    public override void OnEnter()
    {
        _context.CurrentStateType = StateType.JUMP;
        Jump(Vector2.up, _context.JumpPower);
        _elapsedTime = 0f;
    }

    public override void FixedUpdate(float deltaTime)
    {
        _elapsedTime += deltaTime;

        if (_elapsedTime >= _jumpTime)
        {
            _stateMachine.ChangeState<IdleState>();
        }
    }

    public override void OnExit()
    {
    }

    public void Jump(Vector2 jumpDirection, float jumpStrength)
    {
        _context.Rigidbody2D.AddForce(jumpDirection * jumpStrength, ForceMode2D.Impulse);
    }
}