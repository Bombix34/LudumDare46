using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatform : MonoBehaviour
{
    public Material bouncingMaterial;

    public void Start()
    {
        GetComponent<Renderer>().material = bouncingMaterial;    
    }

    public Vector3 BounceDirection
    {
        get => transform.TransformDirection(transform.up).normalized;
    }
}
