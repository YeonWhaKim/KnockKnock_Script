using UnityEngine;

public class DotAnimation_Intro : MonoBehaviour
{
    public static DotAnimation_Intro instance;
    public float fadingSpeed;
    public float scalingSpeed = 1.5f;

    private bool isEnabled = false;
    private bool prvState = false;
    private SpriteRenderer sp;

    private const float startScale = 0.6f;
    private float scale;
    private float targetScale = 2f;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        instance = this;
        ResetForRestart(false);
    }

    public void ResetForRestart(bool available)
    {
        Color c = sp.color;
        c.a = 0f;
        sp.color = c;
        if (available)
            setEnableAtPosition(gameObject, false, new Vector3(0, 0, 0));
    }

    public void setEnableAtPosition(GameObject dotObject, bool enable, Vector3 pos)
    {
        prvState = isEnabled;

        dotObject.transform.position = pos;
        scale = startScale;
        dotObject.transform.localScale = new Vector3(scale, scale);

        isEnabled = enable;

        if (sp.Equals(null))
            sp = sp = GetComponent<SpriteRenderer>();
        sp.color = ThemeChanger_Intro.current.dotColor;
        Color c = sp.color;

        if (isEnabled)
        {
            c.a = 1;
        }
        else
        {
            c.a = 0;
        }

        sp.color = c;
    }

    public void revertPrvState()
    {
        isEnabled = prvState;
        scale = startScale;
        transform.localScale = new Vector3(scale, scale);

        if (!isEnabled)
        {
            Color c = sp.color;
            c.a = 0f;
            sp.color = c;
        }
    }

    private void Update()
    {
        if (isEnabled)
        {
            scaleChange();
        }
    }

    public void setTargetScale(float target)
    {
        targetScale = target;
    }

    private void scaleChange()
    {
        scale += Time.deltaTime * scalingSpeed;
        if (scale > targetScale) scale = startScale;

        float alpha = targetScale - scale;
        changeAlpha(alpha);

        transform.localScale = new Vector3(scale, scale);
    }

    private void changeAlpha(float alpha)
    {
        alpha = alpha / targetScale;
        Color c = sp.color;
        c.a = alpha;
        sp.color = c;
    }
}
