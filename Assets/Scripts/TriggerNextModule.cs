using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextModule : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.LoadNextModule();
            this.gameObject.SetActive(false);
        }
    }
}
