using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEffectSensorCreate : MonoBehaviour, IPointerClickHandler
{
    private Vector3 pos;

    private void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Screen.width * 2, Screen.height * 2);
        gameObject.GetComponent<RectTransform>().localPosition =
            new Vector3(Screen.width * -0.5f, Screen.height * -0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pos = eventData.position;
        ShowTouchEffect();
    }

    public void ShowTouchEffect()
    {
        var item = TouchEffectObjectPool.instance.PopFromPool();
        item.GetComponent<RectTransform>().localPosition = pos;
    }
}