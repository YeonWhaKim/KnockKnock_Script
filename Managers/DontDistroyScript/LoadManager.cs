using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public void DataSettingFromCloudData(string data)
    {
        var dataSplit = data.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        SaveManager.instance.SaveInt("currChapter", int.Parse(dataSplit[0]));
        SaveManager.instance.SaveInt("currFriendIndex", int.Parse(dataSplit[1]));
        SaveManager.instance.SaveInt("currToolIndex", int.Parse(dataSplit[2]));
        SaveManager.instance.SaveInt("levelSelected", int.Parse(dataSplit[3]));
        SaveManager.instance.SaveInt("numberOfHints", int.Parse(dataSplit[4]));
        SaveManager.instance.SaveInt("currChapter_S", int.Parse(dataSplit[5]));
        SaveManager.instance.SaveInt("currFriendIndex_S", int.Parse(dataSplit[6]));
        SaveManager.instance.SaveInt("currToolIndex_S", int.Parse(dataSplit[7]));
        SaveManager.instance.SaveInt("levelSelected_S", int.Parse(dataSplit[8]));
        SaveManager.instance.SaveInt("resetCount", int.Parse(dataSplit[9]));
        SaveManager.instance.SaveInt("revertCount", int.Parse(dataSplit[10]));

        SaveManager.instance.SaveBool("isTwoPathTutorialDone", bool.Parse(dataSplit[11]));
        SaveManager.instance.SaveBool("isRedArrowTutorialDone", bool.Parse(dataSplit[12]));
        SaveManager.instance.SaveBool("isRatingPopUpShown", bool.Parse(dataSplit[13]));
        SaveManager.instance.SaveBool("isRatingPopUpYes", bool.Parse(dataSplit[14]));
        SaveManager.instance.SaveBool("isIntroFirst", bool.Parse(dataSplit[15]));
        SaveManager.instance.SaveBool("sound", bool.Parse(dataSplit[16]));
        SaveManager.instance.SaveBool("isVIP", bool.Parse(dataSplit[17]));
        SaveManager.instance.SaveString("cloudSaveDate", dataSplit[18]);
        SaveManager.instance.SaveString("date", dataSplit[19]);
        SaveManager.instance.SaveBool("isSceneFirst_S", bool.Parse(dataSplit[20]));

        var dataSplit_isSCeneFirst = dataSplit[21].Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < dataSplit_isSCeneFirst.Length; i++)
        {
            SaveManager.instance.SaveString(string.Format("{0}{1}", "isSceneFirst", i), dataSplit_isSCeneFirst[i]);
        }
        var dataSplit_chapterAD = dataSplit[22].Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < dataSplit_chapterAD.Length; i++)
        {
            SaveManager.instance.SaveString(string.Format("{0}{1}", "chapterAd", i), dataSplit_chapterAD[i]);
        }

        var dataSplit_friendDic = dataSplit[23].Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
        SaveManager.instance.SaveArrayStringToArrayInt("friendStateDic", dataSplit_friendDic);
        var dataSplit_toolDic = dataSplit[24].Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
        SaveManager.instance.SaveArrayStringToArrayInt("toolStateDic", dataSplit_toolDic);

        var dataSplit_friendDic_S = dataSplit[25].Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
        SaveManager.instance.SaveArrayStringToArrayInt("friendStateDic_S", dataSplit_friendDic_S);
        var dataSplit_toolDic_S = dataSplit[26].Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
        SaveManager.instance.SaveArrayStringToArrayInt("toolStateDic_S", dataSplit_toolDic_S);
    }
}