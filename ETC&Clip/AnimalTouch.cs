using UnityEngine;

public class AnimalTouch : MonoBehaviour
{
    public Animator animator;

    public void OnTouch(int index)
    {
        animator.SetTrigger("Touch");
        if (index > 8)
            return;
        SoundManager.instance.FriendSound(index);
    }

    public void AnimClip()
    {
        //animator.SetBool("Touch", false);
    }

}