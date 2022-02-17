using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Control : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per framew
    void Update()
    {
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector3.back * moveSpeed);
        }
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector3.left * moveSpeed);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector3.forward * moveSpeed);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector3.right * moveSpeed);
        }
        
    }
}
