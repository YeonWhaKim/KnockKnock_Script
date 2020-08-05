using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolaroidManager_S : MonoBehaviour
{
    public static PolaroidManager_S instance;
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

    private void Start()
    {
        instance = this;
    }

    public void PanelSetting(bool ddddd = false)
    {
        polaButtonPercentImage.fillAmount = CollectionManager.instance.EachChapterAchieventRate_S(ChapterManager.SpecialChapter);
        polaButtonPercentText.text = string.Format("{0}%", (int)(CollectionManager.instance.EachChapterAchieventRate_S(ChapterManager.SpecialChapter) * 100));
        if (ddddd.Equals(false)
            && SaveManager.polaroidPopUp_S.Equals(false)
            && CollectionManager.instance.EachChapterAchieventRate_S(ChapterManager.SpecialChapter)==1
            && ChapterManager.SpecialChapter < ChapterManager.currChapter_S)
            PopUpManager_EachChapterMainScene.instance.ShowPolaroidPopUp();
        collectionText.text = LanguageManager.instance.ReturnMenuPopUpString(55);
        mainChapterText.text = string.Format("{0} S", LanguageManager.instance.ReturnMenuPopUpString(56));
        friendsText.text = LanguageManager.instance.ReturnMenuPopUpString(57);
        toolsText.text = LanguageManager.instance.ReturnMenuPopUpString(58);
        // 친구메뉴 세팅
        for (int i = 0; i < CollectionManager.DEFAULT_FRIEND_COUNT; i++)
        {
            friendImageLists[i].sprite = friendSprites[i];
            friendStateImageLists[i].sprite =
                StateSpriteLists[CollectionManager.friendStateDic_S[i]];
            if (CollectionManager.friendStateDic_S[i] == 0)
            {
                friendNameTextLists[i].text = LanguageManager.instance.ReturnEachLanguage(6, i);
                friendImageLists[i].color = Color.white;
            }
            else
            {
                friendNameTextLists[i].text = string.Format("???");
                friendImageLists[i].color = new Vector4(1f, 0.48f, 0.48f, 0.14f);
            }
        }

        // 도구메뉴 세팅

        for (int i = 0; i < CollectionManager.DEFAULT_TOOL_COUNT; i++)
        {
            toolImageLists[i].sprite = toolSprites[i];
            toolStateImageLists[i].sprite =
                StateSpriteLists[CollectionManager.toolStateDic_S[i]];
            if (CollectionManager.toolStateDic_S[i] == 0)
            {
                toolNameTextLists[i].text = LanguageManager.instance.ReturnEachLanguage(4, i);
                toolImageLists[i].color = Color.white;
            }
            else
            {
                toolNameTextLists[i].text = string.Format("???");
                toolImageLists[i].color = new Vector4(1f, 0.48f, 0.48f, 0.14f);
                if (CollectionManager.toolStateDic_S[i] == 1)
                    toolStateImageLists[i].GetComponent<Animator>().SetTrigger("isAD");
            }
        }
    }

    public void ToolStateAnimation()
    {
        for (int i = 0; i < CollectionManager.DEFAULT_TOOL_COUNT; i++)
        {
            if (CollectionManager.toolStateDic_S[i] == 1)
                toolStateImageLists[i].GetComponent<Animator>().SetTrigger("isAD");
        }
    }

    public void ActiveAdPopupForGettingTool(int index)
    {
        if (CollectionManager.toolStateDic_S[index] ==
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
        else if (CollectionManager.toolStateDic_S[index] ==
            CollectionManager.STATE_ACTIVATION)
            ExpPopUpSetAndActive_Tool(index);
    }

    public void ToolAd()
    {
        SceneM.instance.isSpecialChapter_CallAd = true;
        AdmobController.instance.ShowRewardBasedVideo(AdmobController.CallObject.CHAPTER);
    }

    public void ToolActivation_InCollectionMenu()
    {
        CollectionManager.instance.ToolCollectionSetting_S(0, currSelectedToolIndex);
        PanelSetting();
        MenuManager_ChapterSpecial.instance.PolaroidButtonActive();
        ChapterSettingManager_S.instance.Setting();
        ExpPopUpSetAndActive_Tool(currSelectedToolIndex);
        //Social.ReportProgress(GPGSIds.achievement_gift_for_you, 100f, null);
    }

    public void ExpPopUpSetAndActive_Tool(int index)
    {
        icon.sprite = toolImageLists[index].sprite;
        name.text = toolNameTextLists[index].text;

        exp.text = LanguageManager.instance.ReturnEachLanguage(5, index);
        confirm.text = LanguageManager.instance.ReturnMenuPopUpString(0);
        expPopUp.SetActive(true);
    }

    public void ExpPopUpSetAndActive_Friend(int index)
    {
        if (CollectionManager.friendStateDic_S[index] ==
            CollectionManager.STATE_ACTIVATION)
        {
            icon.sprite = friendImageLists[index].sprite;
            icon.rectTransform.sizeDelta = new Vector2(256, 256);
            name.text = friendNameTextLists[index].text;
            exp.text = LanguageManager.instance.ReturnEachLanguage(7, index);
            confirm.text = LanguageManager.instance.ReturnMenuPopUpString(0);

            expPopUp.SetActive(true);
        }
    }
}