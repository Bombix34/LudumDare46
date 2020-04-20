using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecrepitEffect : MonoBehaviour
{
    // Blends between two materials

    public Material materialOk;
    public Material materialNotOk;
    public Material dissolveMaterial;
    float duration = 2.0f;
    Renderer rend;

    private float decrepitTotalTime;

    bool isDead = false;
    float dissolveAmount = 0f;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        materialOk = GameManager.Instance.okMaterial;
        materialNotOk = GameManager.Instance.notOkMaterial;
        dissolveMaterial = GameManager.Instance.dissolveMaterial;
        // At start, use the first material
        rend.material = materialOk;
        decrepitTotalTime = GameManager.Instance.currentLife;
    }

    private void Update()
    {
        if(GameManager.Instance.currentLife>0)
        {
            rend.material = materialOk;
            rend.material.Lerp(materialNotOk, materialOk, GameManager.Instance.currentLife/decrepitTotalTime);
        }
        else
        {
            rend.material = dissolveMaterial;
            dissolveAmount += (Time.deltaTime*0.5f);
            rend.material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }

    
}
