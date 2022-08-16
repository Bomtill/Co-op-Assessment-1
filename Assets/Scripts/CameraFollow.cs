using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    //Vector3 cameraTransform;
    
    float playersDistance, cameraDistance;

    // Update is called once per frame
    void Update()
    {
        
        playersDistance = Vector3.Distance(player1.position, player2.position);
        Mathf.Clamp(playersDistance, 0, 1);

        // move the camera up and down on the Y axis depending on d

        transform.position = Vector3.Lerp(player1.position, player2.position, 0.5f);


    }
}
