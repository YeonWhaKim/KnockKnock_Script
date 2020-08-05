using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Credit : MonoBehaviour
{
    public static Credit instance;
    public Button XButton;
    public Animator fadeAnim;
    public PlayableDirector[] creditTimeline;
    public GameObject[] cuts;
    public GameObject endingCredit;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        XButton.interactable = false;
        SoundManager.instance.BGM.Stop();
        SoundManager.instance.BGM.Play();
        creditTimeline[0].Play();
        StartCoroutine(CreditCo());
    }

    IEnumerator CreditCo()
    {
        var index = 0;
        yield return new WaitForSeconds(3f);
        XButton.interactable = true;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if(creditTimeline[index].state.Equals(PlayState.Paused))
            {
                index++;
                if(index>=creditTimeline.Length)
                {
                    endingCredit.SetActive(true);
                    yield break;
                }
                cuts[index - 1].SetActive(false);
                creditTimeline[index-1].Pause();
                creditTimeline[index].Play();

            }
        }
    }

    public void Exit()
    {
        SceneM.instance.destinationSceneName = SceneM.instance.startSceneName;
        fadeAnim.SetTrigger("FadeOut");
    }


}
