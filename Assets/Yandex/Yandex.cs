using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;

public class Yandex : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;

    private const string MasterVolume = nameof(MasterVolume);

    [DllImport("__Internal")]
    private static extern void ShowAds();

    [DllImport("__Internal")]
    private static extern void ShowRewardAds();

    public void OnShowAds()
    {
        OnStop();
        ShowAds();
    }

    public void OnShowRewardAds()
    {
        OnStop();
        ShowRewardAds();
    }

    public void OnPlay()
    {
        const float volume = 0;

        _mixer.audioMixer.SetFloat(MasterVolume, volume);
    }

    private void OnStop()
    {
        const float volume = -80;

        _mixer.audioMixer.SetFloat(MasterVolume, volume);
    }
}