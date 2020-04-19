using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && (!isActive))
        {
            isActive = true;
            other.GetComponent<CheckpointManager>().SwitchCheckpoint(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            isActive = true;
            other.GetComponent<CheckpointManager>().SwitchCheckpoint(this.gameObject);
        }
    }
}
