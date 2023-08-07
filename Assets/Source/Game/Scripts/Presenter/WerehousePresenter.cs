using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerehousePresenter : MonoBehaviour
{
    [SerializeField] private TruckPresenter _truckPresenter;
    [SerializeField] private Transform _startDeliverPoint;
    [SerializeField] private Transform _loadingArea;

    private void OnEnable()
    {
        _truckPresenter.SceneChanged += MoveTruck;
    }

    private void OnDisable()
    {
        _truckPresenter.SceneChanged -= MoveTruck;
    }

    private void Start()
    {
        MoveTruck();
    }

    private void MoveTruck()
    {
        Debug.Log("PUSH");
        _truckPresenter.transform.position = _startDeliverPoint.position;
        _truckPresenter.Reset();
        _truckPresenter.Move(_loadingArea.position);
    }
}