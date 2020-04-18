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
    public bool IsDashing { get; set; } = false;
    private int curJumpOnAir = 0;
    private int curDashOnAir = 0;

    private float chronoDash;

    private Vector2 lastMovementInput;

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
            curDashOnAir = 0;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector2 dirVector = new Vector2(x, z).normalized;
        Vector3 move = (transform.right * dirVector.x + transform.forward * dirVector.y);

        if ((dirVector.x>0.1f||dirVector.x<-0.1f) || (dirVector.y>0.1f||dirVector.y<-0.1f) && !IsDashing)
        {
            lastMovementInput = dirVector;
        }
        if (IsDashing)
        {
            move = (transform.right * lastMovementInput.x + transform.forward * lastMovementInput.y)* settings.dashSpeed;
            chronoDash -= Time.deltaTime;
            if(chronoDash<=0)
            {
                IsDashing = false;
            }
        }

        if (Input.GetButtonDown("Fire2") && curDashOnAir < settings.dashNumber)
        {
            IsDashing = true;
            Camera.main.GetComponent<CameraManager>().ApplyFOVEffect(settings.dashFOV, settings.dashFOVDecreaseSpeed);
            Camera.main.GetComponent<CameraManager>().ApplyVignetteEffect(settings.dashVignette, settings.dashVignetteDecreaseSpeed);
            curDashOnAir++;
            chronoDash = settings.dashDuration;
        }
        if (!IsGrounded && !IsDashing)
        {
            move *= settings.airControl;
        }
        controller.Move(move * settings.speed * Time.deltaTime);

        Vector3 velocityOnJump = Vector3.zero;
        float curGravity =Input.GetButton("Jump") ? settings.gravity*0.8f : settings.gravity;
        if(Input.GetButtonDown("Jump") && IsGrounded)
        {
            //first jump
            velocityOnJump = velocity;
            velocity.y = Mathf.Sqrt(settings.jumpHeight * -2f * curGravity);
        }
        else if(Input.GetButtonDown("Jump") && curJumpOnAir<settings.multipleJumpNumber)
        {
            //multiple jump
            curJumpOnAir++;
            velocity.y = Mathf.Sqrt(settings.jumpHeight * -2f * (curGravity * settings.multipleJumpHeightRatio));
        }
        if(!IsGrounded && velocityOnJump!=Vector3.zero)
        {
            velocity += velocityOnJump;
        }
        velocity.y += curGravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
