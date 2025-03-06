using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private List<SpriteRenderer> _spriteRenderers; 
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        _actorMover.Move(Vector2.left, _moveSpeed);
    }
    
    //활성화 시 셋팅
    public void SetEnemy()
    {
        SetEnemyLayer();
        SetEnemyOrder();
    }
    
    public void SetEnemyLayer()
    {
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
            AddEnemyOrder(2);
        }
        else if (gameObject.layer == LayerMask.NameToLayer(InGameSettings.SecondFloorObjectLayer))
        {
            AddEnemyOrder(1);
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
}
