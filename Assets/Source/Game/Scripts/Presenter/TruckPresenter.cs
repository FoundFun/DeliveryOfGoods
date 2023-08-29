using Agava.YandexGames.Samples;
using DeliveryOfGoods.Model;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _smokesExhaust;
    [SerializeField] private DeliverPoint _endDeliverPoint;
    [SerializeField] private ParticleSystem _smokeExplosion;
    [SerializeField] private PlaytestingCanvas _playtestingCanvas;

    private const float AnimationTime = 3;

    private int _boxsInBody;

    public bool IsDelivery { get; private set; }

    public event Action SceneChanged;
    public event Action<int> AddScoreBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxPresenter box))
        {
            box.PlayGoodParticle();
            _boxsInBody++;
            AddScoreBody?.Invoke(_boxsInBody);

            if (_boxsInBody >= Config.CurrentDeliverBoxs && IsDelivery == false)
                Deliver(_endDeliverPoint.transform.position);
        }
    }

    public void Reset()
    {
        IsDelivery = false;
        _boxsInBody = 0;
        AddScoreBody?.Invoke(_boxsInBody);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(Config.NameScene + Config.CurrentLevel);
        _smokeExplosion.Play();
        SceneChanged?.Invoke();
    }

    public void Move(Vector3 targetPosition)
    {
        PlayExhaust();
        transform.LeanMoveZ(targetPosition.z, AnimationTime).setOnComplete(StopExhaust);
    }

    private void Deliver(Vector3 targetPosition)
    {
        IsDelivery = true;
        PlayExhaust();
        transform.LeanMoveZ(targetPosition.z, AnimationTime).setOnComplete(SwitchScene);
    }

    private void SwitchScene()
    {
        Config.Improve();
        SceneManager.LoadScene(Config.NameScene + Config.CurrentLevel);
        _smokeExplosion.Play();
        StopExhaust();
        SceneChanged?.Invoke();
        _playtestingCanvas.OnSetCloudSaveDataButtonClick();
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