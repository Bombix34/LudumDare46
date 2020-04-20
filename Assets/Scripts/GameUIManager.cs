using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField]
    private PlayerSettings settings;

    [SerializeField]
    private Image cursor;
    [SerializeField]
    private Slider treeLifeSlider;
    [SerializeField]
    private Image treeLifeFillColor;

    [SerializeField]
    private Color colorLife;
    [SerializeField]
    private Color colorDeath;


    [SerializeField]
    private Image flowerWarningImg;
    [SerializeField]
    private Image flyWarningImg;

    private void Update()
    {
        cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, settings.cursorOpacity);
        if(treeLifeSlider.value <= 0.25f)
        {
            flowerWarningImg.color = new Color(flowerWarningImg.color.r, flowerWarningImg.color.g, flowerWarningImg.color.b, Mathf.PingPong(Time.time, 1f));
            flyWarningImg.color = new Color(flyWarningImg.color.r, flyWarningImg.color.g, flyWarningImg.color.b, Mathf.PingPong(Time.time, 1f));
        }
        else
        {
            if(flowerWarningImg.color.a<=0)
            {
                return;
            }
            flowerWarningImg.color = new Color(flowerWarningImg.color.r, flowerWarningImg.color.g, flowerWarningImg.color.b, 0f);
            flyWarningImg.color = new Color(flyWarningImg.color.r, flyWarningImg.color.g, flyWarningImg.color.b, 0f);
        }
    }

    public void UpdateTreeLifeUI(float newSliderValue)
    {
        treeLifeSlider.value = newSliderValue;
        treeLifeFillColor.color = Color.Lerp(colorDeath, colorLife, newSliderValue);
    }
}
