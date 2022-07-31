using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public static event Action FinishedLevelEvent;
    public string nextLevel;

    int playersInZone = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInZone ++;
            if (playersInZone == 2)
            {
                FinishedLevelEvent?.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInZone --;
        }
    }
}
