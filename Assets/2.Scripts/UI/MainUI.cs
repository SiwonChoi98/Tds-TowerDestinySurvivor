using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : Singleton<MainUI>
{
    [SerializeField] private GameObject _uiBoxGroup;

    [SerializeField] private Button Btn_gameStart;

    [SerializeField] private Button Btn_DrillSkill;
    [SerializeField] private Text _drillSkillCost_T;
    
    [SerializeField] private Button Btn_FlameSkill;
    [SerializeField] private Text _flameSkillCost_T;
    
    [SerializeField] private FailedPopup _failedPopup;

    [SerializeField] private Text _currentEnergy_T;
    [SerializeField] private Image _currentEnemy_Img;
    
    protected override void Awake()
    {
        base.Awake();
        
        Btn_gameStart.onClick.AddListener(Btn_GameStart);
        Btn_DrillSkill.onClick.AddListener(Btn_SkillDrill);
        Btn_FlameSkill.onClick.AddListener(Btn_SkillFlame);
    }

    private void Start()
    {
        UpdateCurrentEnergyText();
    }

    public void UpdateCurrentEnemyImage()
    {
        _currentEnemy_Img.fillAmount =
            BattleManager.Instance.GetCurrentEnergyAmount() / BattleManager.Instance.GetMaxEnergyAmount();
    }
    public void UpdateCurrentEnergyText()
    {
        _currentEnergy_T.text = BattleManager.Instance.GetCurrentEnergy().ToString();
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
        UpdateCurrentEnergyText();
    }

    public void ShowSkillDrill()
    {
        if (Btn_DrillSkill.gameObject.activeSelf)
            return;
        Btn_DrillSkill.gameObject.SetActive(true);
        _drillSkillCost_T.text = InGameResourceManager.Instance.GetWeaponSkillCost(WeaponType.DRILL).ToString();
    }

    //flameThrower
    private void Btn_SkillFlame()
    {
        BattleManager.Instance.BoxSkill_All(WeaponType.FLAMETHROWER);
        UpdateCurrentEnergyText();
    }

    public void ShowSkillFlame()
    {
        if (Btn_FlameSkill.gameObject.activeSelf)
            return;
        Btn_FlameSkill.gameObject.SetActive(true); 
        _flameSkillCost_T.text = InGameResourceManager.Instance.GetWeaponSkillCost(WeaponType.FLAMETHROWER).ToString();
    }
    
    public void Btn_RePlay()
    {
        SceneManager.LoadScene("MainScene");
    }
}
