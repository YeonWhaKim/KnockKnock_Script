using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerSet : MonoBehaviour
{
    public CanvasScaler cs;

    private void Start()
    {
        cs.referenceResolution = new Vector2(Screen.width, Screen.height);
    }
}