using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCanvas : MonoBehaviour
{
    private Box _box;
    [SerializeField] private Slider _hpSlider;

    private void Awake()
    {
        _box = GetComponentInParent<Box>();
    }

    private void Start()
    {
        _box.UpdateBoxCanvas += UpdateHealth;
    }

    private void OnDestroy()
    {
        _box.UpdateBoxCanvas -= UpdateHealth;
    }

    public void UpdateHealth()
    {
        _hpSlider.value = _box.BoxCurrentHealth / _box.BoxMaxHealth;
    }
}
