using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField hostGameInput;
    [SerializeField] TMP_InputField joinGameInput;

    //RoomOptions roomOptions;
    private int maxPalyers = 2;
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(hostGameInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinGameInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("WaitingRoom");
    }
    
}
