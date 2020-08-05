using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public static bool isTwoPathTutorialDone = false;   //
    public static bool isRedArrowTutorialDone = false;  //
    public static bool[] isSceneFirst = { true, true, true, true, true, true, true, true }; //
    public static int[] bestScore = { 0, 0, 0, 0, 0, 0, 0, 0 };
    public static bool isSceneFirst_S = true;   //
    public static int bestScore_S = 0;  //
    public static string cloudSaveDate = " ";   //
    public static bool[] chapterAd = { false, true, true, true, true, false, false, false };  // 여기선 맨 앞이 스페셜 챕터.
    public static bool[] polaroidPopUp = { false, false, false, false, false, false, false };
    public static bool polaroidPopUp_S = false;

    public static bool isRatingPopUpShown = false;  //
    public static bool isRatingPopUpYes = false;    //
    public static bool isAdsPopUpYes = false;
    public static bool isIntroFirst = true; //
    public static string date = "0";    //
    public static int resetCount = 0;   //
    public static int revertCount = 0;  //

    public const int vipHintUpValue = 5;
    public const int reward_resetCount = 5;
    public const int reward_revertCount = 10;
    public static int gameButtonsIndex = 0;
    public static int chapterAdIndex;
    public static bool isChapterLastProduction = false;
    public static bool isTutorialGODone = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (instance != null)
            return;
        instance = this;
        PlayerData.instance.NumberOfHints = PlayerPrefs.GetInt("numberOfHints", 1);
        resetCount = PlayerPrefs.GetInt("resetCount", 5);
        revertCount = PlayerPrefs.GetInt("revertCount", 10);
        if (SceneM.isVIP)
        {
            if (DateTime.Now.ToString("dd").Equals(date))
                return;
            else
            {
                PlayerData.instance.NumberOfHints += vipHintUpValue;
                SaveString("date", DateTime.Now.ToString("dd"));
            }
        }
        //PlayerData.instance.NumberOfHints = 0;
    }

    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
        //Debug.Log(string.Format("{0} : {1}", key, value));
    }

    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetString(key, value.ToString());
        PlayerPrefs.Save();
        //Debug.Log(string.Format("{0} : {1}", key, value));
    }

    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
        //Debug.Log(string.Format("{0} : {1}", key, value));
    }

    public void SaveDicIntInt(string listName, Dictionary<int, int> dic)
    {
        for (int i = 0; i < dic.Count; i++)
        {
            PlayerPrefs.SetInt(string.Format("{0}{1}", listName, i), dic[i]);
            //Debug.Log(string.Format("{0}{1} : {2}", listName, i,dic[i]));
        }
        PlayerPrefs.Save();
    }

    public void SaveArrayStringToArrayInt(string listName, string[] dic)
    {
        for (int i = 0; i < dic.Length; i++)
        {
            PlayerPrefs.SetInt(string.Format("{0}{1}", listName, i), int.Parse(dic[i]));
            //Debug.Log(string.Format("{0}{1} : {2}", listName, i, dic[i]));
        }
        PlayerPrefs.Save();
    }

    public string MergeSavedDataForCloudSave()
    {
        string mergedData;
        var mergedData_int = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},",
            ChapterManager.currChapter,
            ChapterManager.currFriendIndex,
            ChapterManager.currToolIndex,
            LevelData.levelSelected,
            PlayerData.instance.NumberOfHints,
            ChapterManager.currChapter_S,
            ChapterManager.currFriendIndex_S,
            ChapterManager.currToolIndex_S,
            LevelData.levelSelected_S,
            resetCount,
            revertCount
            );

        var mergedData_bs = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},",
            isTwoPathTutorialDone.ToString(),
            isRedArrowTutorialDone.ToString(),
            isRatingPopUpShown.ToString(),
            isRatingPopUpYes.ToString(),
            isIntroFirst.ToString(),
            SettingManager.instance.sound.ToString(),
            SceneM.isVIP.ToString(),
            cloudSaveDate,
            date,
            isSceneFirst_S);

        var mergedData_listString = "";
        for (int i = 0; i < isSceneFirst.Length; i++)
        {
            if (i.Equals(0))
                mergedData_listString = string.Format("{0}-", isSceneFirst[i]);
            else if (i.Equals(isSceneFirst.Length - 1))
                mergedData_listString += string.Format("{0},", isSceneFirst[i]);
            else
                mergedData_listString += string.Format("{0}-", isSceneFirst[i]);
        }

        var mergedData_listStringChapterAd = "";
        for (int i = 0; i < chapterAd.Length; i++)
        {
            if (i.Equals(0))
                mergedData_listStringChapterAd = string.Format("{0}-", chapterAd[i]);
            else if (i.Equals(chapterAd.Length - 1))
                mergedData_listStringChapterAd += string.Format("{0},", chapterAd[i]);
            else
                mergedData_listStringChapterAd += string.Format("{0}-", chapterAd[i]);
        }

        var mergedData_listIntFriendDic = "";
        for (int i = 0; i < CollectionManager.friendStateDic.Count; i++)
        {
            if (i.Equals(0))
                mergedData_listIntFriendDic = string.Format("{0}-", CollectionManager.friendStateDic[i]);
            else if (i.Equals(CollectionManager.friendStateDic.Count - 1))
                mergedData_listIntFriendDic += string.Format("{0},", CollectionManager.friendStateDic[i]);
            else
                mergedData_listIntFriendDic += string.Format("{0}-", CollectionManager.friendStateDic[i]);
        }

        var mergedData_listInToolDic = "";
        for (int i = 0; i < CollectionManager.toolStateDic.Count; i++)
        {
            if (i.Equals(0))
                mergedData_listInToolDic = string.Format("{0}-", CollectionManager.toolStateDic[i]);
            else if (i.Equals(CollectionManager.toolStateDic.Count - 1))
                mergedData_listInToolDic += string.Format("{0},", CollectionManager.toolStateDic[i]);
            else
                mergedData_listInToolDic += string.Format("{0}-", CollectionManager.toolStateDic[i]);
        }

        var mergedData_listIntFriendDic_S = "";
        for (int i = 0; i < CollectionManager.friendStateDic_S.Count; i++)
        {
            if (i.Equals(0))
                mergedData_listIntFriendDic_S = string.Format("{0}-", CollectionManager.friendStateDic_S[i]);
            else if (i.Equals(CollectionManager.friendStateDic_S.Count - 1))
                mergedData_listIntFriendDic_S += string.Format("{0},", CollectionManager.friendStateDic_S[i]);
            else
                mergedData_listIntFriendDic_S += string.Format("{0}-", CollectionManager.friendStateDic_S[i]);
        }

        // toolDic이 항상 마지막
        var mergedData_listInToolDic_S = "";
        for (int i = 0; i < CollectionManager.toolStateDic_S.Count; i++)
        {
            if (i.Equals(0))
                mergedData_listInToolDic_S = string.Format("{0}-", CollectionManager.toolStateDic_S[i]);
            else if (i.Equals(CollectionManager.toolStateDic_S.Count - 1))
                mergedData_listInToolDic_S += string.Format("{0}", CollectionManager.toolStateDic_S[i]);
            else
                mergedData_listInToolDic_S += string.Format("{0}-", CollectionManager.toolStateDic_S[i]);
        }

        mergedData = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
            mergedData_int,
            mergedData_bs,
            mergedData_listString,
            mergedData_listStringChapterAd,
            mergedData_listIntFriendDic,
            mergedData_listInToolDic,
            mergedData_listIntFriendDic_S,
            mergedData_listInToolDic_S);
        return mergedData;
    }

    public void ChapterAdReward()
    {
        chapterAd[chapterAdIndex] = true;
        SaveString(string.Format("{0}{1}", "chapterAd", chapterAdIndex), chapterAd[chapterAdIndex].ToString());
        UnityEngine.UI.Extensions.Examples.Example03Scene.instance.HomeChapterStateSetting(SceneM.beforeChapter);
    }
}