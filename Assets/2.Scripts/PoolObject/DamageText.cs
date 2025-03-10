using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DamageText : BasePoolObject
{
    [SerializeField] private Text _damage_T;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);
    public void SetDamageText(float damage)
    {
        _damage_T.text = damage.ToString();
        StartCoroutine(SetDamageTextCo());
    }

    private IEnumerator SetDamageTextCo()
    {
        yield return _waitForSeconds;
        ReturnToPool();
    }
}