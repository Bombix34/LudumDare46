using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("MainScene");
        }
        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
