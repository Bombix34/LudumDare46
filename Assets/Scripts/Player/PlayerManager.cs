using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject flyInHand;

    [SerializeField]
    private Animator filet;

    [SerializeField]
    private LayerMask raycastMask;

    private bool IsCatching = false;

    private void Awake()
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
            if(GetComponentInChildren<FiletCollider>().HasFly)
            {
                IsCatching = true;
                GetComponentInChildren<FiletCollider>().DestroyFly();
                StartCoroutine(CatchFly());
            }
        }
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
