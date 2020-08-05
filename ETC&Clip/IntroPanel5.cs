using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanel5 : MonoBehaviour
{
    public PathReader_Intro pi;

    public void OneLoopDone()
    {
        pi.isIntroTutorialOneLoopDone = true;
    }
}
