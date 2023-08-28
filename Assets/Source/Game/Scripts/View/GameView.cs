using DeliveryOfGoods.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _scoreInBodyText;
    [SerializeField] private TMP_Text _scoreTargetText;

    private const string Slash = "/";

    public event Action ExitButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    public void Init()
    {
        _scoreInBodyText.text = 0.ToString();
        _scoreTargetText.text = Slash + Config.CurrentDeliverBoxs.ToString();
    }

    public void AddScore(int score)
    {
        _scoreInBodyText.text = score.ToString();
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }
}