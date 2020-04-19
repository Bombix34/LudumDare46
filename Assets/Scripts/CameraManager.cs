using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private Camera cam;

    private Vignette vignetteLayer;
    private Bloom bloomLayer;
    private PostProcessVolume volume;

    private void Awake()
    {
        cam = Camera.main;
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
        volume.profile.TryGetSettings(out bloomLayer);
    }

    public void ApplyFOVEffect(float newFOVSize, float decreaseSpeed)
    {
        StartCoroutine(FOVEffect(newFOVSize,decreaseSpeed));
    }

    public void ApplyVignetteEffect(float newVignetteVal, float decreaseSpeed)
    {
        StartCoroutine(VignetteEffect(newVignetteVal, decreaseSpeed));
    }

    private IEnumerator FOVEffect(float newFOVSize, float decreaseSpeed)
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
            cam.fieldOfView -= (Time.deltaTime*decreaseSpeed);
            yield return null;
        }
    }

    private IEnumerator VignetteEffect(float newVignetteVal, float decreaseSpeed)
    {
        vignetteLayer.enabled.value = true;
        while(vignetteLayer.intensity.value<newVignetteVal)
        {
            vignetteLayer.intensity.value += (Time.deltaTime * 5f);
            yield return null;
        }
        vignetteLayer.intensity.value = newVignetteVal;
        while(vignetteLayer.intensity.value>0)
        {
            vignetteLayer.intensity.value -= (Time.deltaTime * decreaseSpeed);
            yield return null;
        }
        vignetteLayer.enabled.value = false;
    }

    public void BloomDieEffect(float endValue, float duration)
    {
        bloomLayer.enabled.value = true;
        DOTween.To(() => bloomLayer.intensity.value, x => bloomLayer.intensity.value = x, endValue, duration)
            .OnComplete(ResetBloom);
    }

    private void ResetBloom()
    {
        DOTween.To(() => bloomLayer.intensity.value, x => bloomLayer.intensity.value = x, 0f, 0.3f);
    }
}
