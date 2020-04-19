using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LD46/Player Reglages")]
public class PlayerSettings : ScriptableObject
{
    [Header("View Settings")]
    public float mouseSensitivity = 100f;

    [Header("Movement Settings")]
    public float speed = 12f;
    public float gravity = -9.81f;

    [Header("Jump Settings")]
    public float jumpHeight = 1f;
    [Range(0f,1f)]
    public float airControl = 1f;

    public int multipleJumpNumber = 1;
    [Range(0f, 1f)]
    public float multipleJumpHeightRatio=0.6f;

    [Range(0f, 1f)]
    public float coyoteTime = 0.2f;


    [Header("Dash Settings")]
    public bool IsDashOnVision = false;
    public float dashSpeed = 1f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 0.1f;
    public int dashNumber = 1;

    [Header("Dash FX")]

    public int dashFOV = 80;
    public float dashFOVDecreaseSpeed = 150f;
    [Range(0f, 1f)]
    public float dashVignette;
    public float dashVignetteDecreaseSpeed = 2f;


    [Header("Death Settings")]
    [Tooltip("La distance en Y de la death zone")]
    public float deathTriggerZonePositionY = -10f;
    public float deathBloomIntensity = 50f;
    public float deathBloomDuration = 0.4f;
}
