using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PolaroidPanelManager : MonoBehaviour
{
    public static PolaroidPanelManager instance;
    public Text mainChapterText;
    public List<Image> toolImageLists;
    public List<Image> toolStateImageLists;
    public List<Text> toolNameTextLists;

    public List<Image> friendImageLists;
    public List<Image> friendStateImageLists;
    public List<Text> friendNameTextLists;

    public List<Sprite> toolSprites = new List<Sprite>();
    public List<Sprite> friendSprites = new List<Sprite>();
    public List<Sprite> StateSpriteLists = new List<Sprite>();

    public Image polaButtonPercentImage;
    public Text polaButtonPercentText;
    public GameObject adPopup;
    public Image adPopUpImage;
    public Text expText;
    public Text yesText;
    public Text noText;

    public Text collectionText;
    public Text friendsText;
    public Text toolsText;

    [Header("ExpPopUp")]
    public GameObject expPopUp;

    public Image icon;
    public Text name;
    public Text exp;
    public Text confirm;

    private int currSelectedToolIndex = 0;
    public int chapter = 0;

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public void PanelSetting()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var chapterS = sceneName.Substring(sceneName.Length - 1);
        chapter = int.Parse(chapterS);
        polaButtonPercentImage.fillAmount = CollectionManager.instance.EachChapterAchieventRate(chapter - 1);
        polaButtonPercentText.text = string.Format("{0}%", (int)(CollectionManager.instance.EachChapterAchieventRate(chapter - 1) * 100));

        if (SaveManager.polaroidPopUp[chapter - 1].Equals(false)
            && CollectionManager.instance.EachChapterAchieventRate(chapter - 1) == 1
            && chapter <= ChapterManager.currChapter)
            PopUpManager_EachChapterMainScene.instance.ShowPolaroidPopUp(chapter - 1);

        if (chapter > ChapterManager.CURR_SERVICE_CHAPTER)
            return;
        collectionText.text = LanguageManager.instance.ReturnMenuPopUpString(55);
        mainChapterText.text = string.Format("{0} {1}", LanguageManager.instance.ReturnMenuPopUpString(56), chapter.ToString());
        friendsText.text = LanguageManager.instance.ReturnMenuPopUpString(57);
        toolsText.text = LanguageManager.instance.ReturnMenuPopUpString(58);
        // 친구메뉴 세팅
        var friendChaToIndex = (chapter - 1) * CollectionManager.DEFAULT_FRIEND_COUNT;
        for (int i = 0; i < CollectionManager.DEFAULT_FRIEND_COUNT; i++)
        {
            friendImageLists[i].sprite = friendSprites[i];
            friendStateImageLists[i].sprite =
                StateSpriteLists[CollectionManager.friendStateDic[i + friendChaToIndex]];
            if (CollectionManager.friendStateDic[i + friendChaToIndex] == 0)
            {
                friendNameTextLists[i].text = LanguageManager.instance.ReturnEachLanguage(2, i + friendChaToIndex);
                friendImageLists[i].color = Color.white;
            }
            else
            {
                friendNameTextLists[i].text = string.Format("???");
                friendImageLists[i].color = new Vector4(1f, 0.48f, 0.48f, 0.14f);
            }
        }

        // 도구메뉴 세팅
        var toolChaToIndex = (chapter - 1) * CollectionManager.DEFAULT_TOOL_COUNT;

        for (int i = 0; i < CollectionManager.DEFAULT_TOOL_COUNT; i++)
        {
            toolImageLists[i].sprite = toolSprites[i];
            toolStateImageLists[i].sprite =
                StateSpriteLists[CollectionManager.toolStateDic[i + toolChaToIndex]];
            if (CollectionManager.toolStateDic[i + toolChaToIndex] == 0)
            {
                toolNameTextLists[i].text = LanguageManager.instance.ReturnEachLanguage(0, i + toolChaToIndex);
                toolImageLists[i].color = Color.white;
            }
            else
            {
                toolNameTextLists[i].text = string.Format("???");
                toolImageLists[i].color = new Vector4(1f, 0.48f, 0.48f, 0.14f);
                if (CollectionManager.toolStateDic[i + toolChaToIndex] == 1)
                    toolStateImageLists[i].GetComponent<Animator>().SetTrigger("isAD");
            }
        }
    }

    public void ToolStateAnimation()
    {
        var toolChaToIndex = (chapter - 1) * CollectionManager.DEFAULT_TOOL_COUNT;

        for (int i = 0; i < CollectionManager.DEFAULT_TOOL_COUNT; i++)
        {
            if (CollectionManager.toolStateDic[i + toolChaToIndex] == 1)
                toolStateImageLists[i].GetComponent<Animator>().SetTrigger("isAD");
        }
    }

    public void ActiveAdPopupForGettingTool(int index)
    {
        var real_toolChaToIndex = ((chapter - 1) * CollectionManager.DEFAULT_TOOL_COUNT) + index;
        if (CollectionManager.toolStateDic[real_toolChaToIndex] ==
            CollectionManager.STATE_AD)
        {
            if (SceneM.isVIP)
            {
                currSelectedToolIndex = index;
                ToolActivation_InCollectionMenu();
            }
            else
            {
                currSelectedToolIndex = index;
                expText.text = LanguageManager.instance.ReturnMenuPopUpString(1);
                yesText.text = LanguageManager.instance.ReturnMenuPopUpString(2);
                noText.text = LanguageManager.instance.ReturnMenuPopUpString(3);
                adPopUpImage.sprite = toolImageLists[index].sprite;
                adPopup.SetActive(true);
            }
        }
        else if (CollectionManager.toolStateDic[real_toolChaToIndex] ==
            CollectionManager.STATE_ACTIVATION)
            ExpPopUpSetAndActive_Tool(index);
    }

    public void ToolAd()
    {
        SceneM.instance.isSpecialChapter_CallAd = false;
        AdmobController.instance.ShowRewardBasedVideo(AdmobController.CallObject.CHAPTER);
    }

    public void ToolActivation_InCollectionMenu()
    {
        CollectionManager.instance.ToolCollectionSetting(chapter - 1, currSelectedToolIndex);
        PanelSetting();
        MenuManager_EachChapterMainScene.instance.PolaroidButtonActive();
        ChapterSettingManager.instance.Setting();
        ExpPopUpSetAndActive_Tool(currSelectedToolIndex);
        //Social.ReportProgress(GPGSIds.achievement_gift_for_you, 100f, null);
    }

    public void ExpPopUpSetAndActive_Tool(int index)
    {
        icon.sprite = toolImageLists[index].sprite;
        name.text = toolNameTextLists[index].text;

        var real_toolChaToIndex = ((chapter - 1) * CollectionManager.DEFAULT_TOOL_COUNT) + index;
        exp.text = LanguageManager.instance.ReturnEachLanguage(1, real_toolChaToIndex);
        confirm.text = LanguageManager.instance.ReturnMenuPopUpString(0);
        expPopUp.SetActive(true);
    }

    public void ExpPopUpSetAndActive_Friend(int index)
    {
        var real_friendChaToIndex = ((chapter - 1) * CollectionManager.DEFAULT_FRIEND_COUNT) + index;
        if (CollectionManager.friendStateDic[real_friendChaToIndex] ==
            CollectionManager.STATE_ACTIVATION)
        {
            icon.sprite = friendImageLists[index].sprite;
            icon.rectTransform.sizeDelta = new Vector2(256, 256);
            name.text = friendNameTextLists[index].text;
            exp.text = LanguageManager.instance.ReturnEachLanguage(3, real_friendChaToIndex);
            confirm.text = LanguageManager.instance.ReturnMenuPopUpString(0);

            expPopUp.SetActive(true);
        }
    }
}