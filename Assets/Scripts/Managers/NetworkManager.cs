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
    [HideInInspector]
    public PhotonView pv;
    LevelManager levelManager;
    public GameObject winnerTitle;
    public GameObject loserTitle;
    public GameObject youDiedTitle;
    public GameObject enemies;
    public GameObject restartButton;

    
    

    #region unused variables
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
    [SerializeField] Transform slowSpawnPoint;
    [SerializeField] GameObject slowPlayerPreFab;
    [HideInInspector]
    public GameObject SlowPlayerGetter { get { return slowPlayerPreFab; } }
    #endregion

    
    //public GameObject thisPlayerGetter { get { return thisPlayer; } }

    [SerializeField] Transform fastSpawnPoint;
    [SerializeField] GameObject fastPlayerPreFab;
    //[SerializeField] GameObject endLevelCube;
    //[SerializeField] Transform endLevelTransform;
    public static GameObject thisPlayer;
    [SerializeField] int thisPlayerID;
    GameObject otherPlayer;



    // Start is called before the first frame update
    void Start()
    {
        winnerTitle.SetActive(false);
        loserTitle.SetActive(false);
        youDiedTitle.SetActive(false);
        levelManager = GetComponent<LevelManager>();
        pv = GetComponent<PhotonView>();
        
        //PhotonNetwork.Instantiate(endLevelCube.name, endLevelTransform.transform.position, Quaternion.identity);
        
        if (PhotonNetwork.IsMasterClient)
        {
            thisPlayer = PhotonNetwork.Instantiate(fastPlayerPreFab.name, slowSpawnPoint.transform.position,Quaternion.identity);
        } else
        {
            thisPlayer = PhotonNetwork.Instantiate(fastPlayerPreFab.name, fastSpawnPoint.transform.position, Quaternion.identity);
        }
        thisPlayerID = thisPlayer.GetComponent<PhotonView>().OwnerActorNr;
        thisPlayer.layer = LayerMask.NameToLayer("Players");
    }

    public void ResetLevelData()
    {
        keyCardPickedUp = false;
        p1Seen = false;
        p1PowerActive = false;
        p2PowerActive = false;
        p2Seen = false;
    }

    public void PlayerKilled()
    {
        levelManager.GameOver();
        Cursor.visible = true;
        youDiedTitle.SetActive(true);
        // have the killed screen and then 'respawn'
        // move player to respawn point.
        
        
    }
    [PunRPC]
    void PlayerFinished(int playerID) // have bool for winner
    {
        Cursor.visible = true;
        levelManager.GameOver();
        restartButton.SetActive(false);
        enemies.SetActive(false);
        levelManager.Invoke("ExitToMainButton",5.0f);
        if(playerID == thisPlayerID) //PhotonNetwork.LocalPlayer.ActorNumber
        {
            winnerTitle.SetActive(true);
            // this player is the winner
        } else
        {
            loserTitle.SetActive(true);
            // another player is the winner
        }

        // have the other players finish and this player win
    }
    public void Restart()
    {
        Cursor.visible = false;
        youDiedTitle.SetActive(false);
        thisPlayer.transform.position = fastSpawnPoint.transform.position;
    }

    
}

