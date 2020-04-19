using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Material okMaterial;
    public Material notOkMaterial;

    public PlayerSettings settings;

    [Range(0f,100f)]
    public float currentLife = 100f;

    private void Awake()
    {
        currentLife = settings.decrepitTime;
    }

    private void Update()
    {
        currentLife -= Time.deltaTime;
    }

    public void FeedTree()
    {
        currentLife += (settings.decrepitTime*0.5f);
        if(currentLife>settings.decrepitTime)
        {
            currentLife = settings.decrepitTime;
        }
    }

}
