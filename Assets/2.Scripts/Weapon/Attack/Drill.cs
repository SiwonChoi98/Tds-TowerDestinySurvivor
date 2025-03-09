using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Weapon
{
    protected override void Attack()
    {
        base.Attack();
        
        LayerMask targetLayerMask = LayerMask.GetMask(
            InGameSettings.FirstFloorObjectLayer, 
            InGameSettings.SecondFloorObjectLayer,
            InGameSettings.ThirdFloorObjectLayer);
        
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackRange.bounds.center,
            AttackRange.size, 0f, targetLayerMask);
        
        // 감지된 대상에게 데미지 적용
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<ActorState>()?.TakeDamage_I(_weaponData.Damage);
            break;
        }
    }
}
