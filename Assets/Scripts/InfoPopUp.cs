using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUp : MonoBehaviour
{
    [SerializeField] GameObject textPopUp;
    BoxCollider triggerZone;

    public bool playerInZone;

    // Start is called before the first frame update
    void Start()
    {
        textPopUp.SetActive(false);
        triggerZone = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // maybe put in while loop so both players can be in it.

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInZone = true;
            textPopUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            textPopUp.SetActive(false);
        }
    }


}
