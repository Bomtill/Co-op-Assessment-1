using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl : MonoBehaviour,IInteractable
{
    public GameObject textPopUp;
    public GameObject objectRef;
    [SerializeField] bool playerInRange = false;
    bool fanIsWorking = false;


    public void Start()
    {
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
            if (fanIsWorking)
            {
                objectRef.GetComponent<InteractiveFan>().FanStop();

            }
            else if (!fanIsWorking)
            {
                objectRef.GetComponent<InteractiveFan>().FanStart();
            }
            fanIsWorking = !fanIsWorking;
        }
        else return;

    }



}
