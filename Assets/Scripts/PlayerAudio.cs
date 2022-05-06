using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip stopTimeSFX;
    public AudioClip startTimeSFX;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayStopTimeSFX()
    {
        audioSource.PlayOneShot(stopTimeSFX);
    }
    private void PlayStartTimeSFX()
    {
        audioSource.PlayOneShot(startTimeSFX);
    }
    private void OnEnable()
    {
        PlayerPowers.PauseTimeEvent += PlayStopTimeSFX;
        PlayerPowers.RestartTimeEvent += PlayStartTimeSFX;
    }
    private void OnDisable()
    {
        PlayerPowers.PauseTimeEvent -= PlayStopTimeSFX;
        PlayerPowers.RestartTimeEvent -= PlayStartTimeSFX;
    }
}
