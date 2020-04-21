using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private GameObject flyInHand;

    [SerializeField]
    private Animator filet;

    [SerializeField]
    private GameObject InGameUI;

    [SerializeField]
    private Material winMaterial;

    private bool IsCatching = false;

    private void Start()
    {
        flyInHand.SetActive(false);
    }

    private void Update()
    {
        if(IsCatching)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && !flyInHand.activeInHierarchy)
        {
            SoundManager.Instance.PlaySound(4);
            filet.SetTrigger("Attack");
            if (GetComponentInChildren<FiletCollider>().HasFly&& GetComponentInChildren<FiletCollider>().IsGoldFly)
            {
                IsCatching = true;
                WinChange();
            }
            else if (GetComponentInChildren<FiletCollider>().HasFly)
            {
                IsCatching = true;
                GetComponentInChildren<FiletCollider>().DestroyFly();
                StartCoroutine(CatchFly());
            }
        }
    }

    private void WinChange()
    {
        GameManager.Instance.Win();
        InGameUI.SetActive(false);
        GetComponentInChildren<FiletCollider>().DestroyFly();
        flyInHand.GetComponent<Renderer>().material = winMaterial;
        StartCoroutine(CatchFly());
    }

    public bool FeedPlant()
    {
        if(flyInHand.activeInHierarchy)
        {
            //FEED TREE
            FlyDisappearFromHand();
            SoundManager.Instance.PlaySound(7);
            GameManager.Instance.FeedTree();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FlyAppearInHand()
    {
        flyInHand.transform.DOLocalMoveY(-0.6f,0f);
        flyInHand.SetActive(true);
        flyInHand.transform.DOLocalMoveY(-0.2f, 0.4f);
    }

    public void FlyDisappearFromHand()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(flyInHand.transform.DOLocalMoveY(-0.6f, 0.4f))
            .OnComplete(() => flyInHand.SetActive(false)); ;
    }

    private IEnumerator CatchFly()
    {
        yield return new WaitForSeconds(0.3f);
        FlyAppearInHand();
        IsCatching = false;
    }
}
