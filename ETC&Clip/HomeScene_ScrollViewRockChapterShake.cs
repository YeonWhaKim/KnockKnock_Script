using UnityEngine;

public class HomeScene_ScrollViewRockChapterShake : MonoBehaviour
{
    public Animator imageAnimator;

    public void OnClickRockChapter()
    {
        imageAnimator.SetTrigger("Shake");
    }
}