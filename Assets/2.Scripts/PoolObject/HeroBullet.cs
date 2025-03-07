using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBullet : BasePoolObject
{
    private Rigidbody2D _rigidbody2D;
    private float _damage;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    private void Move()
    {
        _rigidbody2D.velocity = transform.right * 10;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<ActorState>().TakeDamage_I(_damage);
            
            ReturnToPool();
        }
        
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            ReturnToPool();
        }
    }
}
