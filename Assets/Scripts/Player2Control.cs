using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Control : MonoBehaviour
{
    [Tooltip("Faster Player")]
    [SerializeField] float speedMultiplier, rotationSpeed, gravityForce, jumpForce, groundCastRange;
    //[SerializeField] float moveAcceleration = 20f;
    //[SerializeField] float maxSpeed = 10f;

    CharacterController charCon;
    //Rigidbody rb;
    Animator anim;
    [SerializeField] float zMovement, xMovement = 0f;
    Vector3 moveDirection;
    bool hasInput = false;

    // Start is called before the first frame update
    void Awake()
    {
        charCon = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        //rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey("left")) { zMovement = 1; hasInput = true; }
        if (Input.GetKey("up")) { xMovement = 1; hasInput = true; }
        if (Input.GetKey("right")) { zMovement = -1; hasInput = true; }
        if (Input.GetKey("down")) { xMovement = -1; hasInput = true; }
        //if (hasInput) { zMovement = 0; xMovement = 0; hasInput = false; } // need to reset axis movement to 0

        if (hasInput)
        {
            moveDirection.Set(xMovement, 0, zMovement);
            charCon.Move(moveDirection * speedMultiplier * Time.deltaTime);
            anim.SetBool("HasInput", true);

        } else { anim.SetBool("HasInput", false); }

        var desiredDirection = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredDirection, rotationSpeed * Time.deltaTime);

        

        // character only move in one direction
        /*
        var animationVector = transform.InverseTransformDirection(rb.velocity);

        anim.SetFloat("ForwardSpeed", animationVector.z);
        anim.SetFloat("StrafeSpeed", animationVector.x);
        */
    }
    /*
    void FixedUpdate()
    {
        if (rb.velocity.magnitude >= maxSpeed) return;

        if (Input.GetKey("left"))
        {
            rb.AddForce(moveAcceleration * Vector3.back);
            
        }
        if (Input.GetKey("up"))
        {
            rb.AddForce(moveAcceleration * Vector3.left);
            
        }
        if (Input.GetKey("right"))
        {
            rb.AddForce(moveAcceleration * Vector3.forward);
            
        }
        if (Input.GetKey("down"))
        {
            rb.AddForce(moveAcceleration * Vector3.right);
            
        }
    }*/
}
