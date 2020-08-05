using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public static Intro instance;
    public Animator fadeAnim;
    public GameObject pathreader;
    public PathReader_Intro pathReader_intro;

    [Header("TimeLine")]
    public PlayableDirector intro;

    public GameObject panel5GO;
    public Text startExp;
    public Text exp1;
    public Text exp2;

    public PlayableDirector[] middleToFinal;
    public GameObject[] cutGO;
    public PlayableDirector audio1;
    public PlayableDirector audio2;

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;
        intro.Play();
        audio1.Play();
        StartCoroutine(CoUpdate());
        startExp.text = LanguageManager.instance.ReturnMenuPopUpString(42);
        exp1.text = LanguageManager.instance.ReturnMenuPopUpString(40);
        exp2.text = LanguageManager.instance.ReturnMenuPopUpString(41);
    }

    private IEnumerator CoUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (intro.state.Equals(PlayState.Paused))
            {
                CreateWindow();
                yield break;
            }
        }
    }

    public void CreateWindow()
    {
        Camera.main.orthographic = true;
        panel5GO.SetActive(true);
        //fakeOneLine.SetActive(false);
        pathReader_intro.readJson();
        pathReader_intro.createWay();
    }

    public void WindowClear()
    {
        StartCoroutine(CoUpdate2());
    }

    private IEnumerator CoUpdate2()
    {
        panel5GO.SetActive(false);
        yield return new WaitForSeconds(1.5f);     
        Camera.main.orthographic = false;
        pathreader.SetActive(false);
        pathReader_intro.dotEffect.gameObject.SetActive(false);
        audio1.Pause();
        audio2.Play();

        var index = 0;
        middleToFinal[0].Play();
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (middleToFinal[index].state.Equals(PlayState.Paused))
            {
                index++;
                if (index >= middleToFinal.Length)
                {
                    IntroEnd();
                    yield break;
                }
                cutGO[index - 1].SetActive(false);
                middleToFinal[index - 1].Pause();
                middleToFinal[index].Play();
            }
        }
    }

    public void IntroEnd()
    {
        SaveManager.isIntroFirst = false;
        SaveManager.instance.SaveBool("isIntroFirst", SaveManager.isIntroFirst);
        HomeSceneManager.isFirst = true;
        SceneM.instance.destinationSceneName = SceneM.HomeScene;
        fadeAnim.SetTrigger("FadeOut");
    }
}