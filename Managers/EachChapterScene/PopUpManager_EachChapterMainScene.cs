using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager_EachChapterMainScene : MonoBehaviour
{
    [Serializable]
    public class RatePopUp
    {
        public GameObject GO;
        public Text expText_1;
        public Text expText_2;
        public Text yesText;
        public Text noText;

        public void Set()
        {
            var str = LanguageManager.instance.ReturnMenuPopUpString(7).Split('/');
            expText_1.text = str[0];
            expText_2.text = str[1];
            yesText.text = LanguageManager.instance.ReturnMenuPopUpString(8);
            noText.text = LanguageManager.instance.ReturnMenuPopUpString(9);
        }
    }

    [Serializable]
    public class PolaroidPopUp
    {
        public GameObject GO;
        public Text expText_1;
        public Text expText_2;
        public Text yesText;

        public void Set()
        {
            expText_1.text = LanguageManager.instance.ReturnMenuPopUpString(79);
            expText_2.text = LanguageManager.instance.ReturnMenuPopUpString(80);
            yesText.text = LanguageManager.instance.ReturnMenuPopUpString(81);
        }
    }

    [Serializable]
    public class NoAdsPopUP
    {
        public GameObject GO;
        public Text expText_1;
        public Text expText_2;
        public Text exptext_3;
        public Text yesText;
        public Text noText;

        public void Set()
        {
            expText_1.text = LanguageManager.instance.ReturnMenuPopUpString(69);
            expText_2.text = LanguageManager.instance.ReturnMenuPopUpString(70);
            exptext_3.text = LanguageManager.instance.ReturnMenuPopUpString(78);
            yesText.text = LanguageManager.instance.ReturnMenuPopUpString(71);
            noText.text = LanguageManager.instance.ReturnMenuPopUpString(9);
        }
    }

    public static PopUpManager_EachChapterMainScene instance;

    [SerializeField] public RatePopUp ratePopUp;
    [SerializeField] public PolaroidPopUp polaroidPopUP;
    [SerializeField] public NoAdsPopUP noAdsPopUp;

    private void Start()
    {
        instance = this;
    }

    public void ShowRatePopUp()
    {
        ratePopUp.Set();
        SaveManager.isRatingPopUpShown = true;
        SaveManager.instance.SaveBool("isRatingPopUpShown", SaveManager.isRatingPopUpShown);
        ratePopUp.GO.SetActive(true);
    }

    public void RatePopUp_Yes()
    {
        Application.OpenURL("market://details?id=com.twogediGames.KnockKnock");
        SaveManager.isRatingPopUpYes = true;
        SaveManager.instance.SaveBool("isRatingPopUpYes", SaveManager.isRatingPopUpYes);
    }

    public void ShowAdRemovePopUp()
    {
        noAdsPopUp.Set();
        noAdsPopUp.GO.SetActive(true);

        SaveManager.isAdsPopUpYes = true;
        SaveManager.instance.SaveBool("isAdsPopUpYes", SaveManager.isAdsPopUpYes);
    }

    public void ShowPolaroidPopUp(int currChapter)
    {
        polaroidPopUP.Set();
        polaroidPopUP.GO.SetActive(true);

        SaveManager.polaroidPopUp[currChapter] = true;
        SaveManager.instance.SaveBool(string.Format("polaroidPopUp{0}", currChapter), SaveManager.polaroidPopUp[currChapter]);
    }

    public void ShowPolaroidPopUp()
    {
        polaroidPopUP.Set();
        polaroidPopUP.GO.SetActive(true);

        SaveManager.polaroidPopUp_S = true;
        SaveManager.instance.SaveBool("polaroidPopUp_S", SaveManager.polaroidPopUp_S);
    }
}