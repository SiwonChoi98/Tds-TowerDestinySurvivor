using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] private List<Box> _boxes;
    public List<Box> Boxes => _boxes;
    
    [SerializeField] private Hero _hero;
    [SerializeField] private List<float> _boxYPosList;
    
    private Animator _wheelAnimator;
    private static readonly int IsRotation = Animator.StringToHash("IsRotation");

    private void Awake()
    {
        InitBoxPosY();

        _wheelAnimator = GetComponentInChildren<Animator>();
        PlayWheelAnim();
    }

    private void InitBoxPosY()
    {
        for (int i = 0; i < _boxes.Count; i++)
        {
            _boxYPosList[i] = _boxes[i].transform.position.y;
        }
    }

    //박스 사망 시 위치 재수정
    public void SetBoxPosY(int boxIndex)
    {
        int targetIndex = _boxes.FindIndex(b => b.GetBoxIndex() == boxIndex);
        // 리스트에서 박스를 제거
        _boxes.RemoveAt(targetIndex);

        // 사망한 박스보다 뒤에 있는 박스들만 위치 조정
        for (int i = targetIndex; i < _boxes.Count; i++)
        {
            _boxes[i].transform.position = new Vector3(_boxes[i].transform.position.x, _boxYPosList[i]);
        }
        
        _hero.transform.position = new Vector3(_hero.transform.position.x, _boxYPosList[_boxes.Count]);
    }

    public void StopWheelAnim()
    {
        _wheelAnimator.SetBool(IsRotation, false);
    }

    private void PlayWheelAnim()
    {
        _wheelAnimator.SetBool(IsRotation, true);
    }
}
