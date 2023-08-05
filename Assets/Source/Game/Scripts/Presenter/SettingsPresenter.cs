using System;
using UnityEngine;

public class SettingsPresenter : ScreenPresenter
{
    [SerializeField] private SettingsView _settingsView;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _settingsView.ExitButtonClick += OnCloseButtonClick;
        _settingsView.MusicOnButtonClick += OnMusicOnButtonClick;
        _settingsView.MusicOffButtonClick += OnMusicOffButtonClick;
        _settingsView.SoundOnButtonClick += OnSoundOnButtonClick;
        _settingsView.SoundOffButtonClick += OnSoundOffButtonClick;
    }

    private void OnDisable()
    {
        _settingsView.ExitButtonClick -= OnCloseButtonClick;
        _settingsView.MusicOnButtonClick -= OnMusicOnButtonClick;
        _settingsView.MusicOffButtonClick -= OnMusicOffButtonClick;
        _settingsView.SoundOnButtonClick -= OnSoundOnButtonClick;
        _settingsView.SoundOffButtonClick -= OnSoundOffButtonClick;
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }

    private void OnMusicOnButtonClick()
    {

    }

    private void OnMusicOffButtonClick()
    {

    }

    private void OnSoundOnButtonClick()
    {

    }

    private void OnSoundOffButtonClick()
    {

    }
}