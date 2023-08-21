using System;
using UnityEngine;
using System.Linq;
using System.Collections;

public class GamePresenter : ScreenPresenter
{
    [SerializeField] private HeartPresenter[] _hearts;
    [SerializeField] private GameView _gameView;
    [SerializeField] private SpawnerBox _spawnerBox;
    [SerializeField] private WerehousePresenter _werehousePresenter;
    [SerializeField] private BordResurrectPresenter _bordResurrect;
    [SerializeField] private BordSkipPresenter _bordSkip;

    private Coroutine _coroutine;
    private int _numberHeart;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _gameView.ExitButtonClick += OnCloseButtonClick;
        _werehousePresenter.BoxFallen += OnBoxFallen;
    }

    private void OnDisable()
    {
        _gameView.ExitButtonClick -= OnCloseButtonClick;
        _werehousePresenter.BoxFallen -= OnBoxFallen;
    }

    public void Init()
    {
        _numberHeart = _hearts.Where(heart => heart.gameObject.activeSelf == true).Count();
        _bordResurrect.gameObject.SetActive(false);
        _bordSkip.gameObject.SetActive(false);
    }

    protected override void OpenScreen()
    {
        _spawnerBox.Active();
        base.OpenScreen();
    }

    protected override void CloseScreen()
    {
        _spawnerBox.Inactive();
        base.CloseScreen();
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }

    private void OnBoxFallen()
    {
        HeartPresenter lastHeart = _hearts.LastOrDefault(heart => heart.gameObject.activeSelf == true);

        if (lastHeart != null)
        {
            lastHeart.gameObject.SetActive(false);
            _numberHeart--;
        }

        if (_numberHeart <= 0)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(EnableGameOverBord());
        }
    }

    private IEnumerator EnableGameOverBord()
    {
        const float Delay = 4;

        _bordResurrect.gameObject.SetActive(true);

        yield return new WaitForSeconds(Delay);

        _bordSkip.gameObject.SetActive(true);
    }
}