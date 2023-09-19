using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private TMP_Text _scoreInBodyText;
    [SerializeField] private TMP_Text _scoreTargetText;
    [SerializeField] private TMP_Text _tutorial;

    private const string Slash = "/";

    private Coroutine _coroutine;
    private bool _isTutorial;

    public event Action ExitButtonClick;
    public event Action LoadNextLevel;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _nextLevelButton.onClick.AddListener(OnLoadNextLevel);
        _tutorialButton.onClick.AddListener(EnableTutorial);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _nextLevelButton.onClick.RemoveListener(OnLoadNextLevel);
        _tutorialButton.onClick.RemoveListener(EnableTutorial);
    }

    public void Init()
    {
        _scoreInBodyText.text = 0.ToString();
    }

    public void AddScore(int score)
    {
        _scoreInBodyText.text = score.ToString();
    }

    public void SetTargetScore(int score)
    {
        _scoreTargetText.text = Slash + score;
    }

    public void EnableNextLevelButton()
    {
        const float animationTime = 0.5f;

        _nextLevelButton.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();
    }

    public void DisableNextButton()
    {
        const float animationTime = 0.5f;

        _nextLevelButton.transform.LeanScale(Vector3.zero, animationTime).setEaseOutExpo();
    }

    private void EnableTutorial()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (!_isTutorial)
            _coroutine = StartCoroutine(OnEnableTutorial());
        else
            DisableTutorial();
    }

    private void DisableTutorial()
    {
        const float animationTime = 0.2f;
        
        _tutorial.transform.LeanScale(Vector3.zero, animationTime);
        
        _isTutorial = false;
    }

    private IEnumerator OnEnableTutorial()
    {
        const float delay = 2;
        const float animationTime = 0.2f;

        _isTutorial = true;
        _tutorial.gameObject.LeanScale(Vector3.one, animationTime);

        yield return new WaitForSeconds(delay);

        DisableTutorial();
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }

    private void OnLoadNextLevel()
    {
        LoadNextLevel?.Invoke();
    }
}