using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DelayedAppear : MonoBehaviour
{
    private Vector3 finalSize;
    public float chrono = 0f;
    public float appearSpeed = 1f;
    private float curChono;

    private bool IsShowed = false;

    private void Awake()
    {
        finalSize = this.transform.localScale;
        curChono = chrono;
        this.transform.localScale = new Vector3(0f, 0f, 0f);

    }

    private void Update()
    {
        if(curChono>0f)
        {
            curChono -= Time.deltaTime;
        }
        else if(!IsShowed)
        {
            IsShowed = true;
            this.transform.DOScaleX(finalSize.x, appearSpeed);
            this.transform.DOScaleY(finalSize.y, appearSpeed);
            this.transform.DOScaleZ(finalSize.z, appearSpeed);
        }
    }

    private IEnumerator StartAnimation()
    {
        this.transform.DOScaleX(finalSize.x*1.2f, appearSpeed * 0.8f);
        this.transform.DOScaleY(finalSize.y * 1.2f, appearSpeed * 0.8f);
        this.transform.DOScaleZ(finalSize.z * 1.2f, appearSpeed * 0.8f);
        yield return new WaitForSeconds(appearSpeed * 0.2f);
        this.transform.DOScaleX(finalSize.x, appearSpeed * 0.2f);
        this.transform.DOScaleY(finalSize.y, appearSpeed * 0.2f);
        this.transform.DOScaleZ(finalSize.z, appearSpeed * 0.2f);
    }
}
