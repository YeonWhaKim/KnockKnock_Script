using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    private bool isSetActiveFalse = false;
    public Text chapExpText;
    public Text circleExpText;
    public Text resetExpText;
    public Text revertExpText;
    public Text hintExpText;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        chapExpText.text = LanguageManager.instance.ReturnMenuPopUpString(49);
        circleExpText.text = LanguageManager.instance.ReturnMenuPopUpString(50);
        resetExpText.text = LanguageManager.instance.ReturnMenuPopUpString(51);
        revertExpText.text = LanguageManager.instance.ReturnMenuPopUpString(52);
        hintExpText.text = LanguageManager.instance.ReturnMenuPopUpString(53);
        yield return new WaitForSeconds(5f);
        isSetActiveFalse = true;
    }

    public void OnClickObject()
    {
        if (!isSetActiveFalse)
            return;
        StartCoroutine(SetActiveFalse());
    }

    private IEnumerator SetActiveFalse()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
