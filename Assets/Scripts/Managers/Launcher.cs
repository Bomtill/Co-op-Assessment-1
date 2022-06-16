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

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("OnlineLobby");
        Debug.Log("Joined room");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. ");
        
    }
    public void BackButton()
    {
        PhotonNetwork.Disconnect();
        MySceneManager.MSMInstance.LoadNewScene(0);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
}
