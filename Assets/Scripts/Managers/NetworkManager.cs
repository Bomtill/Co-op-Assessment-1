using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
