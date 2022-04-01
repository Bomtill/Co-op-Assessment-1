using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{

    public GameObject textPopUp;
    Animator anim;
    //public bool isDoorUnlocked = LevelManager.keyPickedUp;

    bool doorISOpen = false;
    public void Start()
    {
        textPopUp.SetActive(false);
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LevelManager.keyPickedUp == false)
            {
                textPopUp.SetActive(true);
            } else if (!doorISOpen) OpenDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
            textPopUp.SetActive(false);
        }
    }
    private void OpenDoor()
    {
        Debug.Log("Oped Door");
        anim.SetTrigger("OpenDoor");
        doorISOpen = true;
    }
        
    private void CloseDoor()
    {
        if (doorISOpen)
        {
            anim.SetTrigger("CloseDoor");
            doorISOpen = false;
        }
        return;
    }
}
