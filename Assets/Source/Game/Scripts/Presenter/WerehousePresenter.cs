using DeliveryOfGoods.Model;
using System;
using UnityEngine;

public class WerehousePresenter : MonoBehaviour
{
    [SerializeField] private TruckPresenter _truckPresenter;
    [SerializeField] private Transform _startDeliverPoint;
    [SerializeField] private Transform _loadingArea;
    [SerializeField] private SpawnerBox _spawnerBox;

    private const int MaxMissBox = 5;

    private int _currentMissBox;

    public event Action BoxFallen;

    private void OnEnable()
    {
        _truckPresenter.SceneChanged += Reset;
    }

    private void OnDisable()
    {
        _truckPresenter.SceneChanged -= Reset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BoxPresenter boxPresenter))
        {
            BoxFallen?.Invoke();
            boxPresenter.PlayBadParticle();
            boxPresenter.PlayAudio();

            if (_currentMissBox >= MaxMissBox)
            {
                _truckPresenter.ResetScene();
                _currentMissBox = 0;
            }
        }
    }

    public void Reset()
    {
        _spawnerBox.Reset();
        _truckPresenter.transform.position = _startDeliverPoint.position;
        _truckPresenter.Reset();
        _truckPresenter.Move(_loadingArea.position);
    }
}