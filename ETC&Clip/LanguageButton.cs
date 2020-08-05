using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    public GameObject panel;

    public void OnClickLanguageButton()
    {
        panel.SetActive(!panel.activeSelf);
    }
}