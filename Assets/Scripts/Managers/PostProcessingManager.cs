using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    private Grain grainEffect;
    void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out grainEffect);

        grainEffect.enabled.Override(false);
        grainEffect.intensity.Override(1f);
    }

    private void GrainVFXActive()
    {
        grainEffect.enabled.Override(true);
    }

    private void GrainVFXDisable()
    {
        grainEffect.enabled.Override(false);
    }
    private void OnEnable()
    {
        PlayerPowers.PauseTimeEvent += GrainVFXActive;
        PlayerPowers.RestartTimeEvent += GrainVFXDisable;
    }
    private void OnDisable()
    {
        PlayerPowers.PauseTimeEvent -= GrainVFXActive;
        PlayerPowers.RestartTimeEvent -= GrainVFXDisable;
    }

}
