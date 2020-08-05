using System;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyPopUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Serializable]
    public class SavePopUp
    {
        public GameObject go;
        public Text exp;
        public Text yes;
        public Text no;

        public void Set()
        {
            exp.text = LanguageManager.instance.ReturnMenuPopUpString(17);
            yes.text = LanguageManager.instance.ReturnMenuPopUpString(18);
            no.text = LanguageManager.instance.ReturnMenuPopUpString(19);
        }
    }

    [Serializable]
    public class SaveClear
    {
        public GameObject go;
        public Text exp;
        public Text yes;

        public void Set()
        {
            exp.text = LanguageManager.instance.ReturnMenuPopUpString(20);
            yes.text = LanguageManager.instance.ReturnMenuPopUpString(21);
        }
    }

    [Serializable]
    public class LoadPopUp
    {
        public GameObject go;
        public Text exp;
        public Text date;
        public Text yes;
        public Text no;

        public void Set()
        {
            exp.text = LanguageManager.instance.ReturnMenuPopUpString(22);
            date.text = SaveManager.cloudSaveDate;
            yes.text = LanguageManager.instance.ReturnMenuPopUpString(23);
            no.text = LanguageManager.instance.ReturnMenuPopUpString(24);
        }
    }

    [Serializable]
    public class LoadClear
    {
        public GameObject go;
        public Text exp;
        public Text yes;

        public void Set()
        {
            exp.text = LanguageManager.instance.ReturnMenuPopUpString(25);
            yes.text = LanguageManager.instance.ReturnMenuPopUpString(26);
        }
    }

    [Serializable]
    public class NetworkState
    {
        public GameObject go;
        public Text exp;
        public Text yes;

        public void Set()
        {
            exp.text = LanguageManager.instance.ReturnMenuPopUpString(33);
            yes.text = LanguageManager.instance.ReturnMenuPopUpString(34);
        }
    }

    [Serializable]
    public class LoginFailedPopUp
    {
        public GameObject GO;
        public Text expText;
        public Text okText;

        public void Set()
        {
            expText.text = LanguageManager.instance.ReturnMenuPopUpString(48);
            okText.text = LanguageManager.instance.ReturnMenuPopUpString(21);
        }
    }

    [Serializable]
    public class NetWorkNotConnectedAdBanner
    {
        public GameObject GO;
        public Text expText;

        public void Set()
        {
            expText.text = LanguageManager.instance.ReturnMenuPopUpString(61);
        }
    }

    [Serializable]
    public class TimeAttackUnLockPopUp
    {
        public GameObject GO;
        public Text expText;
        public Text yesText;

        public void Set(bool isSpecialChapter)
        {
            if (isSpecialChapter)
                expText.text = string.Format(LanguageManager.instance.ReturnMenuPopUpString(74), "Special");
            else
                expText.text = string.Format(LanguageManager.instance.ReturnMenuPopUpString(74), ChapterManager.currChapter);

            yesText.text = LanguageManager.instance.ReturnMenuPopUpString(26);
        }
    }

    [Serializable]
    public class AdRemovePopUp_Public
    {
        public GameObject GO;
        public Text expText;
        public Text expText2;
        public Text yesText;
        public Text noText;

        public void Set()
        {
            expText.text = LanguageManager.instance.ReturnMenuPopUpString(70);
            expText2.text = LanguageManager.instance.ReturnMenuPopUpString(78);
            yesText.text = LanguageManager.instance.ReturnMenuPopUpString(71);
            noText.text = LanguageManager.instance.ReturnMenuPopUpString(9);
        }
    }

    public static DontDestroyPopUpManager instance;
    [SerializeField] public SavePopUp savePopUp;
    [SerializeField] public SaveClear saveClear;
    [SerializeField] public LoadPopUp loadPopUp;
    [SerializeField] public LoadClear loadClear;
    [SerializeField] public NetworkState networkState;
    [SerializeField] public LoginFailedPopUp loginFailedPopUp;
    [SerializeField] public NetWorkNotConnectedAdBanner adBannerPopUp;
    [SerializeField] public TimeAttackUnLockPopUp timeAttackUnLockPopUp;
    [SerializeField] public AdRemovePopUp_Public adRemovePopUp_Public;

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public void ShowSavePopUp()
    {
        savePopUp.Set();
        savePopUp.go.SetActive(true);
    }

    public void ShowSaveClear()
    {
        saveClear.Set();
        saveClear.go.SetActive(true);
    }

    public void ShowLoadPopUp()
    {
        loadPopUp.Set();
        loadPopUp.go.SetActive(true);
    }

    public void ShowLoadClear()
    {
        loadClear.Set();
        loadClear.go.SetActive(true);
    }

    public void ShowNetworkState()
    {
        networkState.Set();
        networkState.go.SetActive(true);
    }

    public void ShowLoginFailedPopUp()
    {
        loginFailedPopUp.Set();
        loginFailedPopUp.GO.SetActive(true);
    }

    public void ShowNotConnectedNetworkAdBanner()
    {
        adBannerPopUp.Set();
        adBannerPopUp.GO.SetActive(true);
    }

    public void ShowTimeAttackUnLockPopUp(bool isSpecialChpater)
    {
        timeAttackUnLockPopUp.Set(isSpecialChpater);
        timeAttackUnLockPopUp.GO.SetActive(true);
    }

    public void ShowAdremovePublicPopUp()
    {
        adRemovePopUp_Public.Set();
        adRemovePopUp_Public.GO.SetActive(true);
    }

    public void BuyNoAds()
    {
        InAppPurchaser.instance.BuyProductID(InAppPurchaser.productID);
    }
}