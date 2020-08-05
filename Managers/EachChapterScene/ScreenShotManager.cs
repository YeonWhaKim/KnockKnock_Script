using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenShotManager : MonoBehaviour
{
    public static ScreenShotManager instance;
    public Image screenShotImage;
    public Sprite sample;
    public GameObject infoGO;
    public Text expText;
    public Text debugText;
    public GameObject screenShotSrpiteGO;
    public Animator infoAnimator;

    private void Start()
    {
        instance = this;
    }
    public void OnClickInfoButton()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals(SceneM.GameScene))
            expText.text = LanguageManager.instance.ReturnChapterExpString(ChapterManager.currChapter - 1);
        else if (sceneName.Equals(SceneM.GameScene_Special) || sceneName.Equals(SceneM.ChapterSpecial))
            expText.text = LanguageManager.instance.ReturnChapterExpString_S(1);
        else
        {
            var chapterS = sceneName.Substring(sceneName.Length - 1);
            var chapter = int.Parse(chapterS);
            expText.text = LanguageManager.instance.ReturnChapterExpString(chapter - 1);
        }
        infoGO.SetActive(true);
    }

    public void OnClickSaveButton()
    {
        //SaveScreenShot();
    }

    public void OnClickShareButton()
    {
        StartCoroutine(SaveAndShare());
    }

    public void OnClickBackButton()
    {
        StartCoroutine(BackCoroutine());
    }

    public void OnClickInfoBackButton()
    {
        StartCoroutine(InfoBackCoroutine());
    }

    private IEnumerator InfoBackCoroutine()
    {
        infoAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.9f);
        infoGO.SetActive(false);
    }


    private IEnumerator BackCoroutine()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(0.9f);
        gameObject.SetActive(false);
    }

    private IEnumerator SaveAndShare()
    {
        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        var screenShotName = "";
        var sceneName = SceneManager.GetActiveScene().name;
        var chapter = 0;
        var path = "";

        if (sceneName.Equals(SceneM.GameScene))
        {
            chapter = ChapterManager.currChapter;
            screenShotName = string.Format("Chapter{0}ScreenShot_{1}.png", ChapterManager.currChapter, DateTime.Now.ToString("yyMMdd"));
            path = string.Format("ScreenShot/chapter{0}_ScreenShot", chapter);
        }
        else if(sceneName.Equals(SceneM.GameScene_Special) || sceneName.Equals(SceneM.ChapterSpecial))
        {
            screenShotName = string.Format("ChapterSScreenShot_{0}.png", DateTime.Now.ToString("yyMMdd"));
            path = string.Format("ScreenShot/chapterS_ScreenShot");
        }
        else
        {
            var chapterS = sceneName.Substring(sceneName.Length - 1);
            chapter = int.Parse(chapterS);
            screenShotName = string.Format("Chapter{0}ScreenShot_{1}.png", chapter, DateTime.Now.ToString("yyMMdd"));
            path = string.Format("ScreenShot/chapter{0}_ScreenShot", chapter);
        }

        Texture2D texture = null;
        Sprite screenShotSP = Resources.Load<Sprite>(path); // 이때 각 스프라이트는 compression = none, read/write enabled = true
        texture = screenShotSP.texture;  

        byte[] bytes = texture.EncodeToPNG();
        string screenShotPath = Application.persistentDataPath + "/" + screenShotName;
        File.WriteAllBytes(screenShotPath, bytes);

        string shareSubject = "한붓그리기 퍼즐을 풀어 외로운 소녀의 공간을 아름답게 채워보세요.";
        string shareMessage = "https://play.google.com/store/apps/details?id=com.twogediGames.KnockKnock";

        //Create intent for action send
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //put image and string extra
        intentObject.Call<AndroidJavaObject>("setType", "image/png");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text and Image Sharing");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        //create image URI to add it to the intent
        //AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaClass uriClass = new AndroidJavaClass("androidx.core.content.FileProvider");
        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");

        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", screenShotPath);// Set Image Path Here
        AndroidJavaObject stringObject = new AndroidJavaObject("java.lang.String", "com.myproject.project.share.fileprovider");// Set Image Path Here

        //AndroidJavaObject uriObject = uriClass.CallStatic<androidjavaobject>("fromFile", fileObject);

        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("getUriForFile", currentActivity, stringObject, fileObject);

        // string uriPath =  uriObject.Call<string>("getPath");
        bool fileExist = fileObject.Call<bool>("exists");
        Debug.Log("File exist : " + fileExist);
        if (fileExist)
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        currentActivity.Call("startActivity", intentObject);
    }
}