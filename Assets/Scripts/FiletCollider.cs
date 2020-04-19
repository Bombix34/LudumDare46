using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiletCollider : MonoBehaviour
{
    private GameObject curFly;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Fly"))
        {
            curFly = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Fly"))
        {
            curFly = null;
        }
    }

    public void DestroyFly()
    {
        Destroy(curFly);
    }

    public bool HasFly
    {
        get => curFly != null;
    }
}
