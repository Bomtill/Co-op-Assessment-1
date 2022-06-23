using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player1;
    //[SerializeField] GameObject player2;

    public Transform topCameraPoint;
    public Transform bottomCameraPoint;
    //Vector3 cameraTransform;
    
    float playersDistance, cameraDistance;

    private void Start()
    {
        Invoke("FindPlayers",0.5f);
        player1 = GameObject.Find("TopDownWalker_Fast(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player1 != null) { FindPlayers(); }

        if (player1) { transform.position = player1.transform.position; }
        
    }
    void FindPlayers()
    {
        player1 = GameObject.Find("TopDownWalker_Fast(Clone)");
    }
}
