using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightSwitch : MonoBehaviour , IInteractable
{
    [SerializeField] List<Light> lights = new List<Light>();
    [SerializeField] bool lightsAreOn = true;

    MeshRenderer lightSwitch;
    bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        lightSwitch = GetComponentInChildren<MeshRenderer>();
        lightSwitch.material.color = Color.green;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        } else playerInRange = false;
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
        foreach (Light i in lights)
        {
            i.enabled = true;
        }
    }

    public void TurnLightsOn()
    {
        lightSwitch.material.color = Color.green;
        foreach (Light i in lights)
        {
            i.enabled = false;
        }
    }
}
