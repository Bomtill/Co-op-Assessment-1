using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EndLevelTrigger : MonoBehaviourPunCallbacks
{
    public static event Action FinishedLevelEvent;
    PhotonView pv;
    public NetworkManager networkManager;
    [SerializeField] static GameObject localPlayerPrefab;
    [SerializeField] int localPlayerID;
    

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        
        Invoke("GetLocalPlayer", 0.6f);
    }

    int playersInZone = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            networkManager.pv.RPC("PlayerFinished", RpcTarget.All, localPlayerID);
            
        }
    }
    private void GetLocalPlayer()
    {
        localPlayerPrefab = GameObject.Find("TopDownWalker_Fast(Clone)");
        localPlayerID = localPlayerPrefab.GetComponent<PhotonView>().OwnerActorNr;
        //PhotonNetwork.LocalPlayer.ActorNumber;
        //localPlayerPrefab.GetComponent<PhotonView>().ViewID;
    }
}
