using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUp : MonoBehaviour
{
    [SerializeField] GameObject textPopUp;
    // Start is called before the first frame update
    void Start()
    {
        textPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            textPopUp.SetActive(true);
        }
    }
}
