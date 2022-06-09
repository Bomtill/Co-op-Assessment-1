using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    PhotonView pv;
    [Header("global variables")]
    // scene manager
    // game paused

    [Header("Level Variables")]
    public bool keyCardPickedUp = false;

    [Header("player One Variables")]
    public bool p1Seen = false;
    public bool p1PowerActive = false;

    [Header("player Two Variables")]
    public bool p2Seen = false;
    public bool p2PowerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetLevelData()
    {
        keyCardPickedUp = false;
        p1Seen = false;
        p1PowerActive = false;
        p2PowerActive = false;
        p2Seen = false;
    }
}

