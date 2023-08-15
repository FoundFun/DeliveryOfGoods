using DeliveryOfGoods.Model;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private DeliverPoint _endDeliverPoint;
    [SerializeField] private ParticleSystem _smokeExplosion;

    private const float AnimationTime = 3;

    private int _targetNumberBoxs = 3;
    private int _currentLevel = 0;

    private int _boxsInBody;

    public bool IsDelivery { get; private set; }

    public event Action SceneChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxPresenter box))
        {
            box.Complete();
            _boxsInBody++;

            if (_boxsInBody >= _targetNumberBoxs && IsDelivery == false)
            {
                Deliver(_endDeliverPoint.transform.position);
                _targetNumberBoxs++;
            }
        }
    }

    public void Reset()
    {
        IsDelivery = false;
        _boxsInBody = 0;
    }

    public void Move(Vector3 targetPosition)
    {
        transform.LeanMoveZ(targetPosition.z, AnimationTime);
    }

    private void Deliver(Vector3 targetPosition)
    {
        IsDelivery = true;
        transform.LeanMoveZ(targetPosition.z, AnimationTime).setOnComplete(SwitchScene);
    }

    private void SwitchScene()
    {
        _currentLevel++;
        SceneManager.LoadScene($"Level{_currentLevel}");
        _smokeExplosion.Play();
        SceneChanged?.Invoke();
        Config.Improve();
    }
}