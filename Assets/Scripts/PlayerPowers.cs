using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowers : MonoBehaviour
{
    public static event Action PauseTimeEvent;
    public static event Action RestartTimeEvent;


    public Image fillImage;
    private Slider powerBar;
    // will get rid of once there is only 1 mesh
    public SkinnedMeshRenderer player1;
    public SkinnedMeshRenderer player1Body;
    public SkinnedMeshRenderer player2;
    public SkinnedMeshRenderer player2Body;

    public GameObject otherPlayer;
    public Material bodyDefault;
    public Material bodyTranspartent;

    public bool isPlayer1 = true;

    public bool powerIsActve = false;
    [Header("Powers, Only one active at a time")]
    public bool invisibility = false;
    public bool stopTime = false;
    public bool reverseTime = false;
    public bool confuse = false;
    public bool wallClip = false;
    public bool distract = false;

    //could have an enum for different powers? and changes which one is active through the inspector.
    
    public float maxPowerAmount = 5;
    public float currentPowerAmount;

    private void Start()
    {
        powerBar = GetComponentInChildren<Slider>();
        currentPowerAmount = maxPowerAmount;
        powerBar.maxValue = maxPowerAmount;
    }

    private void Update()
    {
        powerBar.value = currentPowerAmount;
    }
    public void ActivatePower()
    {
        Debug.Log("Activate power has been called");
        powerIsActve = true;
        if (invisibility)
        {
            StartCoroutine(PowerCountDown(currentPowerAmount));
            gameObject.layer = LayerMask.NameToLayer("Default");
            otherPlayer.layer = LayerMask.NameToLayer("Default");
            player1.material = bodyTranspartent;
            player1Body.material = bodyTranspartent;
            player2.material = bodyTranspartent;
            player2Body.material = bodyTranspartent;
            
        }
        if (stopTime)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            otherPlayer.layer = LayerMask.NameToLayer("Default");
            StartCoroutine(PowerCountDown(currentPowerAmount));
            PauseTimeEvent?.Invoke();
        }
    }

    public void DeactivatePower()
    {
        powerIsActve = false;

        //StopCoroutine(PowerCountDown(currentPowerAmount));
        if (invisibility)
        {
            gameObject.layer = LayerMask.NameToLayer("Players");
            otherPlayer.layer = LayerMask.NameToLayer("Players");
            player1.material = bodyDefault;
            player1Body.material = bodyDefault;
            player2.material = bodyDefault;
            player2Body.material = bodyDefault;
            StartCoroutine(PowerRecharge(currentPowerAmount));
        }
        if (stopTime)
        {
            gameObject.layer = LayerMask.NameToLayer("Players");
            otherPlayer.layer = LayerMask.NameToLayer("Players");
            RestartTimeEvent?.Invoke();
            StartCoroutine(PowerRecharge(currentPowerAmount));
        }
        // set everthing back to defaults.
    }

    IEnumerator PowerCountDown(float currentPower) //WaitForSecondsRealtime countDownTimer
    {
        while (currentPower >= 0f && powerIsActve)
        {
            currentPower -= Time.deltaTime;
            currentPowerAmount = currentPower;
            yield return null;
        }
        DeactivatePower();
        //Debug.Log("End Couroutine");
        yield return null;
    }
    IEnumerator PowerRecharge(float currentPower)
    {
        while (currentPower <= maxPowerAmount && !powerIsActve)
        {
            currentPower += Time.deltaTime * 2;
            currentPowerAmount = currentPower;
            yield return null;
        }
        yield return null;
    }
}
