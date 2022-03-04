using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterControl : MonoBehaviour
{
    [Tooltip("Movement Values")]
    [SerializeField] float speedMultiplier, rotationSpeed, gravityForce, jumpForce;

    //Components
    CharacterController cc;
    Animator anim;

    Vector3 movementDirection;
    Vector3 playerVelocity;
    bool playerIsGrounded;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerIsGrounded = cc.isGrounded;
        if(playerIsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        var xMovement = Input.GetAxis("Horizontal");
        var zMovement = Input.GetAxis("Vertical");

        if(xMovement != 0||zMovement != 0)
        {
            movementDirection.Set(xMovement, 0, zMovement);
            cc.Move(movementDirection * speedMultiplier * Time.deltaTime);
            anim.SetBool("HasInput", true);

        } else { anim.SetBool("HasInput", false); }

        // could change the disired direction to follow the mouse position. for a top down shooter
        // have to pass in the mouse position without a y axis
        var desiredDirection = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredDirection, rotationSpeed * Time.deltaTime);

        var animationVector = transform.InverseTransformDirection(cc.velocity);

        anim.SetFloat("ForwardSpeed", animationVector.z);
        anim.SetFloat("StrafeSpeed", animationVector.x);
        ProcessGravtiy();
    }
    public void ProcessGravtiy()
    {
        if(Input.GetButtonDown("Jump") && playerIsGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityForce);
        }
        playerVelocity.y += gravityForce * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }
}
