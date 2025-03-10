using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DamageText : BasePoolObject
{
    [SerializeField] private Text _damage_T;
    
    public void SetDamageText(float damage)
    {
        _damage_T.text = damage.ToString();
    }
}