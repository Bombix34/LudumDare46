using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecrepitEffect : MonoBehaviour
{
    // Blends between two materials

    public Material materialOk;
    public Material materialNotOk;
    float duration = 2.0f;
    Renderer rend;

    private float decrepitTotalTime;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        materialOk = GameManager.Instance.okMaterial;
        materialNotOk = GameManager.Instance.notOkMaterial;
        // At start, use the first material
        rend.material = materialOk;
        decrepitTotalTime = GameManager.Instance.currentLife;
    }

    private void Update()
    {
        rend.material.Lerp(materialNotOk, materialOk, GameManager.Instance.currentLife/decrepitTotalTime);
    }

    
}
