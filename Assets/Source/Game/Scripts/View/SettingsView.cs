using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _musicOnButton;
    [SerializeField] private Button _musicOffButton;
    [SerializeField] private Button _soundOnButton;
    [SerializeField] private Button _soundOffButton;

    public event Action ExitButtonClick;
    public event Action MusicOnButtonClick;
    public event Action MusicOffButtonClick;
    public event Action SoundOnButtonClick;
    public event Action SoundOffButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _musicOnButton.onClick.AddListener(OnMusicOnButtonClick);
        _musicOffButton.onClick.AddListener(OnMusicOffButtonClick);
        _soundOnButton.onClick.AddListener(OnSoundOnButtonClick);
        _soundOffButton.onClick.AddListener(OnSoundOffButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _musicOnButton.onClick.RemoveListener(OnMusicOnButtonClick);
        _musicOffButton.onClick.RemoveListener(OnMusicOffButtonClick);
        _soundOnButton.onClick.RemoveListener(OnSoundOnButtonClick);
        _soundOffButton.onClick.RemoveListener(OnSoundOffButtonClick);
    }
    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }

    private void OnMusicOnButtonClick()
    {
        ChangeButton(_soundOffButton, _musicOnButton);
        MusicOnButtonClick?.Invoke();
    }

    private void OnMusicOffButtonClick()
    {
        ChangeButton(_musicOnButton, _musicOffButton);
        MusicOffButtonClick?.Invoke();
    }

    private void OnSoundOnButtonClick()
    {
        ChangeButton(_soundOffButton, _soundOnButton);
        SoundOnButtonClick?.Invoke();
    }

    private void OnSoundOffButtonClick()
    {
        ChangeButton(_soundOnButton, _soundOffButton);
        SoundOffButtonClick?.Invoke();
    }

    private void ChangeButton(Button buttonToDisable, Button buttonToEnable)
    {
        buttonToDisable.gameObject.SetActive(false);
        buttonToDisable.interactable = false;
        buttonToEnable.gameObject.SetActive(true);
        buttonToEnable.interactable = true;
    }
}