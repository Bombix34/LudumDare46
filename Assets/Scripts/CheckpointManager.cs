using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject curCheckpoint;

    public void SwitchCheckpoint(GameObject newCheckpoint)
    {
        if(curCheckpoint!=null && newCheckpoint== curCheckpoint)
        {
            return;
        }
        if(curCheckpoint!=null)
        {
            Destroy(curCheckpoint);
        }
        curCheckpoint = newCheckpoint;
    }

    public void Respawn()
    {
        this.GetComponent<PlayerMovement>().TeleportToPosition(curCheckpoint.transform.position);
    }
}
