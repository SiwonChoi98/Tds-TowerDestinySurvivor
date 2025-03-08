using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class HeroInput : MonoBehaviour
{
    private Hero _hero;
    
    [SerializeField] private Vector2 _reservationPosition; // 터치한 목표 위치 (없으면 자동 조준)
    [SerializeField] private bool _isTargetReservation = false; // 목표를 설정했는지 확인
    [SerializeField] private bool _isDrag = false;
    public bool IsTargetReservation => _isTargetReservation;
    public bool IsDrag => _isDrag;
    private void Awake()
    {
        _hero = GetComponent<Hero>();
    }

    private void Update()
    {
        GetTouchDown();
        GetTouch();
        GetTouchUp();
    }

    private void GetTouchDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hero.RotateGunToTouch(Input.mousePosition);
            _hero.SetGunShotRange(true);

            //예약
            if (!_isTargetReservation)
            {
                SetTargetReservation(true);
                _reservationPosition = Input.mousePosition;
            }
            //_hero.SpawnBullet();
        }
    }

    private void GetTouch()
    {
        if (Input.GetMouseButton(0))
        {
            _isDrag = true;
            _hero.RotateGunToTouch(Input.mousePosition);
        }
    }

    private void GetTouchUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isDrag = false;
            _hero.SetGunShotRange(false);
        }
    }

    public void SetTargetReservation(bool reservation)
    {
        _isTargetReservation = reservation;
    }

    public Vector2 GetReservationTargetPos()
    {
        return _reservationPosition;
    }
}
