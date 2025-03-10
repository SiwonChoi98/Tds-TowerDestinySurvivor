using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCanvas : MonoBehaviour
{
    private Box _box;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _box = GetComponentInParent<Box>();
    }

    private void Start()
    {
        _box.UpdateBoxCanvas += UpdateHealth;
        _box.UpdateChangeBodyColorAction += ChangeBodyColor;
    }

    private void OnDestroy()
    {
        _box.UpdateBoxCanvas -= UpdateHealth;
        _box.UpdateChangeBodyColorAction -= ChangeBodyColor;
    }

    private void UpdateHealth()
    {
        _hpSlider.value = _box.BoxCurrentHealth / _box.BoxMaxHealth;
    }
    
    private void ChangeBodyColor()
    {
        StartCoroutine(ChangeBodyColorCo());
    }

    private IEnumerator ChangeBodyColorCo()
    {
        _spriteRenderer.color = Color.red;
        
        yield return new WaitForSeconds(0.1f);
        
        _spriteRenderer.color = Color.white;
        
    }
}
