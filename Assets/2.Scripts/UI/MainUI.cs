using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : Singleton<MainUI>
{
    [SerializeField] private GameObject _uiBoxGroup;

    [SerializeField] private Button Btn_gameStart;

    [SerializeField] private Button Btn_DrillSkill;
    [SerializeField] private Button Btn_FlameSkill;

    [SerializeField] private FailedPopup _failedPopup;
    protected override void Awake()
    {
        base.Awake();
        
        Btn_gameStart.onClick.AddListener(Btn_GameStart);
        Btn_DrillSkill.onClick.AddListener(Btn_SkillDrill);
        Btn_FlameSkill.onClick.AddListener(Btn_SkillFlame);
    }

    private void HideUIBoxGroup()
    {
        _uiBoxGroup.SetActive(false);
    }

    private void HideBtnGameStart()
    {
        Btn_gameStart.gameObject.SetActive(false);
    }

    public void ShowFailedPopup()
    {
        _failedPopup.gameObject.SetActive(true);
    }

    // public void ShowUIBoxGroup()
    // {
    //     _uiBoxGroup.SetActive(true);
    // }

    private void Btn_GameStart()
    {
        BattleManager.Instance.IsGameStart = true;
        
        HideUIBoxGroup();
        HideBtnGameStart();
    }
    
    //drill
    private void Btn_SkillDrill()
    {
        BattleManager.Instance.BoxSkill_All(WeaponType.DRILL);
    }

    //flameThrower
    private void Btn_SkillFlame()
    {
        BattleManager.Instance.BoxSkill_All(WeaponType.FLAMETHROWER);
    }
}
