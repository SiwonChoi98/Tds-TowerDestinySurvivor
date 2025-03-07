using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : BasePoolObject
{

    public Rigidbody2D Rigidbody2D;
    
    [Header("##StateMachine")]
    protected StateMachine<Enemy> _stateMachine;
    public StateMachine<Enemy> StateMachine => _stateMachine;
    public StateType CurrentStateType;
    
    [Header("##Move")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    [Header("##Jump")] 
    [SerializeField] private float _jumpPower;
    public float JumpPower => _jumpPower;
    
    
    [Header("##Order")]
    [SerializeField] private List<SpriteRenderer> _spriteRenderers;
    
    [Header("##RayCast")]
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private float _rayDistance;
    [SerializeField] private float _backRayDistance;

    #region UnityLifeSycle

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetState();
    }
    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate(Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        //check truck, enemy
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_bodyTransform.position, Vector2.left * _rayDistance); 
        
        // Gizmos.color = Color.blue;
        // Gizmos.DrawRay(_bodyTransform.position, Vector2.up * _backRayDistance); 
    }

    #endregion
    
    //활성화 시 셋팅
    public void SetEnemy()
    {
        SetEnemyLayer();
        SetEnemyOrder();
    }
    private void SetState()
    {
        _stateMachine = new StateMachine<Enemy>(this, new MoveState());
        
        _stateMachine.AddState(new AttackState());
        _stateMachine.AddState(new DeadState());
        _stateMachine.AddState(new JumpState());
        _stateMachine.AddState(new IdleState());
    }
    
    public void SetEnemyLayer()
    {
        //gameObject.layer = LayerMask.NameToLayer(InGameSettings.FirstFloorObjectLayer);
        float value = Random.Range(0f, 1f);
        if (value < 0.4f)
        {
            gameObject.layer = LayerMask.NameToLayer(InGameSettings.FirstFloorObjectLayer); // 0.0 ~ 0.4
        }
        else if (value < 0.7f)
        {
            gameObject.layer = LayerMask.NameToLayer(InGameSettings.SecondFloorObjectLayer); // 0.4 ~ 0.7
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(InGameSettings.ThirdFloorObjectLayer); // 0.7 ~ 1.0
        }
    }

    public void SetEnemyOrder()
    {
        if (gameObject.layer == LayerMask.NameToLayer(InGameSettings.FirstFloorObjectLayer))
        {
            AddEnemyOrder(4);
        }
        else if (gameObject.layer == LayerMask.NameToLayer(InGameSettings.SecondFloorObjectLayer))
        {
            AddEnemyOrder(2);
        }
        else if (gameObject.layer == LayerMask.NameToLayer(InGameSettings.ThirdFloorObjectLayer))
        {
            AddEnemyOrder(0);
        }
    }

    private void AddEnemyOrder(int amount)
    {
        for (int i = 0; i < _spriteRenderers.Count; i++)
        {
            _spriteRenderers[i].sortingOrder += amount;
        }
    }

    private Collider2D UpdateDirectionRayCast(int targetLayer)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(_bodyTransform.position, Vector2.left, _rayDistance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject != gameObject && hit.collider.gameObject.layer == targetLayer) // 자기 자신 제외
            {
                return hit.collider; // 첫 번째로 감지된 적절한 오브젝트 반환
            }
        }

        return null;
    }
    // private Collider2D UpdateOppositeDirectionRayCast(int targetLayer)
    // {
    //     RaycastHit2D[] hits = Physics2D.RaycastAll(_bodyTransform.position, Vector2.up, _backRayDistance);
    //
    //     foreach (var hit in hits)
    //     {
    //         if (hit.collider.gameObject != gameObject && hit.collider.gameObject.layer == targetLayer) // 자기 자신 제외
    //         {
    //             return hit.collider; // 첫 번째로 감지된 적절한 오브젝트 반환
    //         }
    //     }
    //
    //     return null;
    // }
    
    #region ChangeState

    public void CheckJumpAction()
    {
        Collider2D frontHit = UpdateDirectionRayCast(gameObject.layer); // 앞 체크
        if (frontHit == null)
            return;

        // Collider2D backHit = UpdateOppositeDirectionRayCast(gameObject.layer);
        // if (backHit != null)
        //     return;

        _stateMachine.ChangeState<JumpState>();
    }

    public void CheckAttackAction()
    {
        Collider2D hitCollider = UpdateDirectionRayCast(LayerMask.NameToLayer("Truck"));
        if (hitCollider == null) 
            return;
        
        _stateMachine.ChangeState<AttackState>();
    }

    #endregion
    
}
