using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ToolDBFrame
{
    public int activeToolIndex;
    public long toolKCSV;

    public ToolDBFrame(int activeToolIndex,
        long toolKCSV)
    {
        this.activeToolIndex = activeToolIndex;
        this.toolKCSV = toolKCSV;
    }
}
