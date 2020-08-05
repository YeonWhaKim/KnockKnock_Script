using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathReader_Intro : MonoBehaviour
{
    public GameObject[] dotanimations;
    public GameObject wayUI;
    public GameObject linePath;
    public DotAnimation_Intro dotAnimation;
    public Animator dotEffect;
    private LineRenderer line;
    public AudioSource knockSound;

    private List<Ways> ways;
    private List<GameObject> targetColliders;
    public Stack<GameObject> connetedPaths;

    private bool isStarted = false;
    private bool isLineStarted = false;
    public bool isIntroTutorialOneLoopDone = false;
    // Start is called before the first frame update
    void Start()
    {
        targetColliders = new List<GameObject>();
        line = linePath.GetComponent<LineRenderer>();
        connetedPaths = new Stack<GameObject>();

        Color c = Color.white;

        line.startColor = c;
        line.endColor = c;

        line.positionCount = 2;
    }

    private bool iSStatingFromRightPosition(Vector2 pos)
    {
        if (isStarted)
        {
            Vector2 pos1 = new Vector2(dotAnimation.transform.position.x, dotAnimation.transform.position.y);
            if(isIntroTutorialOneLoopDone)
                Intro.instance.panel5GO.SetActive(false);
            dotEffect.gameObject.transform.position = pos1;
            dotEffect.Rebind();
            Vector2 pos2 = pos;

            if (Vector2.Distance(pos2, pos1) > 0.3f)
            {
                return false;
            }
            if (dotEffect.gameObject.activeSelf.Equals(false))
                dotEffect.gameObject.SetActive(true);
        }

        return true;
    }

    private void canStartLine(Vector3 pos)
    {
        if (pos == Vector3.zero)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Collider2D[] circles = Physics2D.OverlapCircleAll(pos, 0.1f);

        if (circles != null && circles.Length > 0 && iSStatingFromRightPosition(new Vector2(pos.x, pos.y)))
        {
            targetColliders.Clear();
            StartLine(circles[0].transform.position);
            for (int i = 0; i < circles.Length; i++)
            {
                circles[i].transform.parent.gameObject.SendMessage("childCount", circles[i].gameObject);
            }
        }
    }

    public void touchRightPosition()
    {
    }

    private void StartLine(Vector3 posC)
    {
        isLineStarted = true;
        Vector3 pos = posC;
        pos.z = 0;
        line.SetPosition(0, pos);
    }

    private void MoveLine()
    {
        if (isLineStarted)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            line.SetPosition(1, pos);

            if (!linePath.activeSelf)
            {
                linePath.SetActive(true);
            }
            checkIfColliderCollider(pos);
        }
    }

    private bool checkIfColliderCollider(Vector3 pos)
    {
        Collider2D[] circles = Physics2D.OverlapCircleAll(pos, 0.1f);
        if (circles != null && circles.Length > 0)
        {
            for (int i = 0; i < circles.Length; i++)
            {
                if (targetColliders.Contains(circles[i].gameObject))
                {
                    EndLine();
                    circles[i].gameObject.transform.parent.gameObject.SendMessage("chageColor", circles[i].gameObject);
                    isStarted = true;

                    canStartLine(Vector3.zero);
                    return true;
                }
            }
        }

        return false;
    }

    private void EndLine(bool isRevert = false)
    {
        linePath.SetActive(false);
        knockSound.Play();
        isLineStarted = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            pos.z = 0;

            bool iscanStart = !checkIfColliderCollider(pos);
            if (iscanStart && iSStatingFromRightPosition(new Vector2(pos.x, pos.y)))
            {
                canStartLine(Vector3.zero);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }
        else if (Input.GetMouseButton(0))
        {
            MoveLine();
        }
    }

    public void createWay()
    {
        for (int i = 0; i < ways.Count; i++)
        {
            GameObject go = gameObject.transform.GetChild(i).gameObject;
            Ways way = ways[i];

            go.GetComponent<WaysUI_Intro>().setWayModel(way);
            go.GetComponent<WaysUI_Intro>().createUI();
            go.SetActive(true);
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    public void readJson()
    {
        ways = new List<Ways>();
        string levelPath = string.Format("Package_999/level_1");

        TextAsset file = Resources.Load(levelPath) as TextAsset;
        string dataAsJson = file.ToString();

        string[] jsonObjects = dataAsJson.Trim().Split(new char[] { '=' });

        for (int i = 0; i < jsonObjects.Length; i++)
        {
            Ways way = JsonUtility.FromJson<Ways>(jsonObjects[i]);
            ways.Add(way);
        }
    }

    public void readOtherChild(Object obj)
    {
        if (isLineStarted)
        {
            targetColliders.Add((GameObject)obj);
        }
    }

    public void pushConnectedPath(GameObject go)
    {
        connetedPaths.Push(go);
    }

    public void ShowAnimationOnAllNodes()
    {
        dotAnimation.gameObject.SetActive(false);
        WaysUI_Intro[] allUis = GameObject.FindObjectsOfType<WaysUI_Intro>();
        List<Vector3> dotAnimations = new List<Vector3>();
        var index = 0;
        for (int j = 0; j < allUis.Length; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 pos = allUis[j].childPos(i);
                if (!dotAnimations.Contains(pos))
                {
                    GameObject an = dotanimations[index];
                    index++;
                    an.SetActive(true);
                    an.GetComponent<DotAnimation_Intro>().setTargetScale(2f);
                    an.GetComponent<DotAnimation_Intro>().setEnableAtPosition(an, true, pos);
                    an.GetComponent<DotAnimation_Intro>().scalingSpeed = 2.5f;
                    dotAnimations.Add(pos);
                }
            }
        }
        Intro.instance.WindowClear();
    }
}
