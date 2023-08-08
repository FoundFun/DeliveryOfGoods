using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private DeliverPoint _endDeliverPoint;

    private int _boxsInBody;

    public bool IsDelivery { get; private set; }

    public event Action SceneChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxPresenter box))
        {
            box.Complete();
            _boxsInBody++;

            if (_boxsInBody >= 3 && IsDelivery == false)
                Deliver(_endDeliverPoint.transform.position);
        }
    }

    public void Reset()
    {
        IsDelivery = false;
        _boxsInBody = 0;
    }

    public void Move(Vector3 targetPosition)
    {
        transform.LeanMoveZ(targetPosition.z, 3);
    }

    private void Deliver(Vector3 targetPosition)
    {
        IsDelivery = true;
        transform.LeanMoveZ(targetPosition.z, 3).setOnComplete(SwitchScene);
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("Level1");
        SceneChanged?.Invoke();
    }
}