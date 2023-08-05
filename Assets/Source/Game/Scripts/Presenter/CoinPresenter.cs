using UnityEngine;

public class CoinPresenter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxPresenter box))
        {
            gameObject.SetActive(false);
        }
    }
}