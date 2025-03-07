using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class JumpState : State<Enemy>
{
    private float _jumpTime = 0.7f; // 점프 유지 시간
    private float _elapsedTime = 0f;

    public float jumpHeight = 1.3f; // 점프 높이
    public float jumpDistance = 1f; // 점프 거리
    public float jumpDuration = 1f; // 점프 시간
    
    public override void OnEnter()
    {
        _context.CurrentStateType = StateType.JUMP;
        Jump(Vector2.up, _context.ActorState.ActorJumpPower);
        //JumpForward();
        
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
    
    // private void JumpForward()
    // {
    //     Vector3 jumpTarget = _context.transform.position + new Vector3(-jumpDistance, 0, 0);
    //     _context.transform.DOJump(jumpTarget, jumpHeight, 1, jumpDuration).SetEase(Ease.Linear);
    // }
    
}