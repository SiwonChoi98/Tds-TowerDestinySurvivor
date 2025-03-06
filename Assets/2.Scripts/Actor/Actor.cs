using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BasePoolObject
{
    public Rigidbody2D Rigidbody2D;
    
    protected Mover _actorMover;
    
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        _actorMover = GetComponent<Mover>();
    }

    protected virtual void FixedUpdate()
    {
        
    }
}
