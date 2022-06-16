using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Singleton
    private static NetworkManager localInstance;
    public static NetworkManager NWInstance
    {
        get {
            if (localInstance == null)
            {
                Debug.LogError("NetworkManager instance is null!");
            }
            return localInstance;
        }
    }
    #endregion
    PhotonView pv;
    [Header("global variables")]
    // scene manager
    // game paused

    [Header("Level Variables")]
    public bool keyCardPickedUp = false;
    private const byte KEYCARDEVENT = 0;

    [Header("player One Variables")]
    public bool p1Seen = false;
    private const byte P1SEENEVENT = 0;
    public bool p1PowerActive = false;
    private const byte INVISPOWEREVENT = 0;

    [Header("player Two Variables")]
    public bool p2Seen = false;
    private const byte P2SEENEVENT = 0;
    public bool p2PowerActive = false;
    private const byte STOPTIMEPOWEREVENT = 0;

    [SerializeField] Transform fastSpawnPoint;
    [SerializeField] Transform slowSpawnPoint;

    [SerializeField] GameObject fastPlayerPreFab;
    [SerializeField] GameObject slowPlayerPreFab;

    [HideInInspector]
    public GameObject SlowPlayerGetter { get { return slowPlayerPreFab; } }
    [HideInInspector]
    public GameObject FastPlayerGetter { get { return fastPlayerPreFab; } }

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        //PhotonNetwork.Instantiate(fastPlayerPreFab.name, fastSpawnPoint.transform.position, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(fastPlayerPreFab.name, fastSpawnPoint.transform.position,Quaternion.identity);
        } else
        {
            PhotonNetwork.Instantiate(fastPlayerPreFab.name, slowSpawnPoint.transform.position, Quaternion.identity);
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

