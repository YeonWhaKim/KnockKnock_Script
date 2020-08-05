using System.Collections.Generic;
using UnityEngine;

public class ChapterSettingManager_S : MonoBehaviour
{
    public static ChapterSettingManager_S instance;
    public List<GameObject> toolList = new List<GameObject>();
    public List<GameObject> friendList = new List<GameObject>();

    private bool allObjectisCollected;

    private void Start()
    {
        instance = this;
        allObjectisCollected = true;
        Setting();
        if (instance != null)
            return;
    }

    public void Setting()
    {
        var friendIndex = ChapterManager.DEFAULT_FRIEND_COUNT;
        var toolIndex = ChapterManager.DEFAULT_TOOL_COUNT;
        var collectionManager = CollectionManager.instance;

        for (int i = 0; i < friendIndex; i++)
        {
            //Debug.Log("friend " + i + "(" + collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_FRIEND_COUNT) + ")" + ":" + CollectionManager.friendStateDic[collectionManager.OneRayValue(selectedChapter, i, ChapterManager.DEFAULT_FRIEND_COUNT)]);
            if (CollectionManager.friendStateDic_S[collectionManager.OneRayValue(ChapterManager.SpecialChapter, i, ChapterManager.DEFAULT_FRIEND_COUNT)] == 0)
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
            if (CollectionManager.toolStateDic_S[collectionManager.OneRayValue(ChapterManager.SpecialChapter, i, ChapterManager.DEFAULT_TOOL_COUNT)] == 0)
                toolList[i].SetActive(true);
            else
            {
                toolList[i].SetActive(false);
                //allObjectisCollected = false;
            }
        }
    }
}