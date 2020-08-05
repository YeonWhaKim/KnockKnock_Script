using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;
    public static SystemLanguage LanguageCode;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public void LanguageSetting(int index)
    {
        LanguageChange(index);
        // 설정창에 있는것들 싹 세팅
        SettingManager.instance.settingText.text = ReturnMenuPopUpString(59);
        SettingManager.instance.cloudSaveText.text = ReturnMenuPopUpString(27);
        SettingManager.instance.cloudLoadText.text = ReturnMenuPopUpString(28);
        SettingManager.instance.creditText.text = ReturnMenuPopUpString(31);
        SettingManager.instance.achievementText.text = ReturnMenuPopUpString(39);
        SettingManager.instance.updateText.text = ReturnMenuPopUpString(73);

        DontDestroyPopUpManager.instance.adBannerPopUp.expText.text = ReturnMenuPopUpString(61);

        //각 챕터별 세팅
        if (PolaroidPanelManager.instance != null)
        {
            PolaroidPanelManager.instance.PanelSetting();
            MenuManager_EachChapterMainScene.instance.knockGuideText.text = ReturnMenuPopUpString(13);
        }
        // 나중엔 홈씬에있는 chapter1 이런것도 글씨 싹 바꿔야함.
        if (UnityEngine.UI.Extensions.Examples.Example03Scene.instance != null)
        {
            UnityEngine.UI.Extensions.Examples.Example03Scene.instance.HomeChapterNameSetting();
            var chapter = ChapterManager.currChapter;
            if (chapter.Equals(ChapterManager.CURR_SERVICE_CHAPTER))
                chapter--;
            UnityEngine.UI.Extensions.Examples.Example03Scene.instance.ChapterWordSetting(chapter);
            UnityEngine.UI.Extensions.Examples.Example03Scene.instance.HomeChapterStateSetting(chapter);
        }
    }

    // 언어 추가시 setting쪽, settingManager도 추가해야함.
    public static int ReturnLanguageIndex(SystemLanguage languageCode)
    {
        var index = 0;
        switch (languageCode)
        {
            case SystemLanguage.Korean:
                index = 0;
                break;

            case SystemLanguage.English:
                index = 1;
                break;

            case SystemLanguage.French:
                index = 2;
                break;

            case SystemLanguage.German:
                index = 3;
                break;

            case SystemLanguage.Portuguese:
                index = 4;
                break;

            case SystemLanguage.Russian:
                index = 5;
                break;

            case SystemLanguage.Spanish:
                index = 6;
                break;

            case SystemLanguage.Italian:
                index = 7;
                break;

            case SystemLanguage.Dutch:
                index = 8;
                break;

            case SystemLanguage.Indonesian:
                index = 9;
                break;

            default:
                index = 1;
                break;
        }
        return index;
    }

    public static SystemLanguage ReturnSystemLanguage(int index)
    {
        switch (index)
        {
            case 0:
                return SystemLanguage.Korean;

            case 1:
                return SystemLanguage.English;

            case 2:
                return SystemLanguage.French;

            case 3:
                return SystemLanguage.German;

            case 4:
                return SystemLanguage.Portuguese;

            case 5:
                return SystemLanguage.Russian;

            case 6:
                return SystemLanguage.Spanish;

            case 7:
                return SystemLanguage.Italian;

            case 8:
                return SystemLanguage.Dutch;

            case 9:
                return SystemLanguage.Indonesian;

            default:
                return SystemLanguage.English;
        }
    }

    public void LanguageChange(int index)
    {
        switch (index)
        {
            case 0:
                LanguageCode = SystemLanguage.Korean;
                break;

            case 1:
                LanguageCode = SystemLanguage.English;
                break;

            case 2:
                LanguageCode = SystemLanguage.French;
                break;

            case 3:
                LanguageCode = SystemLanguage.German;
                break;

            case 4:
                LanguageCode = SystemLanguage.Portuguese;
                break;

            case 5:
                LanguageCode = SystemLanguage.Russian;
                break;

            case 6:
                LanguageCode = SystemLanguage.Spanish;
                break;

            case 7:
                LanguageCode = SystemLanguage.Italian;
                break;

            case 8:
                LanguageCode = SystemLanguage.Dutch;
                break;

            case 9:
                LanguageCode = SystemLanguage.Indonesian;
                break;

            default:
                LanguageCode = SystemLanguage.English;
                break;
        }
        SaveManager.instance.SaveInt("LanguageCode", index);
    }

    // 챕터 설명 관련
    public string ReturnChapterExpString(int index)
    {
        var returnValue = "";
        switch (LanguageCode)
        {
            case SystemLanguage.Korean:
                returnValue = DataSplit.chapterExp[index].kr;
                break;

            case SystemLanguage.English:
                returnValue = DataSplit.chapterExp[index].en;
                break;

            case SystemLanguage.French:
                returnValue = DataSplit.chapterExp[index].fr;
                break;

            case SystemLanguage.German:
                returnValue = DataSplit.chapterExp[index].de;
                break;

            case SystemLanguage.Portuguese:
                returnValue = DataSplit.chapterExp[index].pt;
                break;

            case SystemLanguage.Russian:
                returnValue = DataSplit.chapterExp[index].ru;
                break;

            case SystemLanguage.Spanish:
                returnValue = DataSplit.chapterExp[index].es;
                break;

            case SystemLanguage.Italian:
                returnValue = DataSplit.chapterExp[index].it;
                break;

            case SystemLanguage.Dutch:
                returnValue = DataSplit.chapterExp[index].nl;
                break;

            case SystemLanguage.Indonesian:
                returnValue = DataSplit.chapterExp[index].id;
                if (returnValue.Substring(returnValue.Length - 1) == "\n")
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                break;

            case SystemLanguage.Unknown:
                returnValue = DataSplit.chapterExp[index].en;
                break;
        }
        return returnValue;
    }

    public string ReturnChapterExpString_S(int index)
    {
        var returnValue = "";
        switch (LanguageCode)
        {
            case SystemLanguage.Korean:
                returnValue = DataSplit.chapterExp_special[index].kr;
                break;

            case SystemLanguage.English:
                returnValue = DataSplit.chapterExp_special[index].en;
                break;

            case SystemLanguage.French:
                returnValue = DataSplit.chapterExp_special[index].fr;
                break;

            case SystemLanguage.German:
                returnValue = DataSplit.chapterExp_special[index].de;
                break;

            case SystemLanguage.Portuguese:
                returnValue = DataSplit.chapterExp_special[index].pt;
                break;

            case SystemLanguage.Russian:
                returnValue = DataSplit.chapterExp_special[index].ru;
                break;

            case SystemLanguage.Spanish:
                returnValue = DataSplit.chapterExp_special[index].es;
                break;

            case SystemLanguage.Italian:
                returnValue = DataSplit.chapterExp_special[index].it;
                break;

            case SystemLanguage.Dutch:
                returnValue = DataSplit.chapterExp_special[index].nl;
                break;

            case SystemLanguage.Indonesian:
                returnValue = DataSplit.chapterExp_special[index].id;
                if (returnValue.Substring(returnValue.Length - 1) == "\n")
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                break;

            case SystemLanguage.Unknown:
                returnValue = DataSplit.chapterExp_special[index].en;
                break;
        }
        return returnValue;
    }

    public string ReturnChapterNameString(int index)
    {
        var returnValue = "";
        switch (LanguageCode)
        {
            case SystemLanguage.Korean:
                returnValue = DataSplit.chapterName[index].kr;
                break;

            case SystemLanguage.English:
                returnValue = DataSplit.chapterName[index].en;
                break;

            case SystemLanguage.French:
                returnValue = DataSplit.chapterName[index].fr;
                break;

            case SystemLanguage.German:
                returnValue = DataSplit.chapterName[index].de;
                break;

            case SystemLanguage.Portuguese:
                returnValue = DataSplit.chapterName[index].pt;
                break;

            case SystemLanguage.Russian:
                returnValue = DataSplit.chapterName[index].ru;
                break;

            case SystemLanguage.Spanish:
                returnValue = DataSplit.chapterName[index].es;
                break;

            case SystemLanguage.Italian:
                returnValue = DataSplit.chapterName[index].it;
                break;

            case SystemLanguage.Dutch:
                returnValue = DataSplit.chapterName[index].nl;
                break;

            case SystemLanguage.Indonesian:
                returnValue = DataSplit.chapterName[index].id;
                if (returnValue.Substring(returnValue.Length - 1) == "\n")
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                break;

            case SystemLanguage.Unknown:
                returnValue = DataSplit.chapterName[index].en;
                break;

            default:
                break;
        }
        return returnValue;
    }

    public string ReturnChapterNameString_S(int index)
    {
        var returnValue = "";
        switch (LanguageCode)
        {
            case SystemLanguage.Korean:
                returnValue = DataSplit.chapterName_special[index].kr;
                break;

            case SystemLanguage.English:
                returnValue = DataSplit.chapterName_special[index].en;
                break;

            case SystemLanguage.French:
                returnValue = DataSplit.chapterName_special[index].fr;
                break;

            case SystemLanguage.German:
                returnValue = DataSplit.chapterName_special[index].de;
                break;

            case SystemLanguage.Portuguese:
                returnValue = DataSplit.chapterName_special[index].pt;
                break;

            case SystemLanguage.Russian:
                returnValue = DataSplit.chapterName_special[index].ru;
                break;

            case SystemLanguage.Spanish:
                returnValue = DataSplit.chapterName_special[index].es;
                break;

            case SystemLanguage.Italian:
                returnValue = DataSplit.chapterName_special[index].it;
                break;

            case SystemLanguage.Dutch:
                returnValue = DataSplit.chapterName_special[index].nl;
                break;

            case SystemLanguage.Indonesian:
                returnValue = DataSplit.chapterName_special[index].id;
                if (returnValue.Substring(returnValue.Length - 1) == "\n")
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                break;

            case SystemLanguage.Unknown:
                returnValue = DataSplit.chapterName_special[index].en;
                break;

            default:
                break;
        }
        return returnValue;
    }

    public string ReturnSpecialChapterScriptString(int index)
    {
        var returnValue = "";
        switch (LanguageCode)
        {
            case SystemLanguage.Korean:
                returnValue = DataSplit.specialChapterScript[index].kr;
                break;

            case SystemLanguage.English:
                returnValue = DataSplit.specialChapterScript[index].en;
                break;

            case SystemLanguage.French:
                returnValue = DataSplit.specialChapterScript[index].fr;
                break;

            case SystemLanguage.German:
                returnValue = DataSplit.specialChapterScript[index].de;
                break;

            case SystemLanguage.Portuguese:
                returnValue = DataSplit.specialChapterScript[index].pt;
                break;

            case SystemLanguage.Russian:
                returnValue = DataSplit.specialChapterScript[index].ru;
                break;

            case SystemLanguage.Spanish:
                returnValue = DataSplit.specialChapterScript[index].es;
                break;

            case SystemLanguage.Italian:
                returnValue = DataSplit.specialChapterScript[index].it;
                break;

            case SystemLanguage.Dutch:
                returnValue = DataSplit.specialChapterScript[index].nl;
                break;

            case SystemLanguage.Indonesian:
                returnValue = DataSplit.specialChapterScript[index].id;
                if (returnValue.Substring(returnValue.Length - 1) == "\n")
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                break;

            case SystemLanguage.Unknown:
                returnValue = DataSplit.specialChapterScript[index].en;
                break;

            default:
                break;
        }
        return returnValue;
    }

    // 팝업, 메뉴관련 string
    public string ReturnMenuPopUpString(int kindIndex)
    {
        var returnValue = "";
        switch (LanguageCode)
        {
            case SystemLanguage.Korean:
                returnValue = DataSplit.menuPopUpString[kindIndex].kr;
                break;

            case SystemLanguage.English:
                returnValue = DataSplit.menuPopUpString[kindIndex].en;
                break;

            case SystemLanguage.French:
                returnValue = DataSplit.menuPopUpString[kindIndex].fr;
                break;

            case SystemLanguage.German:
                returnValue = DataSplit.menuPopUpString[kindIndex].de;
                break;

            case SystemLanguage.Portuguese:
                returnValue = DataSplit.menuPopUpString[kindIndex].pt;
                break;

            case SystemLanguage.Russian:
                returnValue = DataSplit.menuPopUpString[kindIndex].ru;
                break;

            case SystemLanguage.Spanish:
                returnValue = DataSplit.menuPopUpString[kindIndex].es;
                break;

            case SystemLanguage.Italian:
                returnValue = DataSplit.menuPopUpString[kindIndex].it;
                break;

            case SystemLanguage.Dutch:
                returnValue = DataSplit.menuPopUpString[kindIndex].nl;
                break;

            case SystemLanguage.Indonesian:
                returnValue = DataSplit.menuPopUpString[kindIndex].id;
                if (returnValue.Substring(returnValue.Length - 1) == "\n")
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                break;

            case SystemLanguage.Unknown:
                returnValue = DataSplit.menuPopUpString[kindIndex].en;
                break;
        }
        return returnValue;
    }

    // 도구, 친구관련 string
    public string ReturnEachLanguage(int kindIndex, int index)
    {
        var returnValue = "";
        switch (LanguageCode)
        {
            case SystemLanguage.English:
                returnValue = KindOfList(kindIndex)[index].en;
                break;

            case SystemLanguage.Korean:
                returnValue = KindOfList(kindIndex)[index].kr;
                break;

            case SystemLanguage.French:
                returnValue = KindOfList(kindIndex)[index].fr;
                break;

            case SystemLanguage.German:
                returnValue = KindOfList(kindIndex)[index].de;
                break;

            case SystemLanguage.Portuguese:
                returnValue = KindOfList(kindIndex)[index].pt;
                break;

            case SystemLanguage.Russian:
                returnValue = KindOfList(kindIndex)[index].ru;
                break;

            case SystemLanguage.Spanish:
                returnValue = KindOfList(kindIndex)[index].es;
                break;

            case SystemLanguage.Italian:
                returnValue = KindOfList(kindIndex)[index].it;
                break;

            case SystemLanguage.Dutch:
                returnValue = KindOfList(kindIndex)[index].nl;
                break;

            case SystemLanguage.Indonesian:
                returnValue = KindOfList(kindIndex)[index].id;
                break;

            case SystemLanguage.Unknown:
                returnValue = KindOfList(kindIndex)[index].en;
                break;
        }
        return returnValue;
    }

    public List<GrobalLanFrame> KindOfList(int kindIndex)
    {
        var returnValue = DataSplit.toolName;
        switch (kindIndex)
        {
            case 0:
                returnValue = DataSplit.toolName;
                break;

            case 1:
                returnValue = DataSplit.toolExp;
                break;

            case 2:
                returnValue = DataSplit.friendName;
                break;

            case 3:
                returnValue = DataSplit.friendExp;
                break;

            case 4:
                returnValue = DataSplit.toolName_special;
                break;

            case 5:
                returnValue = DataSplit.toolExp_special;
                break;

            case 6:
                returnValue = DataSplit.friendName_special;
                break;

            case 7:
                returnValue = DataSplit.friendExp_special;
                break;
        }
        return returnValue;
    }
}