using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RedArrowTutorial : MonoBehaviour
{
    private bool isSetActivefalse = false;
    public Text expText;
    public Text expText2;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        expText.text = LanguageManager.instance.ReturnMenuPopUpString(30);
        expText2.text = LanguageManager.instance.ReturnMenuPopUpString(38);
        yield return new WaitForSeconds(3f);
        isSetActivefalse = true;
    }

    public void OnClickObject()
    {
        if (!isSetActivefalse)
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