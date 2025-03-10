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
    [SerializeField] private Slider _hpChaseSlider;
    [SerializeField] private List<SpriteRenderer> _bodySpriteRenderers;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.2f);
    private void Awake()
    {
        _actorState = GetComponentInParent<ActorState>();
        if (_actorState != null)
        {
            _actorState.UpdateHealthAction += UpdateHealth;
            _actorState.UpdateChangeBodyColorAction += ChangeBodyColor;
        }
        else
        {
            Debug.LogError("ActorState is null in ActorCanvas!");
        }
    }

    public void Start()
    {
        //UpdateHealth();
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
        
        if(gameObject.activeInHierarchy)
            StartCoroutine(UpdateHeroChaseSlider());
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
    
    private IEnumerator UpdateHeroChaseSlider()
    {
        yield return _waitForSeconds;

        while (_hpChaseSlider.value > _hpSlider.value) 
        {
            _hpChaseSlider.value -= Time.deltaTime;
            yield return null;
        }

        _hpChaseSlider.value = _hpSlider.value;
    }
}
