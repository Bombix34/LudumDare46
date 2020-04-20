using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> modulesInOrder;
    public Material okMaterial;
    public Material notOkMaterial;

    public PlayerSettings settings;

    private int curIndexModule = -1;

    [Range(0f,100f)]
    public float currentLife = 100f;

    private void Awake()
    {
        currentLife = settings.decrepitTime;
    }

    private void Start()
    {
        if(modulesInOrder!=null)
        {
            for(int i =0; i < modulesInOrder.Count; ++i)
            {
                modulesInOrder[i].SetActive(false);
            }
            LoadNextModule();
        }
    }

    private void Update()
    {
        currentLife -= Time.deltaTime;
        GameUIManager.Instance.UpdateTreeLifeUI(currentLife / settings.decrepitTime);
    }

    public void FeedTree()
    {
        float curLife = currentLife+(settings.decrepitTime * 0.5f);
        if (curLife > settings.decrepitTime)
        {
            curLife = settings.decrepitTime;
        }
        DOTween.To(() => currentLife, x => currentLife = x, curLife, 0.3f);
    }

    public void LoadNextModule()
    {
        if (modulesInOrder == null)
        {
            return;
        }
        curIndexModule++;
        if(curIndexModule>=modulesInOrder.Count)
        {
            curIndexModule = modulesInOrder.Count-1;
            return;
        }
        if(curIndexModule>=2)
        {
            modulesInOrder[curIndexModule - 2].SetActive(false);
        }
        modulesInOrder[curIndexModule].SetActive(true);
    }

}
