using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager instance;
    public const int CURR_SERVICE_CHAPTER = 5;
    public const int DEFAULT_TOOL_COUNT = 4;
    public const int DEFAULT_FRIEND_COUNT = 8;
    public const int SpecialChapter = 0;
    public static int currChapter = 0;
    public static int currFriendIndex = 0;
    public static int currToolIndex = 0;
    public static int animalIndexToActivateTool;
    public static bool chapterCompleteShow;

    public static int currChapter_S = 0;
    public static int currFriendIndex_S = 0;
    public static int currToolIndex_S = 0;
    public static int animalIndexToActivateTool_S;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public void FriendIndexUp()
    {
        currFriendIndex++;
        //if (currFriendIndex >= DEFAULT_FRIEND_COUNT)
        //    currFriendIndex = DEFAULT_FRIEND_COUNT - 1;
        GameManager.CustomDebug("currFriendIndex : " + currFriendIndex);
        SaveManager.instance.SaveInt("currFriendIndex", currFriendIndex);
    }

    public void FriendIndexUp_S()
    {
        currFriendIndex_S++;
        //if (currFriendIndex >= DEFAULT_FRIEND_COUNT)
        //    currFriendIndex = DEFAULT_FRIEND_COUNT - 1;
        GameManager.CustomDebug("currFriendIndex_S : " + currFriendIndex_S);
        SaveManager.instance.SaveInt("currFriendIndex_S", currFriendIndex_S);
    }

    public void ToolIndexUp()
    {
        currToolIndex++;
        //if (currToolIndex >= DEFAULT_TOOL_COUNT)
        //    currToolIndex = DEFAULT_TOOL_COUNT - 1;
        GameManager.CustomDebug("currToolIndex : " + currToolIndex);
        SaveManager.instance.SaveInt("currToolIndex", currToolIndex);
    }

    public void ToolIndexUp_S()
    {
        currToolIndex_S++;
        //if (currToolIndex >= DEFAULT_TOOL_COUNT)
        //    currToolIndex = DEFAULT_TOOL_COUNT - 1;
        GameManager.CustomDebug("currToolIndex_S : " + currToolIndex_S);
        SaveManager.instance.SaveInt("currToolIndex_S", currToolIndex_S);
    }

    public void IsLastFriend()
    {
        if (KnockKnock.friendStandardValue == currFriendIndex)
            KnockKnock.instance.ui.LastKnockUI();
        else
            KnockKnock.instance.ui.KnockUIReset(true, !KnockKnock.instance.isFriend);
    }

    public void IsLastFriend_S()
    {
        if (KnockKnock_S.friendStandardValue == currFriendIndex_S)
            KnockKnock_S.instance.ui.LastKnockUI();
        else
            KnockKnock_S.instance.ui.KnockUIReset(true, !KnockKnock_S.instance.isFriend);
    }

    public void ChapterChange()
    {
        switch (currChapter)
        {
            case 0:
                Social.ReportProgress(GPGSIds.achievement_complete_chapter_1, 100f, null);
                break;

            case 1:
                Social.ReportProgress(GPGSIds.achievement_complete_chapter_2, 100f, null);
                break;

            case 2:
                Social.ReportProgress(GPGSIds.achievement_complete_chapter_3, 100f, null);
                break;

            case 3:
                Social.ReportProgress(GPGSIds.achievement_complete_chapter_4, 100f, null);
                break;

            case 4:
                Social.ReportProgress(GPGSIds.achievement_complete_chapter_5, 100f, null);
                break;

            default:
                break;
        }
        currChapter++;
        DataReset();
        if (currChapter >= CURR_SERVICE_CHAPTER)
        {
            currChapter = CURR_SERVICE_CHAPTER;
            return;
        }
        animalIndexToActivateTool = DataSplit.toolDBLists[currChapter][currToolIndex].activeToolIndex;
    }

    public void ChapterChanger_S()
    {
        Social.ReportProgress(GPGSIds.achievement_complete_chapter_s, 100f, null);
        currChapter_S++;
        SaveManager.instance.SaveInt("currChapter_S", currChapter_S);
    }

    public int Real_ChaToIndex(bool isFriend, int index)
    {
        if (isFriend)
            return (currChapter * CollectionManager.DEFAULT_FRIEND_COUNT) + index;
        else
            return (currChapter * CollectionManager.DEFAULT_TOOL_COUNT) + index;
    }

    public void DataReset()
    {
        currFriendIndex = 0;
        currToolIndex = 0;
        LevelData.levelSelected = 1;
        SaveManager.isRatingPopUpShown = false;
        SaveManager.instance.SaveBool("isRatingPopUpShown", SaveManager.isRatingPopUpShown);
        SaveManager.instance.SaveInt("currChapter", currChapter);
        SaveManager.instance.SaveInt("currFriendIndex", currFriendIndex);
        SaveManager.instance.SaveInt("currToolIndex", currToolIndex);
        SaveManager.instance.SaveInt("levelSelected", LevelData.levelSelected);

        if (currChapter >= CURR_SERVICE_CHAPTER)
            return;
        KnockKnock.instance.ToolKnockStadardValueSetting(true);
        KnockKnock.instance.FriendKnockStadardValueSetting(true);

        KnockKnock.instance.KnockCount = 0;
        KnockKnock.newChapterActiveStandardValue = 2;  // 그냥 여기서 새챕터 바뀌는 수 세팅하기.
    }
}