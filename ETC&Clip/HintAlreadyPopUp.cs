using System.Collections;
using UnityEngine;

public class HintAlreadyPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Fade());
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Animator>().Rebind();
    }

    public void DisActive()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Animator>().SetTrigger("Fade");
    }
}