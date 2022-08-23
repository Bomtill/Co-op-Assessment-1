using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextInfoPopUp : MonoBehaviour
{
    public GameObject textPopUp;
    // Start is called before the first frame update
    void Start()
    {
        //textPopUp = GetComponentInChildren<GameObject>();
        textPopUp.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(false);
        }
    }
}
