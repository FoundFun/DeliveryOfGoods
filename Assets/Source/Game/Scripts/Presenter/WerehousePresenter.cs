using System;
using UnityEngine;

public class WerehousePresenter : MonoBehaviour
{
    [SerializeField] private TruckPresenter _truckPresenter;
    [SerializeField] private Transform _startDeliverPoint;
    [SerializeField] private Transform _loadingArea;
    [SerializeField] private SpawnerBox _spawnerBox;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private ParticleSystem _smokeExplosion;

    private int _currentMissBox;

    public event Action BoxFallen;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BoxPresenter boxPresenter))
        {
            BoxFallen?.Invoke();
            boxPresenter.PlayBadParticle();
            boxPresenter.PlayAudio();
        }
    }

    public void Reset()
    {
        _smokeExplosion.Play();
        _sceneLoader.Load();
        _spawnerBox.Reset();
        _truckPresenter.transform.position = _startDeliverPoint.position;
        _truckPresenter.Reset();
        _truckPresenter.Move(_loadingArea.position);
    }
}