using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField hostGameInput;
    [SerializeField] TMP_InputField joinGameInput;

    string gameVersion = "1";
    bool isConnecting;
    //RoomOptions roomOptions;
    private int maxPlayers = 2;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(hostGameInput.text);
        Debug.Log("Created Room");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinGameInput.text);
        Debug.Log("Joined room");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("OnlineLobby");
    }
    public void BackButton()
    {
        PhotonNetwork.Disconnect();
        MySceneManager.MSMInstance.LoadNewScene(0);
    }
}
