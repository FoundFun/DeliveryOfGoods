using UnityEngine;

public class BordResurrectPresenter : MonoBehaviour
{
    [SerializeField] private BordHeartPresenter _heartPresenter;

    public void OnResurrect()
    {
        _heartPresenter.Recover();
    }
}