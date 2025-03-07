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
        
        // 현재 위치에서 위로 이동할 높이 (최대 2)
        float upHeight = Random.Range(1.0f, 1.2f);

        // 최종적으로 랜덤 위치로 떨어질 범위 (-2 ~ 2 사이)
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, -0.7f); // 아래로 떨어지는 범위
        
        Vector3 upPosition = transform.position + new Vector3(0, upHeight, 0);
        Vector3 randomFallPosition = upPosition + new Vector3(randomX, randomY, 0);

        // 위로 올라가는 애니메이션 (0.5초 동안 이동)
        transform.DOMove(upPosition, 0.4f)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                // 랜덤한 위치로 이동 (0.5초 동안)
                transform.DOMove(randomFallPosition, 0.4f).SetEase(Ease.OutQuad).OnComplete(ReturnToPool);
            });
    }
}