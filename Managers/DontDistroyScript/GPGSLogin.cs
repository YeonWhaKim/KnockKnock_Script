using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GPGSLogin : MonoBehaviour
{
    public static GPGSLogin instance;
    public bool onSaving = false;
    public bool onLoading = false;
    public Text text;
    public Text savingText;
    public Text loadingText;
    public Button[] cloudPanelButtons;
    private string saveData;
    public string leaderBoardID;

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;
#if UNITY_ANDROID && !UNITY_EDITOR
        StartCoroutine(Init());
#endif
    }

    public IEnumerator Init()
    {
        if (!CheckNetWorkState())
            yield break;
        PlayGamesClientConfiguration config =
            new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .RequestServerAuthCode(false)
            .Build();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        yield return new WaitForEndOfFrame();
        Login();
    }

    public void Login()
    {
        if (Social.localUser.authenticated)
            return;
        Social.localUser.Authenticate((bool success) =>
        {
            if (!success)
                DontDestroyPopUpManager.instance.ShowLoginFailedPopUp();
            else
            {
                Social.ReportProgress(GPGSIds.achievement_first_login, 100f, null);
            }
        });
    }

    public void SignOut()
    {
        // 로그인 상태면 호출
        if (PlayGamesPlatform.Instance.IsAuthenticated() == true)
        {
            PlayGamesPlatform.Instance.SignOut();
        }
    }

    public bool CheckLogin()
    {
        return Social.localUser.authenticated;
    }

    public bool CheckNetWorkState()
    {
        /// 1. 인터넷 연결 확인.
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Change the Text
            DontDestroyPopUpManager.instance.ShowNetworkState();
            return false;
        }
        return true;
    }

    #region Save

    public void SaveToCloud(string data)
    {
        if (!CheckNetWorkState())
            return;
        StartCoroutine(Save(data));
    }

    private IEnumerator Save(string data)
    {
        // 저장할 준비 - 로그인 확인, 저장할 데이터 세팅, 저장파일이름 정립
        Debug.Log("Try to save data");

        if (CheckLogin() == false)
        {
            StartCoroutine(Init());
            yield return new WaitForSeconds(2f);
            if (CheckLogin() == false)
            {
                DontDestroyPopUpManager.instance.ShowLoginFailedPopUp();
                DontDestroyPopUpManager.instance.saveClear.go.SetActive(false);
                DontDestroyPopUpManager.instance.savePopUp.go.SetActive(false);
                yield break;
            }
        }
        onSaving = true;
        StartCoroutine(IngTextCo(true, LanguageManager.instance.ReturnMenuPopUpString(46), savingText));
        string id = Social.localUser.id;
        string fileName = string.Format("{0}_DATA", id);
        saveData = data;

        OpenSavedGame(fileName, true);
    }

    private void OpenSavedGame(string fileName, bool saved)
    {
        // 저장된 게임을 열때, 읽을건지 또 저장할 건지
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;
        if (saved == true)
            savedClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave);
        else
            savedClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead);
    }

    private void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        // 요청에 성공하게 되면 byte배열로 변환해서 현재 시간과 함께 넘겨준다
        if (status == SavedGameRequestStatus.Success)
        {
            byte[] b = Encoding.UTF8.GetBytes(string.Format(saveData));
            SaveGame(data, b, DateTime.Now.TimeOfDay);
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    private void SaveGame(ISavedGameMetadata data, byte[] _byte, TimeSpan playTime)
    {
        // 저장된 게임 열어서 시간비교하고
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder.WithUpdatedPlayedTime(playTime).WithUpdatedDescription("saved at " + DateTime.Now);
        SaveManager.cloudSaveDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SaveManager.instance.SaveString("cloudSaveDate", SaveManager.cloudSaveDate);

        SavedGameMetadataUpdate updatedData = builder.Build();
        // 데이터들을 Commit해줌
        savedClient.CommitUpdate(data, updatedData, _byte, OnSavedGameWritten);
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        // 그냥 저장 성공여부 콜백 함수
        if (status == SavedGameRequestStatus.Success)
        {
            onSaving = false;
            Debug.Log("Save Complete");
            if (InAppPurchaser.instance.isnoadsbuy.Equals(false))
                DontDestroyPopUpManager.instance.ShowSaveClear();
            InAppPurchaser.instance.isnoadsbuy = false;
        }
        else
        {
            Debug.Log("Save Fail");
        }
    }

    private IEnumerator IngTextCo(bool isSaving, string oriString, Text text)
    {
        if (isSaving)
        {
            cloudPanelButtons[0].interactable = false;
            cloudPanelButtons[1].interactable = false;
            while (onSaving)
            {
                text.text = string.Format("{0}.", oriString);
                yield return new WaitForSeconds(1f);
                text.text = string.Format("{0}..", oriString);
                yield return new WaitForSeconds(1f);
                text.text = string.Format("{0}...", oriString);
                yield return new WaitForSeconds(1f);
            }
            cloudPanelButtons[0].interactable = true;
            cloudPanelButtons[1].interactable = true;
        }
        else
        {
            cloudPanelButtons[2].interactable = false;
            cloudPanelButtons[3].interactable = false;

            while (onLoading)
            {
                text.text = string.Format("{0}.", oriString);
                yield return new WaitForSeconds(1f);
                text.text = string.Format("{0}..", oriString);
                yield return new WaitForSeconds(1f);
                text.text = string.Format("{0}...", oriString);
                yield return new WaitForSeconds(1f);
            }
            cloudPanelButtons[2].interactable = true;
            cloudPanelButtons[3].interactable = true;
        }
        text.text = "";
    }

    #endregion Save

    #region Load

    public void LoadFromCloud()
    {
        if (!CheckNetWorkState())
            return;
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        onLoading = true;
        StartCoroutine(IngTextCo(false, LanguageManager.instance.ReturnMenuPopUpString(47), loadingText));
        if (CheckLogin() == false)
        {
            StartCoroutine(Init());
            yield return new WaitForSeconds(2f);
            if (CheckLogin() == false)
            {
                DontDestroyPopUpManager.instance.ShowLoginFailedPopUp();
                DontDestroyPopUpManager.instance.loadClear.go.SetActive(false);
                DontDestroyPopUpManager.instance.loadPopUp.go.SetActive(false);
                yield break;
            }
        }
        Debug.Log("Try to load data");

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_DATA", id);

        OpenSavedGame(fileName, false);
    }

    private void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        if (status == SavedGameRequestStatus.Success)
            LoadGameData(data);
        else
            Debug.Log("Fail");
    }

    private void LoadGameData(ISavedGameMetadata data)
    {
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;
        savedClient.ReadBinaryData(data, OnSavedGameDataRead);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] _byte)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            onLoading = false;
            string data = Encoding.Default.GetString(_byte);
            // 여기서 data load해서 세팅하는걸로 보내고
            // 팝업도 거기서 띄우기
            LoadManager.instance.DataSettingFromCloudData(data);
            DontDestroyPopUpManager.instance.ShowLoadClear();
            Debug.Log("Load Success");
        }
        else
            Debug.Log("Load Fail");
    }

    #endregion Load

    public void ReportLeaderBoard(int bestScore)
    {
        LoadLeaderBoardScore(SceneM.instance.timeAttackChapter - 1);
        if (CheckNetWorkState())
        {
            if (CheckLogin())
            {
                Social.ReportScore(bestScore, leaderBoardID, (bool success) =>
                {
                    Debug.Log("score : " + bestScore);
                });
            }
        }
    }

    public string LoadLeaderBoardScore(int chapter)
    {
        leaderBoardID = "";
        string myBestScore = "0";

        switch (chapter)
        {
            case 0:
                leaderBoardID = GPGSIds.leaderboard_chapter_1_ranking;
                break;

            case 1:
                leaderBoardID = GPGSIds.leaderboard_chapter_2_ranking;
                break;

            case 2:
                leaderBoardID = GPGSIds.leaderboard_chapter_3_ranking;
                break;

            case 3:
                leaderBoardID = GPGSIds.leaderboard_chapter_4_ranking;
                break;

            case 4:
                leaderBoardID = GPGSIds.leaderboard_chapter_5_ranking;
                break;

            default:
                leaderBoardID = GPGSIds.leaderboard_chapter_s_ranking;
                break;
        }
        leaderBoardID = leaderBoardID.Trim();
        Social.LoadScores(leaderBoardID, scores =>
        {
            if (scores.Length > 0)
            {
                foreach (IScore score in scores)
                {
                    if (score.userID.Equals(Social.localUser.id))
                    {
                        myBestScore = score.formattedValue;
                    }
                }
            }
            else
                Debug.Log("No scores loaded");

            TimeAttackManager.instance.isLoadScore = true;
            TimeAttackManager.instance.BestScoreSet(myBestScore);
        });

        return myBestScore;
    }
}