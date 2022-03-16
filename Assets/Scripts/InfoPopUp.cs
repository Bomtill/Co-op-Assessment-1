using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUp : MonoBehaviour
{
    //[SerializeField] GameObject textPopUp;
    BoxCollider triggerZone;
    Canvas canvas;
    public Text text;
    //[SerializeField] Text infoText;
    public string textToShow = "Put Text Here";

    public bool playerInZone;

    // Start is called before the first frame update
    void Start()
    {
        
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.enabled = false;
        triggerZone = GetComponent<BoxCollider>();
        text = GetComponentInChildren<Text>();
        text.text = textToShow;
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
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            canvas.enabled = false;
        }
    }


}
