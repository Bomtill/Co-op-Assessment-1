using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveFan : MonoBehaviour
{
    Animator anim;

    BoxCollider fanCollider;

    bool powerOff = false;
    public void Start()
    {
        anim = GetComponent<Animator>();
        fanCollider = GetComponent<BoxCollider>();
    }

    private void FanStart()
    {
        anim.speed = 1f;
        fanCollider.enabled = true;
    }

    private void FanStop()
    {
        anim.speed = 0f;
        fanCollider.enabled = false;
    }

    public void SetPowerOff() 
    {
        powerOff = true;
        FanStop();
    }

    public void SetPowerOn() 
    {
        powerOff = false;
        FanStart();
    }
    private void PauseTimeActive()
    {
        if (powerOff)
        {
            return;
        }
        else
        {
            FanStop();
        }
    }
    private void PauseTimeInactive()
    {
        if (powerOff)
        {
            return;
        }
        else
        {
            FanStart();
        }
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
