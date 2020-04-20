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

    private void Update()
    {
        cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, settings.cursorOpacity);
    }

    public void UpdateTreeLifeUI(float newSliderValue)
    {
        treeLifeSlider.value = newSliderValue;
        treeLifeFillColor.color = Color.Lerp(Color.black, Color.green, newSliderValue);
    }
}
