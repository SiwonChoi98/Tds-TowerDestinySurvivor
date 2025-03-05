using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [Header("##Map")]
    [SerializeField] private bool _isMapMoving = false;
    [SerializeField] private List<Tile> _tiles;
    [SerializeField] private float _tileSpeed = 2f;
    [SerializeField] private Transform _tileStartPos;
    [SerializeField] private Transform _tileEndPos;
    
    #region UnityLifeSycle

    protected override void Awake()
    {
        base.Awake();
        
    }

    private void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        MoveTiles();
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
    
}
