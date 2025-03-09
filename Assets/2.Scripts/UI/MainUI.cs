using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : Singleton<MainUI>
{
    [SerializeField] private GameObject _uiBoxGroup;

    [SerializeField] private Button Btn_gameStart;
    protected override void Awake()
    {
        base.Awake();
        
        Btn_gameStart.onClick.AddListener(Btn_GameStart);
    }

    private void HideUIBoxGroup()
    {
        _uiBoxGroup.SetActive(false);
    }

    private void HideBtnGameStart()
    {
        Btn_gameStart.gameObject.SetActive(false);
    }

    public void ShowUIBoxGroup()
    {
        _uiBoxGroup.SetActive(true);
    }

    private void Btn_GameStart()
    {
        BattleManager.Instance.IsGameStart = true;
        
        HideUIBoxGroup();
        HideBtnGameStart();
    }
}
