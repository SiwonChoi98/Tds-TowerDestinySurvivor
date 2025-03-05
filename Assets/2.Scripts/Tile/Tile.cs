using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("##Move")] 
    [SerializeField] private bool _isMoving = false;
    
    #region UnityLifeSycle

    private void Awake()
    {
        SetComponent();
    }

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(InGameSettings.TileEndPosTag))
        {
            ResetTilePosition();
        }
    }
    
    #endregion

    private void SetComponent()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void MoveTowards()
    {
        if (!_isMoving)
            return;
        
        _rigidbody2D.MovePosition(_rigidbody2D.position + 
                                  Vector2.left * 
                                  (BattleManager.Instance.GetTileSpeed() * Time.fixedDeltaTime));
    }

    private void ResetTilePosition()
    {
        _isMoving = false;
        
        transform.position = BattleManager.Instance.GetTileStartPos().position;
        _isMoving = true;

    }
}
