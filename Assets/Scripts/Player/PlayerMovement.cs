﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private MouseLook vision;
    [SerializeField]
    private PlayerSettings settings;

    private Vector3 velocity;

    private DeathTriggerZone deathZone;

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
    private float curDashCooldown = 0f;

    private Vector2 lastMovementInput;

    private float chronoSinceNotGrounded = 0f;

    private GameObject lastPlatform = null;

    [SerializeField]
    private FiletCollider filetCollider;


    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        vision = this.GetComponentInChildren<MouseLook>();
    }

    private void Start()
    {
        deathZone = DeathTriggerZone.Instance;
        deathZone.Player = this.gameObject;
    }

    private void Update()
    {
        if(GameManager.Instance.isWinning)
        {
            GravityEffect();
            return;
        }
        DashCooldown();
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(IsGrounded)
        {
            curJumpOnAir = 0;
            curDashOnAir = 0;
            chronoSinceNotGrounded = 0;
        }
        else
        {
            chronoSinceNotGrounded += Time.deltaTime;
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
            if(!settings.IsDashOnVision)
                move = (transform.right * lastMovementInput.x + transform.forward * lastMovementInput.y)* settings.dashSpeed;
            else
                move = vision.ViewForward * settings.dashSpeed;
            chronoDash -= Time.deltaTime;
            if(chronoDash<=0)
            {
                IsDashing = false;
            }
        }
        else
        {
            move *= settings.speed;
        }

        if (Input.GetButtonDown("Fire2") && curDashOnAir < settings.dashNumber && curDashCooldown<=0)
        {
            IsDashing = true;
            velocity = Vector3.zero;
            curDashCooldown = settings.dashCooldown;
            SoundManager.Instance.PlaySound(2);
            Camera.main.GetComponent<CameraManager>().ApplyFOVEffect(settings.dashFOV, settings.dashFOVDecreaseSpeed);
            Camera.main.GetComponent<CameraManager>().ApplyVignetteEffect(settings.dashVignette, settings.dashVignetteDecreaseSpeed);
            curDashOnAir++;
            chronoDash = settings.dashDuration;
        }
        if (!IsGrounded && !IsDashing)
        {
            move *= settings.airControl;
        }
        controller.Move(move * Time.deltaTime);

        Vector3 velocityOnJump = Vector3.zero;
        float curGravity =Input.GetButton("Jump") ? settings.gravity*0.8f : settings.gravity;
        if(Input.GetButtonDown("Jump") && (IsGrounded ||chronoSinceNotGrounded<settings.coyoteTime))
        {
            //first jump
            velocity = Vector3.zero;
            velocityOnJump = velocity;
            SoundManager.Instance.PlaySound(3);
            velocity.y = Mathf.Sqrt(settings.jumpHeight * -2f * curGravity);
        }
        else if(Input.GetButtonDown("Jump") && curJumpOnAir<settings.multipleJumpNumber)
        {
            //multiple jump
            velocity = Vector3.zero;
            SoundManager.Instance.PlaySound(6);
            curJumpOnAir++;
            velocity.y = Mathf.Sqrt(settings.jumpHeight * -2f * (curGravity * settings.multipleJumpHeightRatio));
        }
        if(!IsGrounded && velocityOnJump!=Vector3.zero)
        {
            velocity += velocityOnJump;
        }
        if(!isFlyMode)
        velocity.y += curGravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public bool isFlyMode=false;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<BouncingPlatform>() != null)
        {
            Bounce(hit.gameObject.GetComponent<BouncingPlatform>().BounceDirection);
            return;
        }
        else if(hit.transform.parent!=null && hit.transform.parent.GetComponent<BouncingPlatform>()!=null)
        {
            Bounce(hit.transform.parent.GetComponent<BouncingPlatform>().BounceDirection);
            return;
        }
        if (hit.transform.gameObject.CompareTag("Ground") && hit.gameObject!=lastPlatform && IsGrounded)
        {
            if(GameManager.Instance.IsTreeDead)
            {
                Destroy(hit.gameObject);
                return;
            }
            lastPlatform = hit.gameObject;
            deathZone?.SwitchPosition();
        }
        else if(hit.transform.parent!=null && hit.transform.parent.CompareTag("Ground") && hit.transform.parent.gameObject!=lastPlatform && IsGrounded)
        {
            if (GameManager.Instance.IsTreeDead)
            {
                Destroy(hit.transform.parent.gameObject);
                return;
            }
            lastPlatform = hit.transform.parent.gameObject;
            deathZone?.SwitchPosition();
        }
    }

    public void GravityEffect()
    {
        velocity.y -= settings.gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void DashCooldown()
    {
        if(curDashCooldown>0)
        {
            curDashCooldown -= Time.deltaTime;
        }
    }

    public void ResetVelocity()
    {
        controller.Move(Vector3.zero);
    }

    public void TeleportToPosition(Vector3 position)
    {
        StartCoroutine(TeleportEffect(position));
    }

    private IEnumerator TeleportEffect(Vector3 position)
    {
        this.GetComponentInChildren<CameraManager>().BloomDieEffect(settings.deathBloomIntensity, settings.deathBloomDuration);
        GetComponent<PlayerManager>().FlyDisappearFromHand();
        filetCollider.RespawnFly();
        yield return new WaitForSeconds(0.3f);
        controller.enabled = false;
        this.transform.position = position;
        controller.enabled = true;
        deathZone.SwitchPosition();
        ResetVelocity();
    }

    public PlayerSettings Settings
    {
        get => settings;
    }

    private void Bounce(Vector3 dirVector)
    {
        velocity = Vector3.zero;
        velocity.y += settings.bounceForce;
        SoundManager.Instance.PlaySound(5);
        curJumpOnAir = 0;
        curDashOnAir = 0;
    }


}
