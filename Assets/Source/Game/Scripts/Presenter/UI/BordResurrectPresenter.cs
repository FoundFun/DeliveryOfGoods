using Agava.YandexGames.Samples;
using UnityEngine;

public class BordResurrectPresenter : MonoBehaviour
{
    [SerializeField] private BordResurrectView _view;
    [SerializeField] private PlaytestingCanvas _canvas;
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
        _canvas.OnShowVideoButtonClick();
        _heartPresenter.Recover();
    }
}