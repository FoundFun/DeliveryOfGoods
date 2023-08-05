using System.Collections.Generic;
using UnityEngine;

public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private DeliverPoint _deliverPoint;

    private Vector3 _startPosition;

    private List<BoxPresenter> _boxsInBody = new List<BoxPresenter>();

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxPresenter box))
        {
            _boxsInBody.Add(box);
            box.Complete();

            if (_boxsInBody.Count >= 3)
                Deliver(_deliverPoint);
        }

        if (other.TryGetComponent(out DeliverPoint deliverPoint))
        {
            Debug.Log("Deliver");
            CleanBody();
            ArriveToLoad();
        }
    }

    private void Deliver(DeliverPoint deliverPoint)
    {
        Debug.Log("Move");
        transform.LeanMoveZ(deliverPoint.transform.position.z, 3);
    }

    private void ArriveToLoad()
    {
        transform.LeanMoveLocalZ(_startPosition.z, 3);
    }

    private void CleanBody()
    {
        for (int i = 0; i < _boxsInBody.Count; i++)
        {
            _boxsInBody[i].Return();

            _boxsInBody.Remove(_boxsInBody[i]);
        }
    }
}