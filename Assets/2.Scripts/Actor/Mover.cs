using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Actor _owner;

    private void Awake()
    {
        _owner = GetComponent<Actor>();
    }

    public void Move(Vector2 direction, float speed)
    {
        Vector2 finalDirection = direction.normalized;
        _owner.Rigidbody2D.velocity = finalDirection * speed;
    }
}
