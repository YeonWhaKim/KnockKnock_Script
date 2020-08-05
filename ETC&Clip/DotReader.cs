using UnityEngine;

public class DotReader : MonoBehaviour
{
    public static DotReader instance;

    private void Start()
    {
        instance = this;
    }

    public void ReturnDot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                ObjectPoolManager.instance.Dot_ReturnToPool(transform.GetChild(i).gameObject);
        }
    }
}