using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public bool IsGameStart = false;
    
    [Header("##Map")]
    [SerializeField] private bool _isMapMoving = false;
    [SerializeField] private List<Tile> _tiles;
    [SerializeField] private float _tileSpeed = 2f;
    [SerializeField] private Transform _tileStartPos;
    [SerializeField] private Transform _tileEndPos;

    [Header("##Spawner")] 
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _enemySpawnTime;
    [SerializeField] private float _enemySpawnUpdateTime;
    [SerializeField] private bool _isEnemySpawn = false;
    
    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;

    [Header("##Truck")]
    [SerializeField] private Truck _truck;
    public Truck Truck => _truck;
    [SerializeField] private List<Weapon> _truckBoxWeapons;
    
    #region UnityLifeSycle

    protected override void Awake()
    {
        base.Awake();
        
        _mainCamera = Camera.main; // Awake에서 카메라 캐싱
    }

    private void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        if (!IsGameStart)
            return;
        
        MoveTiles();

        UpdateSpawnEnemyTime();
        UpdateSpawnEnemy();
    }

    #endregion
    
    #region Tile

    private void MoveTiles()
    {
        if (!_isMapMoving)
            return;

        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].MoveTowards();
        }
    }

    public Transform GetTileStartPos()
    {
        return _tileStartPos;
    }

    public Transform GetTileEndPos()
    {
        return _tileEndPos;
    }

    public float GetTileSpeed()
    {
        return _tileSpeed;
    }
    #endregion


    #region Enemy

    private void UpdateSpawnEnemy()
    {
        if (!_isEnemySpawn)
            return;
        
        _enemySpawner.SpawnEnemy();
        
        _isEnemySpawn = false;
        _enemySpawnUpdateTime = 0;
    }

    private void UpdateSpawnEnemyTime()
    {
        if (_isEnemySpawn)
            return;

        if (_enemySpawnUpdateTime <= _enemySpawnTime)
        {
            _enemySpawnUpdateTime += Time.fixedDeltaTime;
            
            if (_enemySpawnUpdateTime > _enemySpawnTime)
            {
                _isEnemySpawn = true;
            }
        }
    }

    #endregion

    public void AddTruckBoxWeapon(Weapon weapon)
    {
        _truckBoxWeapons.Add(weapon);
    }

    public void RemoveTruckBoxWeapon(Weapon weapon)
    {
        _truckBoxWeapons.Remove(weapon);
    }

    public void BoxSkill_All(WeaponType weaponType)
    {
        for (int i = 0; i < _truckBoxWeapons.Count; i++)
        {
            if (_truckBoxWeapons[i].GetWeaponData().WeaponType == weaponType)
            {
                _truckBoxWeapons[i].Skill();
            }
        }
    }
}
