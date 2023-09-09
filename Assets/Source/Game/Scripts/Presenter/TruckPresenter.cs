using System;
using UnityEngine;
using DeliveryOfGoods.Model;

public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _smokesExhaust;
    [SerializeField] private DeliverPoint _endDeliverPoint;
    [SerializeField] private ParticleSystem _smokeExplosion;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Config _config;

    private const float AnimationTime = 3;

    private int _boxInBody;
    private bool _isDelivery;

    public event Action SceneChanged;
    public event Action<int> AddScoreBody;
    public event Action LevelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxPresenter box))
        {
            box.PlayGoodParticle();
            box.PlayAudio();
            _boxInBody++;
            AddScoreBody?.Invoke(_boxInBody);

            if (_boxInBody >= _config.CurrentDeliverBox && _isDelivery == false)
                Deliver(_endDeliverPoint.transform.position);
        }
    }

    public void Reset()
    {
        _isDelivery = false;
        _boxInBody = 0;
        AddScoreBody?.Invoke(_boxInBody);
    }

    public void ResetScene()
    {
        _sceneLoader.Load();
        _smokeExplosion.Play();
        SceneChanged?.Invoke();
    }

    public void Move(Vector3 targetPosition)
    {
        PlayExhaust();
        transform.LeanMoveZ(targetPosition.z, AnimationTime).setOnComplete(StopExhaust);
    }
    
    public void PassOnNextLevel()
    {
        _smokeExplosion.Play();
        StopExhaust();
        SceneChanged?.Invoke();
    }

    private void Deliver(Vector3 targetPosition)
    {
        _isDelivery = true;
        PlayExhaust();
        transform.LeanMoveZ(targetPosition.z, AnimationTime).setOnComplete(Complete);
    }

    private void Complete()
    {
        LevelCompleted?.Invoke();
    }

    private void PlayExhaust()
    {
        foreach (ParticleSystem smoke in _smokesExhaust)
            smoke.Play();
    }

    private void StopExhaust()
    {
        foreach (ParticleSystem smoke in _smokesExhaust)
            smoke.Stop();
    }
}