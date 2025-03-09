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
    
    public override void Skill()
    {
        base.Skill();
        
        DrillWeaponData drillWeaponData = _weaponData as DrillWeaponData;
        if (drillWeaponData)
        {
            BasePoolObject basePoolObject = PoolManager.Instance.SpawnFromPool(PoolObjectType.DRILL_BULLET, drillWeaponData.SkillBullet,
                transform.position, Quaternion.identity);
            
            basePoolObject.SetPoolObjectType(PoolObjectType.DRILL_BULLET);
            DrillBullet drillBullet = basePoolObject as DrillBullet;
            if (drillBullet)
            {
                drillBullet.SetDamage(_weaponData.Damage * 2);
            }
        }
    }
}
