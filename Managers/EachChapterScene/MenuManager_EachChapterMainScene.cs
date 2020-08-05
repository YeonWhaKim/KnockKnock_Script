using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager_EachChapterMainScene : MonoBehaviour
{
    public static MenuManager_EachChapterMainScene instance;

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
    private int chapter;

    private IEnumerator Start()
    {
        instance = this;

        var sceneName = SceneManager.GetActiveScene().name;
        var chapterS = sceneName.Substring(sceneName.Length - 1);
        chapter = int.Parse(chapterS);
        StartCoroutine(IsKnockGuideAppearCo());

        chapExpText.text = LanguageManager.instance.ReturnChapterExpString(chapter - 1);
        //if (chapter - 1 == ChapterManager.currChapter)
        //    StartCoroutine(IsKnockGuideAppearCo());
        //else
        if (chapter - 1 < ChapterManager.currChapter)
        {
            timeEffectGO.SetActive(true);
            timeEffectParticle.Play();
            timeEffectParticle2.Play();
        }

        if (SaveManager.isSceneFirst[chapter - 1])
        {
            chapExpGO.SetActive(true);
            SaveManager.isSceneFirst[chapter - 1] = false;
            SaveManager.instance.SaveString(string.Format("{0}{1}", "isSceneFirst", chapter - 1), "false");
        }

        PolaroidButtonActive();

        if (ChapterManager.currFriendIndex.Equals(3)
            && SaveManager.isRatingPopUpShown.Equals(false)
            && SaveManager.isRatingPopUpYes.Equals(false))
            PopUpManager_EachChapterMainScene.instance.ShowRatePopUp();

        if (
            ChapterManager.currChapter.Equals(0)
            && !SceneM.isVIP
            && ChapterManager.currFriendIndex.Equals(2)
            && SaveManager.isAdsPopUpYes.Equals(false))
            PopUpManager_EachChapterMainScene.instance.ShowAdRemovePopUp();

        yield return new WaitUntil(() => PolaroidPanelManager.instance != null);
        PolaroidPanelManager.instance.PanelSetting();
        if (!SceneM.instance.isAllCollected)
        {
            collectionPanel.SetActive(true);
            PolaroidPanelManager.instance.ToolStateAnimation();
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
    }

    public void PolaroidButtonActive()
    {
        if (CollectionManager.instance.EachChapterAchieventRate(chapter - 1) == 1
    && chapter <= ChapterManager.currChapter)
        {
            if (SaveManager.polaroidPopUp[chapter - 1].Equals(false))
                PopUpManager_EachChapterMainScene.instance.ShowPolaroidPopUp(chapter - 1);
            polaroidButtonGO.SetActive(true);
        }
        else
            polaroidButtonGO.SetActive(false);

        if (chapter - 1 < ChapterManager.currChapter)
            revertButtonGO.SetActive(true);
        else
            revertButtonGO.SetActive(false);
    }

    public void HomeButton()
    {
        SoundManager.instance.MenuSoundPlay(2);
        SceneM.instance.changeSceneIndex = 0;
        SceneM.instance.destinationSceneName = SceneM.HomeScene;
        SceneM.beforeChapter = chapter - 1;
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
        PolaroidPanelManager.instance.ToolStateAnimation();
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
}