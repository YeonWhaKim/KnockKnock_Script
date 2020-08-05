using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneM : MonoBehaviour
{
    public static SceneM instance;
    public const string HomeScene = "Home";
    public const string GameScene = "Game";
    public const string GameScene_Special = "Game_Special";
    public const string IntroScene = "Intro";
    public const string CreditScene = "Credit";
    public const string TimeAttackScene = "TimeAttack";
    public const string Chapter1Scene = "Chapter 1";
    public const string Chapter2Scene = "Chapter 2";
    public const string Chapter3Scene = "Chapter 3";
    public const string Chapter4Scene = "Chapter 4";
    public const string Chapter5Scene = "Chapter 5";
    public const string ChapterSpecial = "Chapter Special";

    public GameObject exitPopUpGO;
    public Text expText;
    public Text yesText;
    public Text noText;
    public int changeSceneIndex;
    public int timeAttackChapter;
    public string startSceneName;
    public string destinationSceneName;
    private bool intro = true;
    public static bool isVIP = false;
    public bool isAllCollected = true;
    public bool chapterClear = false;
    public bool chapterClear_S = false;
    public bool isAnyThingActive = false;
    public bool isSpecialChapter = false;
    public static int beforeChapter;
    public bool isSpecialChapter_CallAd = false;

    private void Awake()
    {
        Application.targetFrameRate = 40;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Start()
    {
        //        /* Mandatory - set your AppsFlyer’s Developer key. */
        //        AppsFlyer.setAppsFlyerKey("zcKrZYJWnrWWctCxcLNnyT");
        //        /* For detailed logging */
        //        /* AppsFlyer.setIsDebug (true); */
        //#if UNITY_IOS
        //  /* Mandatory - set your apple app ID
        //   NOTE: You should enter the number only and not the "ID" prefix */
        //  AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
        //  AppsFlyer.trackAppLaunch ();
        //#elif UNITY_ANDROID
        //        /* Mandatory - set your Android package name */
        //        AppsFlyer.setAppID("com.twogediGames.KnockKnock");
        //        /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
        //        AppsFlyer.init("zcKrZYJWnrWWctCxcLNnyT", "AppsFlyerTrackerCallbacks");
        //#endif
        StartCoroutine(UpdateCo());
        yield return new WaitUntil(() => SaveManager.instance != null);

        if (SaveManager.isIntroFirst)
        {
            LoadScene(IntroScene);
        }
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator UpdateCo()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var sceneIndex = SceneManager.GetActiveScene().buildIndex;
                if (sceneIndex == 0) // Home씬 - 0
                {
                    if (DontDestroyPopUpManager.instance.networkState.go.activeSelf)
                        DontDestroyPopUpManager.instance.networkState.go.SetActive(false);
                    else if (DontDestroyPopUpManager.instance.saveClear.go.activeSelf)
                    {
                        DontDestroyPopUpManager.instance.saveClear.go.SetActive(false);
                        DontDestroyPopUpManager.instance.savePopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loadClear.go.activeSelf)
                        yield break;
                    else if (DontDestroyPopUpManager.instance.savePopUp.go.activeSelf)
                    {
                        if (!GPGSLogin.instance.onSaving)
                            DontDestroyPopUpManager.instance.savePopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loadPopUp.go.activeSelf)
                    {
                        if (!GPGSLogin.instance.onLoading)
                            DontDestroyPopUpManager.instance.loadPopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loginFailedPopUp.GO.activeSelf)
                        DontDestroyPopUpManager.instance.loginFailedPopUp.GO.SetActive(false);
                    else if (DontDestroyPopUpManager.instance.timeAttackUnLockPopUp.GO.activeSelf)
                        DontDestroyPopUpManager.instance.timeAttackUnLockPopUp.GO.SetActive(false);
                    else if (exitPopUpGO.activeSelf)
                        exitPopUpGO.SetActive(false);
                    else if (SettingManager.instance.settingPanel.activeSelf)
                        SettingManager.instance.settingPanel.SetActive(false);
                    else
                        OnClickExit();
                }
                else if (sceneIndex == 1)    // Game씬 - 1
                {
                    if (PopUpManager.instance.hintGetAdPopUp.GO.activeSelf)
                        PopUpManager.instance.hintGetAdPopUp.GO.SetActive(false);
                    else if (PopUpManager.instance.viewPopUp.GO.activeSelf)
                        PopUpManager.instance.viewPopUp.GO.SetActive(false);
                    else if (PopUpManager.instance.screenShotGO.activeSelf)
                        PopUpManager.instance.screenShotGO.GetComponent<ScreenShotManager>().OnClickBackButton();
                    else if (PopUpManager.instance.adPopUp.GO.activeSelf)
                    {
                        ActivationManager.instance.ToolNoActivation();
                        EffectManager.instance.CloseDoor();
                        MenuManager.instance.KnockTrue();
                        PopUpManager.instance.adPopUp.GO.SetActive(false);
                    }
                    else if (MenuManager.instance.helpPanel.activeSelf)
                        MenuManager.instance.helpPanel.SetActive(false);
                    else
                    {
                        if (EffectManager.instance.isBackButton)
                            MenuManager.instance.OnClickBackMenu();
                    }
                }
                else if (sceneIndex == 2)    // 스페셜 Game씬 - 2
                {
                    if (PopUpManager_S.instance.hintGetAdPopUp.GO.activeSelf)
                        PopUpManager_S.instance.hintGetAdPopUp.GO.SetActive(false);
                    else if (PopUpManager_S.instance.viewPopUp.GO.activeSelf)
                        PopUpManager_S.instance.viewPopUp.GO.SetActive(false);
                    else if (PopUpManager_S.instance.screenShotGO.activeSelf)
                        PopUpManager_S.instance.screenShotGO.GetComponent<ScreenShotManager>().OnClickBackButton();
                    else if (PopUpManager_S.instance.adPopUp.GO.activeSelf)
                    {
                        ActivationManager_S.instance.ToolNoActivation();
                        EffectManager_S.instance.CloseDoor();
                        MenuManager_S.instance.KnockTrue();
                        PopUpManager_S.instance.adPopUp.GO.SetActive(false);
                    }
                    else if (MenuManager_S.instance.helpPanel.activeSelf)
                        MenuManager_S.instance.helpPanel.SetActive(false);
                    else
                    {
                        if (EffectManager_S.instance.isBackButton)
                            MenuManager_S.instance.OnClickBackMenu();
                    }
                }
                else if (sceneIndex == 3)   // intro씬
                {
                    if (exitPopUpGO.activeSelf)
                        exitPopUpGO.SetActive(false);
                    else
                        OnClickExit();
                }
                else if (sceneIndex == 4)    // credit씬
                {
                    if (Credit.instance != null)
                        Credit.instance.Exit();
                }
                else if (sceneIndex == 5)    // timeAttack씬
                {
                    if(TimeAttackMenuManager.instance.startPanel.GO.activeSelf
                        || TimeAttackMenuManager.instance.winPanel.GO.activeSelf
                        || TimeAttackMenuManager.instance.losePanel.GO.activeSelf)
                        TimeAttackMenuManager.instance.StartPanelExit();
                    else
                    {
                        if (TimeAttackMenuManager.instance.pausePanel.GO.activeSelf)
                            TimeAttackMenuManager.instance.ShowPausePanel(false);
                        else
                            TimeAttackMenuManager.instance.ShowPausePanel(true);
                    }
                }
                else if (sceneIndex == 11)  // 스페셜 챕터씬
                {
                    if (MenuManager_ChapterSpecial.instance.screenShotCanvas.activeSelf)
                        ScreenShotManager.instance.OnClickBackButton();
                    else if (MenuManager_ChapterSpecial.instance.collectionPanel.activeSelf)
                    {
                        if (PolaroidManager_S.instance.adPopup.activeSelf)
                            PolaroidManager_S.instance.adPopup.SetActive(false);
                        else if (PolaroidManager_S.instance.expPopUp.activeSelf)
                            PolaroidManager_S.instance.expPopUp.SetActive(false);
                        else if (PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.activeSelf)
                            PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.SetActive(false);
                        else
                            MenuManager_ChapterSpecial.instance.collectionPanel.SetActive(false);
                    }
                    else if (PolaroidManager_S.instance.adPopup.activeSelf)
                        PolaroidManager_S.instance.adPopup.SetActive(false);
                    else if (PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.activeSelf)
                        PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.SetActive(false);
                    else if (exitPopUpGO.activeSelf)
                        exitPopUpGO.SetActive(false);
                    else if (DontDestroyPopUpManager.instance.networkState.go.activeSelf)
                        DontDestroyPopUpManager.instance.networkState.go.SetActive(false);
                    else if (DontDestroyPopUpManager.instance.saveClear.go.activeSelf)
                    {
                        DontDestroyPopUpManager.instance.saveClear.go.SetActive(false);
                        DontDestroyPopUpManager.instance.savePopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loadClear.go.activeSelf)
                        yield break;
                    else if (DontDestroyPopUpManager.instance.savePopUp.go.activeSelf)
                    {
                        if (!GPGSLogin.instance.onSaving)
                            DontDestroyPopUpManager.instance.savePopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loadPopUp.go.activeSelf)
                    {
                        if (!GPGSLogin.instance.onLoading)
                            DontDestroyPopUpManager.instance.loadPopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loginFailedPopUp.GO.activeSelf)
                        DontDestroyPopUpManager.instance.loginFailedPopUp.GO.SetActive(false);
                    else if (SettingManager.instance.settingPanel.activeSelf)
                        SettingManager.instance.settingPanel.SetActive(false);
                    else if (ChapterManager.currChapter.Equals(0)
                        && PopUpManager_EachChapterMainScene.instance.noAdsPopUp.GO.activeSelf)
                        PopUpManager_EachChapterMainScene.instance.noAdsPopUp.GO.SetActive(false);
                    else if (PopUpManager_EachChapterMainScene.instance.ratePopUp.GO.activeSelf)
                        PopUpManager_EachChapterMainScene.instance.ratePopUp.GO.SetActive(false);
                    else
                        MenuManager_ChapterSpecial.instance.HomeButton();
                }
                else
                {
                    if (MenuManager_EachChapterMainScene.instance.screenShotCanvas.activeSelf)
                        ScreenShotManager.instance.OnClickBackButton();
                    else if (MenuManager_EachChapterMainScene.instance.collectionPanel.activeSelf)
                    {
                        if (PolaroidPanelManager.instance.adPopup.activeSelf)
                            PolaroidPanelManager.instance.adPopup.SetActive(false);
                        else if (PolaroidPanelManager.instance.expPopUp.activeSelf)
                            PolaroidPanelManager.instance.expPopUp.SetActive(false);
                        else if (PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.activeSelf)
                            PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.SetActive(false);
                        else
                            MenuManager_EachChapterMainScene.instance.collectionPanel.SetActive(false);
                    }
                    else if (PolaroidPanelManager.instance.adPopup.activeSelf)
                        PolaroidPanelManager.instance.adPopup.SetActive(false);
                    else if (PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.activeSelf)
                        PopUpManager_EachChapterMainScene.instance.polaroidPopUP.GO.SetActive(false);
                    else if (exitPopUpGO.activeSelf)
                        exitPopUpGO.SetActive(false);
                    else if (DontDestroyPopUpManager.instance.networkState.go.activeSelf)
                        DontDestroyPopUpManager.instance.networkState.go.SetActive(false);
                    else if (DontDestroyPopUpManager.instance.saveClear.go.activeSelf)
                    {
                        DontDestroyPopUpManager.instance.saveClear.go.SetActive(false);
                        DontDestroyPopUpManager.instance.savePopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loadClear.go.activeSelf)
                        yield break;
                    else if (DontDestroyPopUpManager.instance.savePopUp.go.activeSelf)
                    {
                        if (!GPGSLogin.instance.onSaving)
                            DontDestroyPopUpManager.instance.savePopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loadPopUp.go.activeSelf)
                    {
                        if (!GPGSLogin.instance.onLoading)
                            DontDestroyPopUpManager.instance.loadPopUp.go.SetActive(false);
                    }
                    else if (DontDestroyPopUpManager.instance.loginFailedPopUp.GO.activeSelf)
                        DontDestroyPopUpManager.instance.loginFailedPopUp.GO.SetActive(false);
                    else if (SettingManager.instance.settingPanel.activeSelf)
                        SettingManager.instance.settingPanel.SetActive(false);
                    else if (ChapterManager.currChapter.Equals(0)
                        && PopUpManager_EachChapterMainScene.instance.noAdsPopUp.GO.activeSelf)
                        PopUpManager_EachChapterMainScene.instance.noAdsPopUp.GO.SetActive(false);
                    else if (PopUpManager_EachChapterMainScene.instance.ratePopUp.GO.activeSelf)
                        PopUpManager_EachChapterMainScene.instance.ratePopUp.GO.SetActive(false);
                    else
                        MenuManager_EachChapterMainScene.instance.HomeButton();
                }
                //PopUpManager.instance.OnClickExit();
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public string LoadMainScene(int chapter)
    {
        var returnValue = "";
        switch (chapter)
        {
            case 1:
                returnValue = Chapter1Scene;
                break;

            case 2:
                returnValue = Chapter2Scene;
                break;

            case 3:
                returnValue = Chapter3Scene;
                break;

            case 4:
                returnValue = Chapter4Scene;
                break;

            case 5:
                returnValue = Chapter5Scene;
                break;

            default:
                returnValue = ChapterSpecial;
                break;
        }
        return returnValue;
    }

    public void OnClickExit()
    {
        expText.text = LanguageManager.instance.ReturnMenuPopUpString(10);
        yesText.text = LanguageManager.instance.ReturnMenuPopUpString(11);
        noText.text = LanguageManager.instance.ReturnMenuPopUpString(12);
        exitPopUpGO.SetActive(true);
        exitPopUpGO.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void GameExit()
    {
        //if (GPGSLogin.instance.CheckLogin())
        //    ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
        Application.Quit();
    }

    public void AdReward(int index)
    {
        switch (index)
        {
            case 0: // 각 챕터씬에서의 광고보고 도구 얻기(CHAPTER)
                if (!isSpecialChapter_CallAd)
                    PolaroidPanelManager.instance.ToolActivation_InCollectionMenu();
                else
                    PolaroidManager_S.instance.ToolActivation_InCollectionMenu();
                break;

            case 1: // GAME씬 툴 얻기
                if (!isSpecialChapter_CallAd)
                    ActivationManager.instance.ToolActivation();
                else
                    ActivationManager_S.instance.ToolActivation();
                break;

            case 2: // RESET
                SaveManager.resetCount += SaveManager.reward_resetCount;
                if (!isSpecialChapter_CallAd)
                    UIControllerForGame.instance.UpdateReset();
                else
                    UIControllerForGame_S.instance.UpdateReset();
                break;

            case 3: // REVERT
                SaveManager.revertCount += SaveManager.reward_revertCount;
                if (!isSpecialChapter_CallAd)
                    UIControllerForGame.instance.UpdateRevert();
                else
                    UIControllerForGame_S.instance.UpdateRevert();
                break;

            case 4: // HINT
                if (!isSpecialChapter_CallAd)
                    TextManger.instance.HintValueChange(2);
                else
                    TextManager_S.instance.HintValueChange(2);
                break;
            case 5: // tool 광고 중간에 끊기
                if(!isSpecialChapter_CallAd)
                {
                    ActivationManager.instance.ToolNoActivation();
                    EffectManager.instance.CloseDoor();
                    MenuManager.instance.KnockTrue();
                }
                else
                {
                    ActivationManager_S.instance.ToolNoActivation();
                    EffectManager_S.instance.CloseDoor();
                    MenuManager_S.instance.KnockTrue();
                }
                break;

            default:
                break;
        }
    }
}