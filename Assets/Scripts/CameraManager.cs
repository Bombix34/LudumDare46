using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void ApplyFOVEffect(float newFOVSize)
    {
        StartCoroutine(FOVEffect(newFOVSize));
    }

    private IEnumerator FOVEffect(float newFOVSize)
    {
        float baseFOV = cam.fieldOfView;
        while(cam.fieldOfView<newFOVSize)
        {
            cam.fieldOfView += (Time.deltaTime * 350f);
            yield return null;
        }
        cam.fieldOfView = newFOVSize;
        while(cam.fieldOfView>baseFOV)
        {
            cam.fieldOfView -= (Time.deltaTime*100f);
            yield return null;
        }
    }
}
