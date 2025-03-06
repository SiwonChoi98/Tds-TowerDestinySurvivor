using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BasePoolObject _enemyPrefab;

    public void SpawnEnemy()
    {
        BasePoolObject enemyObject = PoolManager.Instance.SpawnFromPool(PoolObjectType.ENEMY_MELEE, 
            _enemyPrefab, transform.position, Quaternion.identity);
        
        enemyObject.SetPoolObjectType(PoolObjectType.ENEMY_MELEE);

        Enemy enemy = enemyObject as Enemy;
        enemy.SetEnemy();
    }
    
}
