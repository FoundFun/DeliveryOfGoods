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
        _musicOffButton.gameObject.SetActive(false);
        _musicOffButton.interactable = false;
        _musicOnButton.gameObject.SetActive(true);
        _musicOnButton.interactable = true;
        MusicOnButtonClick?.Invoke();
    }

    private void OnMusicOffButtonClick()
    {
        _musicOnButton.gameObject.SetActive(false);
        _musicOnButton.interactable = false;
        _musicOffButton.gameObject.SetActive(true);
        _musicOffButton.interactable = true;
        MusicOffButtonClick?.Invoke();
    }

    private void OnSoundOnButtonClick()
    {
        _soundOffButton.gameObject.SetActive(false);
        _soundOffButton.interactable = false;
        _soundOnButton.gameObject.SetActive(true);
        _soundOnButton.interactable = true;
        SoundOnButtonClick?.Invoke();
    }

    private void OnSoundOffButtonClick()
    {
        _soundOnButton.gameObject.SetActive(false);
        _soundOnButton.interactable = false;
        _soundOffButton.gameObject.SetActive(true);
        _soundOffButton.interactable = true;
        SoundOffButtonClick?.Invoke();
    }
}