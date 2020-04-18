using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField]
    private PlayerSettings settings;

    Vector3 velocity;

    [SerializeField]
    private Transform groundCheck;
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;
    public bool IsGrounded { get; set; }
    private int curJumpOnAir = 0;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(IsGrounded)
        {
            curJumpOnAir = 0;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z);
        if(!IsGrounded)
        {
            move *= settings.airControl;
        }
        controller.Move(move * settings.speed * Time.deltaTime);

        Vector3 velocityOnJump = Vector3.zero;
        if(Input.GetButtonDown("Jump") && IsGrounded)
        {
            velocityOnJump = velocity;
            velocity.y = Mathf.Sqrt(settings.jumpHeight * -2f * settings.gravity);
        }
        else if(Input.GetButtonDown("Jump")&&curJumpOnAir<settings.multipleJumpNumber)
        {
            curJumpOnAir++;
            velocity.y = Mathf.Sqrt(settings.jumpHeight * -2f * settings.gravity);
        }
        if(!IsGrounded&&velocityOnJump!=Vector3.zero)
        {
            velocity += velocityOnJump;
        }
        velocity.y += settings.gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
