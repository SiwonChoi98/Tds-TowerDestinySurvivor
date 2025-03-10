using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActorCanvas : MonoBehaviour
{
    private ActorState _actorState;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private List<SpriteRenderer> _bodySpriteRenderers;
    
    private void Awake()
    {
        _actorState = GetComponentInParent<ActorState>();
        _actorState.UpdateHealthAction += UpdateHealth;
        _actorState.UpdateChangeBodyColorAction += ChangeBodyColor;
    }

    public void OnEnable()
    {
        UpdateHealth();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _bodySpriteRenderers.Count; i++)
        {
            _bodySpriteRenderers[i].color = Color.white;
        }
    }

    private void OnDestroy()
    {
        _actorState.UpdateHealthAction -= UpdateHealth;
        _actorState.UpdateChangeBodyColorAction -= ChangeBodyColor;
    }
    
    

    private void UpdateHealth()
    {
        _hpSlider.value = _actorState.ActorCurrentHealth / _actorState.ActorMaxHealth;
    }

    private void ChangeBodyColor()
    {
        StartCoroutine(ChangeBodyColorCo());
    }

    private IEnumerator ChangeBodyColorCo()
    {
        for (int i = 0; i < _bodySpriteRenderers.Count; i++)
        {
            _bodySpriteRenderers[i].color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);
        
        for (int i = 0; i < _bodySpriteRenderers.Count; i++)
        {
            _bodySpriteRenderers[i].color = Color.white;
        }
    }
}
