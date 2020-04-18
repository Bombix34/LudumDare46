using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : MonoBehaviour
{
    private Camera cam;

    private Vignette vignetteLayer;
    private PostProcessVolume volume;

    private void Awake()
    {
        cam = Camera.main;
        volume = GetComponent<PostProcessVolume>();
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
