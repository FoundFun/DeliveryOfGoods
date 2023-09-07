using DeliveryOfGoods.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private TMP_Text _scoreInBodyText;
    [SerializeField] private TMP_Text _scoreTargetText;

    private const string Slash = "/";

    public event Action ExitButtonClick;
    public event Action LoadNextLevel;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _nextLevelButton.onClick.AddListener(OnLoadNextLevel);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _nextLevelButton.onClick.RemoveListener(OnLoadNextLevel);
    }

    public void Init()
    {
        _scoreInBodyText.text = 0.ToString();
        _scoreTargetText.text = Slash + Config.CurrentDeliverBox.ToString();
    }

    public void AddScore(int score)
    {
        _scoreInBodyText.text = score.ToString();
    }

    public void EnableNextButton()
    {
        const float animationTime = 0.5f;

        _nextLevelButton.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();
    }

    public void DisableNextButton()
    {
        const float animationTime = 0.5f;

        _nextLevelButton.transform.LeanScale(Vector3.zero, animationTime).setEaseOutExpo();
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