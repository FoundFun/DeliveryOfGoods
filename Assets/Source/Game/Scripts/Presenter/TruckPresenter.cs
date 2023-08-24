using DeliveryOfGoods.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private DeliverPoint _endDeliverPoint;
    [SerializeField] private ParticleSystem _smokeExplosion;
    [SerializeField] private TMP_Text _scoreText;

    private const float AnimationTime = 3;

    private int _boxsInBody;

    public bool IsDelivery { get; private set; }

    public event Action SceneChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxPresenter box))
        {
            box.Complete();
            _boxsInBody++;

            if (_boxsInBody >= Config.CurrentDeliverBoxs && IsDelivery == false)
                Deliver(_endDeliverPoint.transform.position);
        }
    }

    public void Reset()
    {
        IsDelivery = false;
        _boxsInBody = 0;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(Config.NameScene + Config.CurrentLevel);
        _smokeExplosion.Play();
        SceneChanged?.Invoke();
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
        Config.Improve();
        SceneManager.LoadScene(Config.NameScene + Config.CurrentLevel);
        _smokeExplosion.Play();
        SceneChanged?.Invoke();
    }
}