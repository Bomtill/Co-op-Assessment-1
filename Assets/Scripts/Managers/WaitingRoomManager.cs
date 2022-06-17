using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomManager : MonoBehaviourPunCallbacks
{
    public TMP_Text roomName;
    
    public GameObject startButton;
    public GameObject hostText;
    public GameObject clientText;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(false);
        hostText.SetActive(false);
        clientText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            hostText.SetActive(true);
            
        } else clientText.SetActive(true);


    }
    
    public void StartButton()
    {
        PhotonNetwork.LoadLevel("Level_1");
    }
}
