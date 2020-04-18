﻿using System.Collections;
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


    [Header("Dash Settings")]
    public float dashSpeed = 1f;
    public float dashDuration = 0.3f;
    public int dashNumber = 1;
}
