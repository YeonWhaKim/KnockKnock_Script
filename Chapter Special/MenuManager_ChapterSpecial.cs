using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager_ChapterSpecial : MonoBehaviour
{
    public static MenuManager_ChapterSpecial instance;
    public Animator loadAnim;
    public GameObject collectionPanel;
    public GameObject screenShotCanvas;
    public GameObject knockGuideGO;
    public GameObject knockGuideEffectGO;
    public GameObject chapExpGO;
    public GameObject noAdsButtonGO;
    public GameObject polaroidButtonGO;
    public GameObject revertButtonGO;
    public GameObject timeEffectGO;
    public ParticleSystem timeEffectParticle;
    public ParticleSystem timeEffectParticle2;
    public Text knockGuideText;
    public Text chapExpText;
    public GameObject lastExpPanel;
    public Text lastExpText;
    public Animator mermaidAnim;
    public bool isLastProductionDone = false;  

    private IEnumerator Start()
    {
        instance = this;

        StartCoroutine(IsKnockGuideAppearCo());

        chapExpText.text = LanguageManager.instance.ReturnChapterExpString_S(0);

        //if (chapter - 1 == ChapterManager.currChapter)
        //    StartCoroutine(IsKnockGuideAppearCo());
        //else
        if (ChapterManager.currChapter_S > 0)
        {
            timeEffectGO.SetActive(true);
            timeEffectParticle.Play();
            timeEffectParticle2.Play();
        }

        if (SaveManager.isSceneFirst_S)
        {
            chapExpGO.SetActive(true);
            SaveManager.isSceneFirst_S = false;
            SaveManager.instance.SaveString("isSceneFirst_S", "false");
        }

        yield return new WaitUntil(() => PolaroidManager_S.instance != null);
        yield return new WaitUntil(() => PopUpManager_EachChapterMainScene.instance != null);
        PolaroidButtonActive();
        PolaroidManager_S.instance.PanelSetting(true);
        if (!SceneM.instance.isAllCollected)
        {
            collectionPanel.SetActive(true);
            PolaroidManager_S.instance.ToolStateAnimation();
            SceneM.instance.isAllCollected = true;
        }

        if (SceneM.isVIP)
            noAdsButtonGO.SetActive(false);

        if (SceneM.instance.isAnyThingActive)
        {
            if (!SceneM.isVIP)
                CUtils.ShowInterstitialAd();
            SceneM.instance.isAnyThingActive = false;
        }

        if (ChapterManager.currFriendIndex_S.Equals(3)
            && SaveManager.isRatingPopUpShown.Equals(false)
            && SaveManager.isRatingPopUpYes.Equals(false))
            PopUpManager_EachChapterMainScene.instance.ShowRatePopUp();

        if (!SceneM.isVIP
        && ChapterManager.currFriendIndex_S.Equals(2)
        && SaveManager.isAdsPopUpYes.Equals(false))
            PopUpManager_EachChapterMainScene.instance.ShowAdRemovePopUp();

        //StartCoroutine(ChapterSettingManager_S.instance.LastProduction());
    }

    public void PolaroidButtonActive()
    {
        if (ChapterManager.SpecialChapter < ChapterManager.currChapter_S)
        {
            revertButtonGO.SetActive(true);
            if (!SaveManager.isChapterLastProduction)
                StartCoroutine(LastProduction());
        }
        else
            revertButtonGO.SetActive(false);

        if (CollectionManager.instance.EachChapterAchieventRate_S(ChapterManager.SpecialChapter) == 1
&& ChapterManager.SpecialChapter < ChapterManager.currChapter_S)
        {
            if (SaveManager.polaroidPopUp_S.Equals(false))
                StartCoroutine(ShowPolaroidPopUp());
            polaroidButtonGO.SetActive(true);
        }
        else
            polaroidButtonGO.SetActive(false);
    }

    IEnumerator ShowPolaroidPopUp()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitWhile(() => lastExpPanel.activeSelf);
        yield return new WaitForSeconds(1f);
        PopUpManager_EachChapterMainScene.instance.ShowPolaroidPopUp();
    }

    public void HomeButton()
    {
        SoundManager.instance.MenuSoundPlay(2);
        SceneM.instance.changeSceneIndex = 0;
        SceneM.instance.destinationSceneName = SceneM.HomeScene;
        SceneM.beforeChapter = ChapterManager.CURR_SERVICE_CHAPTER;
        loadAnim.SetTrigger("FadeOut");
    }

    public void SettingButton()
    {
        SettingManager.instance.OnClickSettingButton();
    }

    public void OnClickNoAdsButton()
    {
        DontDestroyPopUpManager.instance.ShowAdremovePublicPopUp();
    }

    public void OnClickNoAdsBuy()
    {
        InAppPurchaser.instance.BuyProductID(InAppPurchaser.productID);
    }

    public void PolaroidButton()
    {
        screenShotCanvas.SetActive(true);
        SoundManager.instance.PopUpSoundPlay(2);
    }

    public void CollectionButton()
    {
        collectionPanel.SetActive(true);
        PolaroidManager_S.instance.ToolStateAnimation();
        SoundManager.instance.MenuSoundPlay(1);
    }

    private IEnumerator IsKnockGuideAppearCo()
    {
        yield return new WaitWhile(() => chapExpGO.activeSelf);
        yield return new WaitForSeconds(3f);
        knockGuideText.text = LanguageManager.instance.ReturnMenuPopUpString(13);
        knockGuideGO.SetActive(true);
        knockGuideEffectGO.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        knockGuideGO.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        knockGuideEffectGO.SetActive(false);
        knockGuideGO.SetActive(false);

        while (true)
        {
            yield return new WaitForSeconds(4f);
            knockGuideGO.SetActive(true);
            knockGuideEffectGO.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            knockGuideGO.GetComponent<Animator>().SetTrigger("FadeOut");
            yield return new WaitForSeconds(1f);
            knockGuideGO.SetActive(false);
            knockGuideEffectGO.SetActive(false);
        }
    }

    public void PanelExplanationActive()
    {
        SoundManager.instance.MenuSoundPlay(3);
        chapExpGO.GetComponent<Animator>().SetTrigger("Fade");
    }

    public IEnumerator LastProduction()
    {
        SaveManager.isChapterLastProduction = true;
        SaveManager.instance.SaveString("isChapterLastProduction", SaveManager.isChapterLastProduction.ToString());
        //글씨 뜨고
        lastExpPanel.SetActive(true);
        lastExpText.text = LanguageManager.instance.ReturnChapterExpString_S(1);
        //3초 뒤에 꺼지고
        yield return new WaitForSeconds(3f);
        lastExpPanel.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1f);

        //인어공주 올라가고
        mermaidAnim.SetTrigger("Up");
        //다 올라가면
        yield return new WaitForSeconds(3.5f);
        //다시 마무리 글씨 뜨고
        lastExpPanel.SetActive(true);
        lastExpText.text = LanguageManager.instance.ReturnChapterExpString_S(2);
        lastExpPanel.GetComponent<Animator>().Rebind();
        //3초뒤에 꺼지면서
        yield return new WaitForSeconds(3f);
        lastExpPanel.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        lastExpPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        mermaidAnim.SetTrigger("FadeIn");

    }
}