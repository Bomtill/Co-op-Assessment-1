using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSwitch : MonoBehaviour , IInteractable
{
    MeshRenderer lightSwitch;
    bool lightsAreOn = true;
    InfoPopUp popUp;

    // Start is called before the first frame update
    void Start()
    {
        lightSwitch = GetComponentInChildren<MeshRenderer>();
        lightSwitch.material.color = Color.green;
        popUp = GetComponent<InfoPopUp>();
    }

    public void Interact()
    {
        if (lightsAreOn)
        {
            TurnLightsOff();
        }
        if (!lightsAreOn)
        {
            TurnLightsOn();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnLightsOff()
    {
        lightSwitch.material.color = Color.red;
    }

    public void TurnLightsOn()
    {
        lightSwitch.material.color = Color.green;
    }
}
