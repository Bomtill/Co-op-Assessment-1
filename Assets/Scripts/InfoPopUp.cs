using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPopUp : MonoBehaviour
{
    TMP_Text infoText;
    public string textToShow = "Put Text Here";
    Transform mainCamera;
    void Start()
    {
        mainCamera = Camera.main.transform;
        infoText = GetComponentInChildren<TMP_Text>();
        infoText.SetText(textToShow);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(gameObject.transform.position - mainCamera.position, mainCamera.up);
        
    }
}
