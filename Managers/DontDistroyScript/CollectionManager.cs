using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    //[Serializable]
    //public class UI
    //{
    //    public int currSelectedChapter;
    //    public RectTransform contentRT;
    //    public GameObject selectEffect;
    //    public Text mainChapterText;
    //    public List<Text> eachChapterPercentageText;
    //    public Image percentageUnderBarImage;
    //    public List<Sprite> chapterStateSprite = new List<Sprite>();
    //    public RectTransform menuUnderBarRT;

    //    public List<Image> toolImageLists;
    //    public List<Image> toolStateImageLists;
    //    public List<Text> toolNameTextLists;
    //    public List<Button> toolADButtonLists;

    //    public List<Image> friendImageLists;
    //    public List<Image> friendStateImageLists;
    //    public List<Text> friendNameTextLists;

    //    // 도감 스와이프
    //    public void ChapterSwipe(bool isRight)
    //    {
    //        selectEffect.SetActive(false);
    //        if (isRight)
    //        {
    //            currSelectedChapter++;
    //            if (currSelectedChapter >= eachChapterPercentageText.Count)
    //            {
    //                currSelectedChapter = eachChapterPercentageText.Count - 1;
    //                MoveDone(currSelectedChapter);
    //                return;
    //            }
    //            var destination = contentRT.localPosition - new Vector3(300, 0, 0);
    //            instance.StartCoroutine(ChapterLerp(contentRT, destination, 30, -1));
    //        }
    //        else
    //        {
    //            currSelectedChapter--;
    //            if (currSelectedChapter < 0)
    //            {
    //                currSelectedChapter = 0;
    //                MoveDone(currSelectedChapter);
    //                return;
    //            }
    //            var destination = contentRT.localPosition + new Vector3(300, 0, 0);
    //            instance.StartCoroutine(ChapterLerp(contentRT, destination, 30, 1));
    //        }
    //    }

    //    private IEnumerator ChapterLerp(RectTransform rect, Vector3 destination, int moveValue, int isMinus)
    //    {
    //        while ((int)rect.localPosition.x != (int)destination.x)
    //        {
    //            rect.localPosition += new Vector3(moveValue * isMinus, 0, 0);
    //            yield return new WaitForSeconds(0.02f);
    //        }
    //        MoveDone(currSelectedChapter);
    //    }

    //    private void MoveDone(int currChapter)
    //    {
    //        selectEffect.SetActive(true);
    //        ChapterSellect(currChapter + 1);
    //        if (currChapter <= ChapterManager.instance.currChapter)
    //            percentageUnderBarImage.sprite = chapterStateSprite[0];
    //        else
    //            percentageUnderBarImage.sprite = chapterStateSprite[1];
    //        percentageUnderBarImage.SetNativeSize();
    //    }

    //    // 메뉴 언더바 이동
    //    public void MenuUnderlineLerp(int currButton, Vector3 destination)
    //    {
    //        var res = currButton - instance.currMenuIndex;
    //        if (res < 0)
    //            instance.StartCoroutine(ChapterLerp(menuUnderBarRT, destination, 33 * Mathf.Abs(res), -1));
    //        else
    //            instance.StartCoroutine(ChapterLerp(menuUnderBarRT, destination, 33 * Mathf.Abs(res), 1));

    //        instance.currMenuIndex = currButton;
    //    }

    //    // 도감 메뉴 열 때 도감 정보 세팅
    //    public void CollectionOpenSetting()
    //    {
    //        currSelectedChapter = 0;
    //        contentRT.anchoredPosition = new Vector3(-525, 0, 0);
    //        for (int i = 0; i < eachChapterPercentageText.Count; i++)
    //        {
    //            if (i <= ChapterManager.instance.currChapter)
    //            {
    //                eachChapterPercentageText[i].text = string.Format("{0}%", instance.EachChapterAchieventRate(i));
    //            }
    //            else
    //            {
    //                eachChapterPercentageText[i].text = string.Format(" ");
    //            }
    //        }
    //        instance.OnClickActivitiesMenu(0);
    //        MoveDone(0);
    //    }

    //    // 챕터 선택되면 각 챕터별, 도구, 친구정보 세팅.
    //    public void ChapterSellect(int chapter)
    //    {
    //        if (chapter == 3)
    //            return;
    //        mainChapterText.text = string.Format("CHAPTER{0}", chapter.ToString());
    //        // 친구메뉴 세팅
    //        var friendChaToIndex = (chapter - 1) * DEFAULT_FRIEND_COUNT;
    //        for (int i = 0; i < DEFAULT_FRIEND_COUNT; i++)
    //        {
    //            friendImageLists[i].sprite = instance.friendSprites[i + friendChaToIndex];
    //            friendStateImageLists[i].sprite =
    //                instance.StateSpriteLists[instance.friendStateDic[i + friendChaToIndex]];
    //            if (instance.friendStateDic[i + friendChaToIndex] == 0)
    //            {
    //                friendNameTextLists[i].text = instance.friendName_en[i + friendChaToIndex];
    //                friendImageLists[i].color = Color.white;
    //            }
    //            else
    //            {
    //                friendNameTextLists[i].text = string.Format("???");
    //                friendImageLists[i].color = new Vector4(1f, 0.48f, 0.48f, 0.14f);
    //            }
    //        }
    //        if (chapter == 1)
    //            friendImageLists[DEFAULT_FRIEND_COUNT - 1].transform.parent.gameObject.SetActive(false);
    //        else
    //            friendImageLists[DEFAULT_FRIEND_COUNT - 1].transform.parent.gameObject.SetActive(true);

    //        // 도구메뉴 세팅
    //        var toolChaToIndex = (chapter - 1) * DEFAULT_TOOL_COUNT;

    //        for (int i = 0; i < DEFAULT_TOOL_COUNT; i++)
    //        {
    //            toolImageLists[i].sprite = instance.toolSprites[i + toolChaToIndex];
    //            toolStateImageLists[i].sprite =
    //                instance.StateSpriteLists[instance.toolStateDic[i + toolChaToIndex]];
    //            toolADButtonLists[i].interactable = false;
    //            Debug.Log(i + toolChaToIndex + " - " + instance.toolStateDic[i + toolChaToIndex]);
    //            if (instance.toolStateDic[i + toolChaToIndex] == 0)
    //            {
    //                toolNameTextLists[i].text = instance.toolName_en[i + toolChaToIndex];
    //                toolImageLists[i].color = Color.white;
    //            }
    //            else
    //            {
    //                if (instance.toolStateDic[i + toolChaToIndex] == 1)
    //                    toolADButtonLists[i].interactable = true;
    //                toolNameTextLists[i].text = string.Format("???");
    //                toolImageLists[i].color = new Vector4(1f, 0.48f, 0.48f, 0.14f);
    //            }
    //        }
    //    }
    //}

    //[SerializeField] public UI ui;

    public static CollectionManager instance;
    public const int DEFAULT_TOOL_COUNT = 4;
    public const int DEFAULT_FRIEND_COUNT = 8;
    public const int STATE_ACTIVATION = 0;
    public const int STATE_AD = 1;
    public const int STATE_ROCK = 2;

    public List<Sprite> toolSprites = new List<Sprite>();
    public List<Sprite> friendSprites = new List<Sprite>();

    public List<Sprite> toolSprites_S = new List<Sprite>();
    public List<Sprite> friendSprites_S = new List<Sprite>();

    // value = 0 (활성화), =1 (광고보고와야함), =2 (잠김)
    public static Dictionary<int, int> toolStateDic = new Dictionary<int, int>();
    public static Dictionary<int, int> toolStateDic_S = new Dictionary<int, int>();
    public static Dictionary<int, int> friendStateDic = new Dictionary<int, int>();
    public static Dictionary<int, int> friendStateDic_S = new Dictionary<int, int>();

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;

        for (int i = 0; i < DEFAULT_TOOL_COUNT * ChapterManager.CURR_SERVICE_CHAPTER; i++)
        {
            //toolStateDic.Add(i, PlayerPrefs.GetInt(string.Format("{0}{1}", toolStateDic, i), 2));
            //toolStateDic.Add(i, STATE_ROCK);
            //toolStateDic.Add(i, STATE_ACTIVATION);
            //toolStateDic.Add(i, STATE_AD);
            toolStateDic.Add(i, PlayerPrefs.GetInt(string.Format("{0}{1}", "toolStateDic", i), STATE_ROCK));
        }
        for (int i = 0; i < DEFAULT_FRIEND_COUNT * ChapterManager.CURR_SERVICE_CHAPTER; i++)
        {
            //friendStateDic.Add(i, PlayerPrefs.GetInt(string.Format("{0}{1}", friendStateDic, i), 2));
            //friendStateDic.Add(i, STATE_ROCK);
            //friendStateDic.Add(i, STATE_ACTIVATION);
            friendStateDic.Add(i, PlayerPrefs.GetInt(string.Format("{0}{1}", "friendStateDic", i), STATE_ROCK));
        }
        for (int i = 0; i < DEFAULT_TOOL_COUNT * 1; i++)
        {
            toolStateDic_S.Add(i, PlayerPrefs.GetInt(string.Format("{0}{1}", "toolStateDic_S", i), STATE_ROCK));
        }
        for (int i = 0; i < DEFAULT_FRIEND_COUNT*1; i++)
        {
            friendStateDic_S.Add(i, PlayerPrefs.GetInt(string.Format("{0}{1}", "friendStateDic_S", i), STATE_ROCK));
        }
    }

    // 친구 활성화시 도감 세팅.
    public void FriendCollectionSetting(int chapter, int index)
    {
        friendStateDic[OneRayValue(chapter, index, DEFAULT_FRIEND_COUNT)] = STATE_ACTIVATION;
        SaveManager.instance.SaveDicIntInt("friendStateDic", friendStateDic);
        GameManager.CustomDebug("friend_chapter : " + chapter + "/ index : " + index + "/ state : " + friendStateDic[OneRayValue(chapter, index, DEFAULT_FRIEND_COUNT)]);
    }

    public void FriendCollectionSetting_S(int chapter, int index)
    {
        friendStateDic_S[OneRayValue(chapter, index, DEFAULT_FRIEND_COUNT)] = STATE_ACTIVATION;
        SaveManager.instance.SaveDicIntInt("friendStateDic_S", friendStateDic_S);
        GameManager.CustomDebug("friend_chapter_S : " + chapter + "/ index : " + index + "/ state : " + friendStateDic_S[OneRayValue(chapter, index, DEFAULT_FRIEND_COUNT)]);
    }

    // 도구 활성화시 도감 세팅.
    public void ToolCollectionSetting(int chapter, int index)
    {
        toolStateDic[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)] = STATE_ACTIVATION;
        SaveManager.instance.SaveDicIntInt("toolStateDic", toolStateDic);
        GameManager.CustomDebug("tool_chapter : " + chapter + "/ index : " + index + "/ state : " + toolStateDic[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)]);
    }

    public void ToolCollectionSetting_S(int chapter, int index)
    {
        toolStateDic_S[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)] = STATE_ACTIVATION;
        SaveManager.instance.SaveDicIntInt("toolStateDic_S", toolStateDic_S);
        GameManager.CustomDebug("tool_chapter_S : " + chapter + "/ index : " + index + "/ state : " + toolStateDic_S[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)]);
    }

    public void ToolAdPopUpNo(int chapter, int index)
    {
        toolStateDic[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)] = STATE_AD;
        SaveManager.instance.SaveDicIntInt("toolStateDic", toolStateDic);
        GameManager.CustomDebug("tool AD PopUpNo_chapter : " + chapter + "/ index : " + index + "/ state : " + toolStateDic[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)]);
    }

    public void ToolAdPopUpNo_S(int chapter, int index)
    {
        toolStateDic_S[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)] = STATE_AD;
        SaveManager.instance.SaveDicIntInt("toolStateDic_S", toolStateDic_S);
        GameManager.CustomDebug("tool AD PopUpNo_chapter : " + chapter + "/ index : " + index + "/ state : " + toolStateDic_S[OneRayValue(chapter, index, DEFAULT_TOOL_COUNT)]);
    }

    // 버튼 누를 때마다 도감 정보 바뀜.
    public void OnClickChapter(int chapter)
    {
        //ui.ChapterSellect(chapter);
    }

    public int OneRayValue(int chapter, int index, int standardValue)
    {
        return (chapter * standardValue) + index;
    }

    public float EachChapterAchieventRate(int chapter)
    {
        var count = 0;
        for (int i = chapter * DEFAULT_TOOL_COUNT; i < (chapter + 1) * DEFAULT_TOOL_COUNT; i++)
        {
            if (toolStateDic[i] == STATE_ACTIVATION)
                count++;
        }

        var startNum = (chapter * DEFAULT_FRIEND_COUNT);
        var endNum = ((chapter + 1) * DEFAULT_FRIEND_COUNT);
        var devideNum = 12f;

        if (chapter == 0)
        {
            devideNum--;
            endNum--;
        }

        for (int i = startNum; i < endNum; i++)
        {
            if (friendStateDic[i] == STATE_ACTIVATION)
                count++;
        }
        return count / devideNum;
    }

    public float EachChapterAchieventRate_S(int chapter)
    {
        var count = 0;
        var devideNum = 12f;

        for (int i = 0; i < toolStateDic_S.Count; i++)
        {
            if (toolStateDic_S[i] == STATE_ACTIVATION)
                count++;
        }

        for (int i = 0; i < friendStateDic_S.Count; i++)
        {
            if (friendStateDic_S[i] == STATE_ACTIVATION)
                count++;
        }
        return count / devideNum;
    }
}