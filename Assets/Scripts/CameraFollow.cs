using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    public Transform topCameraPoint;
    public Transform bottomCameraPoint;
    //Vector3 cameraTransform;
    
    float playersDistance, cameraDistance;

    private void Start()
    {
        Invoke("FindPlayers",1.0f);
        //player1 = GameObject.Find("TopDownWalker_Fast");
        //player2 = GameObject.Find("TopDownWalker_Slow");
        // get players by their prefab name, also have check if Gameobject is =!null then recheck
    }

    // Update is called once per frame
    void Update()
    {
        
        playersDistance = Vector3.Distance(player1.transform.position, player2.transform.position);
        Mathf.Clamp(playersDistance, 0, 1);

        // move the camera up and down on the Y axis depending on d

        transform.position = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);

        if (player1 != null) { FindPlayers(); }
        if (player2 != null) { FindPlayers(); }

    }
    void FindPlayers()
    {
        player1 = NetworkManager.NWInstance.FastPlayerGetter;
        player2 = NetworkManager.NWInstance.SlowPlayerGetter;
    }
}
