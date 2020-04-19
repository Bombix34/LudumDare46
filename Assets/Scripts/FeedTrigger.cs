using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FeedTrigger : MonoBehaviour
{
    private float parentScale;
    [SerializeField]
    private Light flowerLighting;

    private void Awake()
    {
        parentScale = transform.parent.localScale.x;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerManager>().FeedPlant())
            {
                GrowEffect();
            }
        }
    }

    private void GrowEffect()
    {
        DOTween.To(() => flowerLighting.intensity, x => flowerLighting.intensity = x, 18f, 2f).OnComplete(() => DegrowEffect()); ;
    }

    private void DegrowEffect()
    {
        DOTween.To(() => flowerLighting.intensity, x => flowerLighting.intensity = x, 0f, 1f);
    }

}
