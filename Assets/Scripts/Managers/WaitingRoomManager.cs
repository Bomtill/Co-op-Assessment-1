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
    public Toggle player1Ready;
    public Toggle player2Ready;
    public GameObject startButton;

    private LayerMask p1Interact;
    private LayerMask p2Interact;

    bool p1Ready = false;
    bool p2Ready = false;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //startButton.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            //p1Interact = player1Ready.GetComponent<LayerMask>();
            // set layermask to UI
        }

    }
    /// <summary>
    /// Need to either have a check for if the player is the host
    /// or the player is joining 
    /// </summary>
    public void Player1Toggle() // need to have check is mine
    {
        p1Ready = !p1Ready;
        enableStartbutton();
    }
    public void Player2Toggle()
    {
        p2Ready = !p2Ready;
        enableStartbutton();
    }

    public void enableStartbutton()
    {
        if(p1Ready && p2Ready)
        {
            startButton.SetActive(true);
        }
    }
    public void StartButton()
    {
        PhotonNetwork.LoadLevel("Level_1");
    }
}
