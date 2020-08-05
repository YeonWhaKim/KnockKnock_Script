using UnityEngine;
using UnityEngine.SceneManagement;

public class BGDoorKnock : MonoBehaviour
{
    public Animator loadAnim;

    public void OnClickDoor()
    {
        SoundManager.instance.KnockSoundPlay();
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Substring(8).Equals("Special"))
        {
            if (ChapterManager.currChapter_S > 0)
            {
                SceneM.instance.timeAttackChapter = ChapterManager.CURR_SERVICE_CHAPTER + 1;
                SceneM.instance.isSpecialChapter = true;
                SceneM.instance.destinationSceneName = SceneM.TimeAttackScene;
            }
            else
                SceneM.instance.destinationSceneName = SceneM.GameScene_Special;
        }
        else
        {
            if (PolaroidPanelManager.instance.chapter - 1 != ChapterManager.currChapter)
            {
                SceneM.instance.timeAttackChapter = PolaroidPanelManager.instance.chapter;
                SceneM.instance.isSpecialChapter = false;
                SceneM.instance.destinationSceneName = SceneM.TimeAttackScene;
            }
            else
                SceneM.instance.destinationSceneName = SceneM.GameScene;
        }
        loadAnim.SetTrigger("FadeOut");
    }
}