using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene_MainCamera : MonoBehaviour
{
    private string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public void DoorTouch()
    {
        if (sceneName.Equals("Game") || sceneName.Equals("TimeAttack"))
        {
            KnockKnock.instance.girltouchAnim.speed = 1.5f;
            KnockKnock.instance.girltouchAnim.SetTrigger("Touch");
        }
        else
        {
            KnockKnock_S.instance.girltouchAnim.speed = 1.5f;
            KnockKnock_S.instance.girltouchAnim.SetTrigger("Touch");
        }
    }

    public void KnockSound()
    {
        SoundManager.instance.KnockSoundPlay();
    }

    public void TouchAndSound()
    {
        if (sceneName.Equals("Special"))
        {
            KnockKnock_S.instance.girltouchAnim.speed = 1.5f;
            KnockKnock_S.instance.girltouchAnim.SetTrigger("Touch");
        }
        else
        {
            KnockKnock.instance.girltouchAnim.speed = 1.5f;
            KnockKnock.instance.girltouchAnim.SetTrigger("Touch");
        }
        SoundManager.instance.KnockSoundPlay();
    }

    public void PathReaderFadeIn()
    {
        if (sceneName.Equals("Special"))
            EffectManager_S.instance.PathReaderFadeIn();
        else
            EffectManager.instance.PathReaderFadeIn();
    }
}