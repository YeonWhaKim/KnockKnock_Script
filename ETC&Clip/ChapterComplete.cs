using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterComplete : MonoBehaviour
{
    public Text nextText;
    public Button nextButton;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(NextButtonDisabled());
    }

    IEnumerator NextButtonDisabled()
    {
        nextText.color = GameManager.HexToColor("#6D6D6D");
        nextButton.interactable = false;
        yield return new WaitForSeconds(3f);
        nextText.color = GameManager.HexToColor("#FFFFFF");
        nextButton.interactable = true;
    }

    public void ButtonClick()
    {
        nextButton.interactable = false;
    }
}
