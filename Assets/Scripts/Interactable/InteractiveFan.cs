using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveFan : MonoBehaviour
{
    Animator anim;

    BoxCollider fanCollider;

    bool doorISOpen = false;
    public void Start()
    {
        anim = GetComponent<Animator>();
        fanCollider = GetComponent<BoxCollider>();
    }

    public void FanStart()
    {
        anim.speed = 1f;
        fanCollider.enabled = true;
    }

    public void FanStop()
    {
        anim.speed = 0f;
        fanCollider.enabled = false;
    }
    private void PauseTimeActive()
    {
        FanStop();
    }
    private void PauseTimeInactive()
    {    
        FanStart();
    }

    private void OnEnable()
    {
        PlayerPowers.PauseTimeEvent += PauseTimeActive;
        PlayerPowers.RestartTimeEvent += PauseTimeInactive;
    }
    private void OnDisable()
    {
        PlayerPowers.PauseTimeEvent -= PauseTimeActive;
        PlayerPowers.RestartTimeEvent -= PauseTimeInactive;
    }
}
