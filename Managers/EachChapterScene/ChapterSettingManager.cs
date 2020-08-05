using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSettingManager : MonoBehaviour
{
    public static ChapterSettingManager instance;
    public List<GameObject> toolList = new List<GameObject>();
    public List<GameObject> friendList = new List<GameObject>();

    private bool allObjectisCollected;

    private void Start()
    {
        allObjectisCollected = true;
        Setting();
        if (instance != null)
            return;
        instance = this;
    }

    public void Setting()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var chapterS = sceneName.Substring(sceneName.Length - 1);
        var selectedChapter = int.Parse(chapterS) - 1;

        var friendIndex = ChapterManager.DEFAULT_FRIEND_COUNT;
        var toolIndex = ChapterManager.DEFAULT_TOOL_COUNT;
        var collectionManager = CollectionManager.instance;
        GameManager.CustomDebug("ChapterSettingManater setting! - currChapter : " + selectedChapter + ", FriendIndex : " + friendIndex + ", ToolIndex : " + toolIndex);

        if (selectedChapter == 0)
            friendIndex -= 1;

        for (int i = 0; i < friendIndex; i++)
        {
            //Debug.Log("friend " + i + "(" + collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_FRIEND_COUNT) + ")" + ":" + CollectionManager.friendStateDic[collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_FRIEND_COUNT)]);
            if (CollectionManager.friendStateDic[collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_FRIEND_COUNT)] == 0)
                friendList[i].SetActive(true);
            else
            {
                friendList[i].SetActive(false);
                allObjectisCollected = false;
            }
        }

        for (int i = 0; i < toolIndex; i++)
        {
            //Debug.Log("tool " + i + "(" + collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_TOOL_COUNT) + ")" + ":" + CollectionManager.toolStateDic[collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_TOOL_COUNT)]);
            if (CollectionManager.toolStateDic[collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_TOOL_COUNT)] == 0)
                toolList[i].SetActive(true);
            else
            {
                toolList[i].SetActive(false);
                //allObjectisCollected = false;
            }
        }

        //if (allObjectisCollected)
        //    qImage.SetActive(false);
        //else
        //    qImage.SetActive(true);
    }
}