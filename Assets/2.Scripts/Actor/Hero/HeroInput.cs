using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroInput : MonoBehaviour
{
    private Hero _hero;

    private void Awake()
    {
        _hero = GetComponent<Hero>();
    }

    private void Update()
    {
        GetTouchDown();
        GetTouch();
    }

    private void GetTouchDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hero.RotateGunToTouch(Input.mousePosition);

            _hero.SpawnBullet();
        }
    }

    private void GetTouch()
    {
        if (Input.GetMouseButton(0))
        {
            _hero.RotateGunToTouch(Input.mousePosition);
        }
    }
}
