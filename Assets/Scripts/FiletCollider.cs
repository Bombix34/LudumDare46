using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiletCollider : MonoBehaviour
{
    private GameObject curFly;
    private List<GameObject> flyCatchs;

    private void Start()
    {
        flyCatchs = new List<GameObject>();
    }
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
        GameObject flyToCatch = curFly;
        flyCatchs.Add(flyToCatch);
        StartCoroutine(DesactivateFly(flyToCatch));
    }

    public IEnumerator DesactivateFly(GameObject flyToDesactivate)
    {
        yield return new WaitForSeconds(0.2f);
        flyToDesactivate.SetActive(false);
    }

    public void RespawnFly()
    {
        for(int i = 0; i < flyCatchs.Count;++i)
        {
            flyCatchs[i].SetActive(true);
        }
        curFly = null;
        flyCatchs.Clear();
    }

    public bool HasFly
    {
        get => curFly != null;
    }
}
