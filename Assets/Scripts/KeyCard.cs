using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour, IInteractable
{
    bool playerInRange = false;
    public GameObject textPopUp;

    public void Start()
    {
        textPopUp.SetActive(false);
    }
    public void Interact()
    {
        if (playerInRange)
        {
            LevelManager.keyPickedUp = true;
        } return;
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
}
