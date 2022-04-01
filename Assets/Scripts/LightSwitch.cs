using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSwitch : MonoBehaviour , IInteractable
{
    [SerializeField] List<Light> lights = new List<Light>();
    [SerializeField] bool lightsAreOn = true;
    [SerializeField] GameObject textPopUp;
    MeshRenderer lightSwitch;
    [SerializeField] bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        lightSwitch = GetComponentInChildren<MeshRenderer>();
        lightSwitch.material.color = Color.green;
        //textPopUp = GetComponentInChildren<InfoPopUp>();

        textPopUp.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerInRange = true;
            textPopUp.SetActive(true);
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            textPopUp.SetActive(false);
        }
    }
    
    private void OnEnable()
    {
        CMF.PlayerActivate.InteractEvent += Interact;
    }
    private void OnDisable()
    {
        CMF.PlayerActivate.InteractEvent -= Interact;
    }
    
    public void Interact()
    {
        if (playerInRange)
        {
            if (lightsAreOn)
            {
                TurnLightsOff();
                
            }
            else if (!lightsAreOn)
            {
                TurnLightsOn();
            }
            lightsAreOn = !lightsAreOn;
        } else return;
        
    }
    

    public void TurnLightsOff()
    {
        //lightsAreOn = false;
        lightSwitch.material.color = Color.red;
        foreach (Light i in lights)
        {
            i.enabled = false;
        }
    }

    public void TurnLightsOn()
    {
        //lightsAreOn = true;
        lightSwitch.material.color = Color.green;
        foreach (Light i in lights)
        {
            i.enabled = true;
        }
    }
}
