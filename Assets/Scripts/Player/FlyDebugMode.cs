using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDebugMode : MonoBehaviour
{

    void Start()
    {
        this.GetComponent<PlayerMovement>().isFlyMode = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            this.GetComponent<PlayerMovement>().isFlyMode = !this.GetComponent<PlayerMovement>().isFlyMode;
        }
    }
}
