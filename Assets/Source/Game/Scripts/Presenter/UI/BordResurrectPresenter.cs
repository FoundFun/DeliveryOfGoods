using UnityEngine;

public class BordResurrectPresenter : MonoBehaviour
{
    [SerializeField] private BordResurrectView _view;
    [SerializeField] private BordHeartPresenter _heartPresenter;

    private void OnEnable()
    {
        _view.Resurrected += OnResurrect;
    }

    private void OnDisable()
    {
        _view.Resurrected -= OnResurrect;
    }

    public void OnResurrect()
    {
        _heartPresenter.Recover();
    }
}