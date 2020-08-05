using UnityEngine;

[System.Serializable]
public class Theme_Intro
{
    public Color dotColor;
    public Color drawingLineColor;
    public Color lineColor;
}

public class ThemeChanger_Intro : MonoBehaviour
{
    public Theme_Intro[] themes;
    public static Theme_Intro current;
    public static ThemeChanger_Intro instance;

    private void Awake()
    {
        current = themes[0];
        instance = this;
    }
}