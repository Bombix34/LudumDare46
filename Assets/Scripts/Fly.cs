using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fly : MonoBehaviour
{
    private Vector3 initPosition;
    private float testChrono = 2f;
    private float randomX;

    private void Start()
    {
        initPosition = transform.position;
        randomX = Random.Range(-0.5f, 0.5f);
        testChrono = Random.Range(0.25f, 2f);
    }

    public void Update()
    {
        if (testChrono>0)
        {
            testChrono -= Time.deltaTime;
        }
        else
        {
            randomX = Random.Range(-0.5f, 0.5f);
            testChrono = Random.Range(0.25f, 2f);
        }
        transform.position=new Vector3(initPosition.x+Mathf.PingPong(Time.time * 2f, randomX), initPosition.y+Mathf.PingPong(Time.time*5f,0.3f), this.transform.position.z);
    }
}
