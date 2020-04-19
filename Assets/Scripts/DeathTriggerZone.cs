using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerZone :Singleton<DeathTriggerZone>
{
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            //death trigger
            other.gameObject.GetComponent<CheckpointManager>().Respawn();

        }
    }

    public void SwitchPosition()
    {
        float yDisplacement = player.GetComponent<PlayerMovement>().Settings.deathTriggerZonePositionY;
        Vector3 finalPosition = new Vector3(player.transform.position.x, player.transform.position.y + yDisplacement, player.transform.position.z);
        if(finalPosition.y == this.transform.position.y)
        {
            return;
        }
        this.transform.position = finalPosition;
    }
}
