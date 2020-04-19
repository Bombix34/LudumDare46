using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatform : MonoBehaviour
{
    public Vector3 BounceDirection
    {
        get => transform.TransformDirection(transform.up).normalized;
    }
}
