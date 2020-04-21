using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerZone :Singleton<DeathTriggerZone>
{
    [SerializeField]
    private GameObject player;
    public GameObject Player
    {
        set => player = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            //death trigger
            if(GameManager.Instance.IsTreeDead)
            {
                //GAME OVER
            }
            else
            {
                other.gameObject.GetComponent<CheckpointManager>().Respawn();
            }

        }
    }

    public void SwitchPosition()
    {
        float yDisplacement = player.GetComponent<PlayerMovement>().Settings.deathTriggerZonePositionY;
        Vector3 finalPosition = new Vector3(player.transform.position.x, player.transform.position.y + yDisplacement, player.transform.position.z);
        this.transform.position = finalPosition;
    }
}
