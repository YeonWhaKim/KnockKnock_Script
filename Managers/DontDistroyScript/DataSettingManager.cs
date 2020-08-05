using UnityEngine;

public class DataSettingManager : MonoBehaviour
{
    public static bool isDataSettingDone = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void FirstLoad()
    {
        //    var defLan = LanguageManager.ReturnLanguageIndex(Application.systemLanguage);
        //    LanguageManager.LanguageCode =
        //LanguageManager.ReturnSystemLanguage(PlayerPrefs.GetInt("LanguageCode", defLan));

        //    PlayerPrefs.DeleteAll();
        //    return;
        // 여기서 정보세팅을 해야겠음.
        DataSetting();
    }

    private static void DataSetting()
    {
        ChapterManager.currChapter = PlayerPrefs.GetInt("currChapter", 0);
        SceneM.beforeChapter = ChapterManager.currChapter;
        if (SceneM.beforeChapter.Equals(ChapterManager.CURR_SERVICE_CHAPTER))
            SceneM.beforeChapter--;
        ChapterManager.currFriendIndex = PlayerPrefs.GetInt("currFriendIndex", 0);
        ChapterManager.currToolIndex = PlayerPrefs.GetInt("currToolIndex", 0);
        ChapterManager.currChapter_S = PlayerPrefs.GetInt("currChapter_S", 0);
        ChapterManager.currFriendIndex_S = PlayerPrefs.GetInt("currFriendIndex_S", 0);
        ChapterManager.currToolIndex_S = PlayerPrefs.GetInt("currToolIndex_S", 0);

        SaveManager.isRedArrowTutorialDone = bool.Parse(PlayerPrefs.GetString("isRedArrowTutorialDone", "false").ToLower());
        SaveManager.isTwoPathTutorialDone = bool.Parse(PlayerPrefs.GetString("isTwoPathTutorialDone", "false").ToLower());
        SaveManager.isRatingPopUpShown = bool.Parse(PlayerPrefs.GetString("isRatingPopUpShown", "false").ToLower());
        SaveManager.isRatingPopUpYes = bool.Parse(PlayerPrefs.GetString("isRatingPopUpYes", "false").ToLower());
        SaveManager.isAdsPopUpYes = bool.Parse(PlayerPrefs.GetString("isAdsPopUpYes", "false").ToLower());
        SaveManager.isIntroFirst = bool.Parse(PlayerPrefs.GetString("isIntroFirst", "true").ToLower());
        SceneM.isVIP = bool.Parse(PlayerPrefs.GetString("isVIP", "false").ToLower());
        SaveManager.isTutorialGODone = bool.Parse(PlayerPrefs.GetString("isTutorialGODone", "false").ToLower());
        //SceneM.isVIP = true;

        SaveManager.cloudSaveDate = PlayerPrefs.GetString("cloudSaveDate", " ");
        SaveManager.date = PlayerPrefs.GetString("date", "0");
        SaveManager.isChapterLastProduction = bool.Parse(PlayerPrefs.GetString("isChapterLastProduction", "false").ToLower());

        for (int i = 0; i < ChapterManager.CURR_SERVICE_CHAPTER; i++)
        {
            SaveManager.isSceneFirst[i] =
                bool.Parse(PlayerPrefs.GetString(string.Format("{0}{1}", "isSceneFirst", i), "True").ToLower());
        }

        for (int i = 0; i < SaveManager.polaroidPopUp.Length; i++)
        {
            SaveManager.polaroidPopUp[i] =
                bool.Parse(PlayerPrefs.GetString(string.Format("{0}{1}", "polaroidPopUp", i), "False").ToLower());
        }
        SaveManager.polaroidPopUp_S = bool.Parse(PlayerPrefs.GetString("polaroidPopUp_S", "false").ToLower());

        for (int i = 0; i < ChapterManager.CURR_SERVICE_CHAPTER; i++)
        {
            SaveManager.bestScore[i] =
                PlayerPrefs.GetInt(string.Format("{0}{1}", "bestScore", i), 0);
        }
        SaveManager.isSceneFirst_S = bool.Parse(PlayerPrefs.GetString("isSceneFirst_S", "True").ToLower());
        SaveManager.bestScore_S = PlayerPrefs.GetInt("bestScore_S", 0);

        if (SaveManager.isIntroFirst)
        {
            for (int i = 0; i < SaveManager.chapterAd.Length; i++)
            {
                if (i.Equals(1) || i.Equals(2))
                    SaveManager.chapterAd[i] = true;
                else
                    SaveManager.chapterAd[i] = false;
            }
        }
        else
        {
            for (int i = 0; i < SaveManager.chapterAd.Length; i++)
            {
                if (i.Equals(0) || i.Equals(5))
                    SaveManager.chapterAd[i] = bool.Parse(PlayerPrefs.GetString(string.Format("{0}{1}", "chapterAd", i), "False").ToLower());
                else
                    SaveManager.chapterAd[i] = bool.Parse(PlayerPrefs.GetString(string.Format("{0}{1}", "chapterAd", i), "True").ToLower());
            }
        }
        var defLan = LanguageManager.ReturnLanguageIndex(Application.systemLanguage);
        LanguageManager.LanguageCode =
            LanguageManager.ReturnSystemLanguage(PlayerPrefs.GetInt("LanguageCode", defLan));

        isDataSettingDone = true;
    }
}