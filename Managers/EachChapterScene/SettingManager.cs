using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;
    public GameObject settingPanel;
    public Text settingText;
    public Text cloudSaveText;
    public Text cloudLoadText;
    public Text creditText;
    public Text achievementText;
    public Text updateText;
    public GameObject soundButton;
    public Button noadsButton;
    public bool sound;
    public Text versionText;

    public string[] kindOfLanguage = { "한국어", "English" ,
    "français",
    "Deutsch",
    "português",
    "русский",
    "español",
    "italiano",
    "Nederlands",
    "Indonesia"};

    //, "日本語"
    [Header("About LanguageSetting")]
    public Text defaultLanguageText;

    public Text[] theOthersText;

    private IEnumerator Start()
    {
        instance = this;
        sound = bool.Parse(PlayerPrefs.GetString("sound", "True").ToLower());
        yield return new WaitUntil(() => SoundManager.instance != null);
        if (SaveManager.isIntroFirst)
            MuteSoundSet(false);
        else
            MuteSoundSet(sound);
        LanguagePanelSet(LanguageManager.ReturnLanguageIndex(LanguageManager.LanguageCode));
    }

    public void OnClickSettingButton()
    {
        SoundManager.instance.MenuSoundPlay(0);
        versionText.text = string.Format("Ver {0}", Application.version);
        settingText.text = LanguageManager.instance.ReturnMenuPopUpString(59);
        cloudSaveText.text = LanguageManager.instance.ReturnMenuPopUpString(27);
        cloudLoadText.text = LanguageManager.instance.ReturnMenuPopUpString(28);
        creditText.text = LanguageManager.instance.ReturnMenuPopUpString(31);
        achievementText.text = LanguageManager.instance.ReturnMenuPopUpString(39);
        updateText.text = LanguageManager.instance.ReturnMenuPopUpString(73);
        LanguagePanelSet(LanguageManager.ReturnLanguageIndex(LanguageManager.LanguageCode));
        if (SceneM.isVIP)
            noadsButton.interactable = false;
        else
            noadsButton.interactable = true;
        settingPanel.SetActive(true);
    }

    public void LanguagePanelSet(int index)
    {
        defaultLanguageText.text = kindOfLanguage[index];
        var j = 0;
        for (int i = 0; i < kindOfLanguage.Length; i++)
        {
            if (i == index)
                continue;
            theOthersText[j].text = kindOfLanguage[i];
            j++;
        }
    }

    public void OnClickLanguageButton(GameObject button)
    {
        var lan = button.transform.GetChild(0).GetComponent<Text>().text;
        var index = 0;
        for (int i = 0; i < kindOfLanguage.Length; i++)
        {
            if (lan == kindOfLanguage[i])
            {
                index = i;
                break;
            }
        }
        LanguagePanelSet(index);
        LanguageManager.instance.LanguageSetting(index);
    }

    public void SetSound()
    {
        var boolValue = SoundManager.instance.BGM.enabled;
        for (int i = 0; i < SoundManager.instance.allAudiosources.Length; i++)
        {
            SoundManager.instance.allAudiosources[i].enabled = !boolValue;
        }
        if (SoundManager.instance.BGM.enabled == false)
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(false);
            soundButton.transform.GetChild(1).gameObject.SetActive(true);
            sound = false;
        }
        else
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(true);
            soundButton.transform.GetChild(1).gameObject.SetActive(false);
            sound = true;
        }
        SaveManager.instance.SaveBool("sound", sound);
    }

    public void MuteSoundSet(bool sound)
    {
        for (int i = 0; i < SoundManager.instance.allAudiosources.Length; i++)
            SoundManager.instance.allAudiosources[i].enabled = sound;
        soundButton.transform.GetChild(0).gameObject.SetActive(sound);
        soundButton.transform.GetChild(1).gameObject.SetActive(!sound);
    }

    public void Rating()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.twogediGames.KnockKnock");
    }

    public void Sharing()
    {
        StartCoroutine(ShareAndroidText());
    }

    private IEnumerator ShareAndroidText()
    {
        //Share 해서 보낼때, 제목
        string subject = "문을 두드리세요(Knock Knock) : 힐링 한붓그리기";
        if (LanguageManager.LanguageCode != SystemLanguage.Korean)
            subject = "Knock Knock : One Line Drawing";

        //연결될 마켓 주소(링크)
        string body = "https://play.google.com/store/apps/details?id=com.twogediGames.KnockKnock";

        yield return new WaitForEndOfFrame();
        //execute the below lines if being run on a Android device
#if UNITY_ANDROID
        //Reference of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Reference of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
#endif
    }

    public void NoAdsSetting()
    {
        InAppPurchaser.instance.BuyProductID(InAppPurchaser.productID);
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/2GEDI-248745225879105");
    }

    public void Insta()
    {
        Application.OpenURL("https://www.instagram.com/knockknock_1line/");
    }

    public void CloudSave()
    {
        DontDestroyPopUpManager.instance.ShowSavePopUp();
    }

    public void CloudSave_YES()
    {
        //save 시작하기전 save data만들기
        //Debug.Log(SaveManager.instance.MergeSavedDataForCloudSave());
        GPGSLogin.instance.SaveToCloud(SaveManager.instance.MergeSavedDataForCloudSave());
    }

    public void CloudLoad()
    {
        DontDestroyPopUpManager.instance.ShowLoadPopUp();
    }

    public void CloudLoad_YES()
    {
        //Load다 되면 다된 정보 세팅하고 재시작 팝업 띄우기
        //LoadManager.instance.DataSettingFromCloudData(SaveManager.instance.MergeSavedDataForCloudSave());
        GPGSLogin.instance.LoadFromCloud();
    }

    public void Credit()
    {
        SceneM.instance.startSceneName = SceneManager.GetActiveScene().name;
        SceneM.instance.destinationSceneName = SceneM.CreditScene;
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("FadeOut");
    }

    public void ReStart()
    {
        AndroidJavaObject AOSUnityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaClass PendingIntentClass = new AndroidJavaClass("android.app.PendingIntent");
        AndroidJavaObject baseContext = AOSUnityActivity.Call<AndroidJavaObject>("getBaseContext");
        AndroidJavaObject intentObj
         = baseContext.Call<AndroidJavaObject>("getPackageManager").Call<AndroidJavaObject>("getLaunchIntentForPackage", baseContext.Call<string>("getPackageName"));

        AndroidJavaObject context = AOSUnityActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject pendingIntentObj
        = PendingIntentClass.CallStatic<AndroidJavaObject>("getActivity", context, 123456, intentObj, PendingIntentClass.GetStatic<int>("FLAG_CANCEL_CURRENT"));

        AndroidJavaClass AlarmManagerClass = new AndroidJavaClass("android.app.AlarmManager");
        AndroidJavaClass JavaSystemClass = new AndroidJavaClass("java.lang.System");

        AndroidJavaObject mAlarmManager = AOSUnityActivity.Call<AndroidJavaObject>("getSystemService", "alarm");
        long restartMillis = JavaSystemClass.CallStatic<long>("currentTimeMillis") + 100;
        mAlarmManager.Call("set", AlarmManagerClass.GetStatic<int>("RTC"), restartMillis, pendingIntentObj);

        JavaSystemClass.CallStatic("exit", 0);
    }

    public void Achievement()
    {
        StartCoroutine(AchievementCO());
    }

    public void Ranking()
    {
        StartCoroutine(RankingCO());
    }

    private IEnumerator AchievementCO()
    {
        if (GPGSLogin.instance.CheckLogin() == false)
        {
            StartCoroutine(GPGSLogin.instance.Init());
            yield return new WaitForSeconds(2f);
            if (GPGSLogin.instance.CheckLogin() == false)
                DontDestroyPopUpManager.instance.ShowLoginFailedPopUp();
        }
        Social.ShowAchievementsUI();
    }

    private IEnumerator RankingCO()
    {
        if (GPGSLogin.instance.CheckLogin() == false)
        {
            StartCoroutine(GPGSLogin.instance.Init());
            yield return new WaitForSeconds(2f);
            if (GPGSLogin.instance.CheckLogin() == false)
                DontDestroyPopUpManager.instance.ShowLoginFailedPopUp();
        }
        Social.ShowLeaderboardUI();
    }
}