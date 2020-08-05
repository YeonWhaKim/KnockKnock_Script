public class InAppPurchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    public static InAppPurchaser instance;
    public bool isnoadsbuy = false;

    #region 상품ID

    public const string productID = "no_ads";

    #endregion 상품ID

    // Start is called before the first frame update
    private void Start()
    {
        InInitialized();
        if (instance != null)
            return;
        instance = this;
    }

    private void InInitialized()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(productID, ProductType.Consumable, new IDs { { productID, GooglePlay.Name } });

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        Debug.Log("결제 기능 초기화 완료");
        extensions.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
        {
            if (result)
            {
                // This does not mean anything was restored,
                // merely that the restoration process succeeded.
                Debug.Log("거래 복원");
            }
            else
            {
                // Restoration failed.
            }
        });
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log(string.Format("error : {0}", error));
    }

    public void OnClickNoAdsProductBuy()
    {
        if (storeController != null)
            Debug.Log("구매 실패 : 결제 기능 초기화 실패");
        else
            storeController.InitiatePurchase(productID);
    }

    public void BuyProductID(string productID)
    {
        try
        {
            if (storeController != null)
            {
                Product p = storeController.products.WithID(productID);
                if (p != null && p.availableToPurchase)
                    storeController.InitiatePurchase(p);
                else if (p != null && p.hasReceipt)
                    Debug.Log("BuyProductID : Fail. The Non Consumable product has already been bought.");
                else
                    Debug.Log("BuyProductID : Fail. Not purchasing product, either is not found or is not available for purchase");
            }
            else
                Debug.Log("BuyProductID : Fail. Not initialized.");
        }
        catch (System.Exception e)
        {
            Debug.Log(string.Format("BuyProductID : Fail. Exception during purchase. - error : {0}", e));
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        bool isSuccess = true;
        //#if UNITY_ANDROID && !UNITY_EDITOR
        CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
        try
        {
            IPurchaseReceipt[] result = validator.Validate(e.purchasedProduct.receipt);
            for (int i = 0; i < result.Length; i++)
            {
                Analytics.Transaction(result[i].productID, e.purchasedProduct.metadata.localizedPrice,
                    e.purchasedProduct.metadata.isoCurrencyCode,
                    result[i].transactionID, null);
            }
        }
        catch (IAPSecurityException)
        {
            isSuccess = false;
        }
        //#endif
        if (isSuccess)
        {
            Debug.Log("구매 완료");
            BuyNoAds();
        }
        else
            Debug.Log("구매 실패 : 비정상 결제");

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        throw new System.NotImplementedException();
    }

    public void BuyNoAds()
    {
        SceneM.isVIP = true;
        SaveManager.instance.SaveBool("isVIP", SceneM.isVIP);
        CUtils.CloseBannerAd();
        if (MenuManager_EachChapterMainScene.instance != null)
            MenuManager_EachChapterMainScene.instance.noAdsButtonGO.SetActive(false);
        if (HomeSceneManager.instance != null)
            HomeSceneManager.instance.noadsButton.SetActive(false);

        PlayerData.instance.NumberOfHints += 3;
        PlayerData.instance.SaveData();
        isnoadsbuy = true;
        SettingManager.instance.CloudSave_YES();
    }
}