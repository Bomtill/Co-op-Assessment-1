using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Control : MonoBehaviour
{
    [Header("Slow Player")]
    [SerializeField] float moveAcceleration = 15f;
    [SerializeField] float maxSpeed = 5f;

    Rigidbody rb;
    Vector3 lastPosition;
    //GameObject capsule;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //capsule = GetComponentInChildren<GameObject>();
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude >= maxSpeed) return;

        lastPosition = transform.position;

        if (Input.GetKey("a")) rb.AddForce(moveAcceleration * Vector3.back);
        if (Input.GetKey("w")) rb.AddForce(moveAcceleration * Vector3.left);
        if (Input.GetKey("d")) rb.AddForce(moveAcceleration * Vector3.forward);
        if (Input.GetKey("s")) rb.AddForce(moveAcceleration * Vector3.right);

        rb.MoveRotation(Quaternion.LookRotation(rb.velocity,Vector3.up));
        
    }
    
}
