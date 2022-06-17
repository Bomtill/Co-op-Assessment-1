using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EndLevelTrigger : MonoBehaviour
{
    public static event Action FinishedLevelEvent;
    PhotonView pv;
    GameObject localPlayer;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        localPlayer = NetworkManager.thisPlayer;
    }

    int playersInZone = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pv.RPC("PlayerFinished", RpcTarget.All, other.GetComponent<PhotonView>().OwnerActorNr);
            
        }
    }
}
