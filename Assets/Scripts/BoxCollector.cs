using System;
using DG.Tweening;
using UnityEngine;

public class BoxCollector : MonoBehaviour {

    private const int COLLECTABLE_OBJECT_LAYER = 7;

    [Header("Text effect")]
    [SerializeField] private GameObject _collectTextEffect;

    [SerializeField] private Transform _startTransform;
    [SerializeField] private float _flyHeight;
    [SerializeField] private float _flyDuration;
    [SerializeField] private float _disappearDuration;

    [Space(10)] [Header("Particle effect")]
    [SerializeField] private ParticleSystem _particleSystem;
    
    [SerializeField] private Player _player;

    private Transform _cameraTransform;

    private void Awake() {
        _cameraTransform = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != COLLECTABLE_OBJECT_LAYER) return;
        _player.BoxesCount++;
        PlayCollectEffect();
        other.gameObject.SetActive(false);
    }

    private void PlayCollectEffect() {
        var textGO = Instantiate(_collectTextEffect, _startTransform.transform.position, Quaternion.identity, _player.transform);
        var textT = textGO.transform;

        Sequence actions = DOTween.Sequence();
        actions.Append(textT.DOMoveY(textT.position.y + _flyHeight, _flyDuration));
        actions.Append(textT.DOScale(new Vector3(0, 0, 0), _disappearDuration));
        actions.onComplete = () => { Destroy(textGO);;};
        
        _particleSystem.Play();
    }
    
    
}