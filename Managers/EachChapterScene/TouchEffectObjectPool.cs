using System.Collections.Generic;
using UnityEngine;

public class TouchEffectObjectPool : MonoBehaviour
{
    public static TouchEffectObjectPool instance;
    public string touchEffectItemName = "TouchEffect";
    public Transform touchEffectParent;
    public GameObject touchEffectPrefab;
    public int poolCount = 15;

    private List<GameObject> touchEffectPoolList = new List<GameObject>();

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;
        Initialized();
    }

    public void Initialized()
    {
        for (int i = 0; i < touchEffectParent.childCount; i++)
        {
            //touchEffectPoolList.Add(CreateItem());
            touchEffectPoolList.Add(touchEffectParent.GetChild(i).gameObject);
        }
    }

    public GameObject CreateItem()
    {
        var item = Instantiate(touchEffectPrefab);
        item.transform.name = touchEffectItemName;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.parent = touchEffectParent;
        item.gameObject.SetActive(false);

        return item;
    }

    public GameObject PopFromPool()
    {
        if (touchEffectPoolList.Count == 0)
            touchEffectPoolList.Add(CreateItem());
        var item = touchEffectPoolList[0];
        item.SetActive(true);
        touchEffectPoolList.RemoveAt(0);

        return item;
    }

    public void ReturnToPool(GameObject item)
    {
        item.transform.SetParent(touchEffectParent);
        item.SetActive(false);
        touchEffectPoolList.Add(item);
    }
}