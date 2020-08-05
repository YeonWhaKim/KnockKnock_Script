using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Animator gameSceneCameraAnimator;

    public void LoadScene()
    {
        SceneM.instance.LoadScene(SceneM.instance.destinationSceneName);
    }

    public void GameSceneCameraZoom()
    {
        if (gameSceneCameraAnimator != null)
            gameSceneCameraAnimator.SetTrigger("Start");
    }
}