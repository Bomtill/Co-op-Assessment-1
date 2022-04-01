using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    #region Graphics Settings
    public void QualitySetLow()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void QualitySetLMid()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void QualitySetHigh()
    {
        QualitySettings.SetQualityLevel(2);
    }
    #endregion
    public void FullScreenToggle(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
