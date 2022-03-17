using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSwitch : MonoBehaviour , IInteractable
{
    [SerializeField] List<Light> lights = new List<Light>();
    [SerializeField] bool lightsAreOn = true;
    [SerializeField] GameObject textPopUp;

    MeshRenderer lightSwitch;
    public bool playerInRange = false;

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
