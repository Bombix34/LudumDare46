using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> modulesInOrder;
    public Material dissolveMaterial;
    public Material okMaterial;
    public Material notOkMaterial;

    public PlayerSettings settings;

    private int curIndexModule = -1;

    private bool isDead;

    [SerializeField]
    private GameObject endGameUI;
    [SerializeField]
    private Text chronoText;

    [SerializeField]
    private GameObject gameOverUI;

    public bool isWinning { get; set; } = false;

    private float curChrono = 0f;

    [Range(0f,100f)]
    public float currentLife = 100f;

    public bool IsGameOver { get; set; } = false;

    private void Awake()
    {
        currentLife = settings.decrepitTime;
        isWinning = false;
        IsGameOver = false;
    }

    private void Start()
    {
        Cursor.visible = false;
        endGameUI.SetActive(false);
        gameOverUI.SetActive(false);
        if (modulesInOrder!=null)
        {
            for(int i =0; i < modulesInOrder.Count; ++i)
            {
                modulesInOrder[i].SetActive(false);
            }
            StartCoroutine(WaitForStartCoroutine());
        }
    }

    private IEnumerator WaitForStartCoroutine()
    {
        yield return new WaitForSeconds(1f);
        LoadNextModule();
    }

    private void Update()
    {
        if(isWinning)
        {
            return;
        }
        else if(IsGameOver)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
            else if(Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }
        currentLife -= Time.deltaTime;
        curChrono += Time.deltaTime;
        GameUIManager.Instance.UpdateTreeLifeUI(currentLife / settings.decrepitTime);
        if (currentLife<=0)
        {
            isDead = true;
        }
    }

    public void Win()
    {
        isWinning = true;
        endGameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        int curMin = Mathf.FloorToInt(curChrono / 60F);
        int curSec = Mathf.FloorToInt(curChrono - curMin * 60);
        string chronoString="";
        if(curMin<10)
        {
            chronoString += "0" + curMin;
        }
        else
        {
            chronoString += curMin;
        }
        chronoString += " ";
        if(curSec<10)
        {
            chronoString += "0" + curSec;
        }
        else
        {
            chronoString += curSec;
        }
        chronoText.text =chronoString;
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        IsGameOver = true;
        gameOverUI.SetActive(true);
        PlayerManager.Instance.GetComponent<PlayerMovement>().enabled = false;
    }

    public bool IsTreeDead
    {
        get => (currentLife / settings.decrepitTime)<=0;
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
