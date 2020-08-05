using System.Collections.Generic;
using UnityEngine;

public class DataSplit : MonoBehaviour
{
    public static DataSplit instance;
    private char[] enter = new char[] { '\n' };
    private char[] enter_multiLine = new char[] { '_' };
    public static List<List<ToolDBFrame>> toolDBLists = new List<List<ToolDBFrame>>();
    public static List<ToolDBFrame> toolDefaultValueData_Special = new List<ToolDBFrame>();

    public List<ToolDBFrame> toolDefaultValueData_Chap1 = new List<ToolDBFrame>();
    public List<ToolDBFrame> toolDefaultValueData_Chap2 = new List<ToolDBFrame>();
    public List<ToolDBFrame> toolDefaultValueData_Chap3 = new List<ToolDBFrame>();
    public List<ToolDBFrame> toolDefaultValueData_Chap4 = new List<ToolDBFrame>();
    public List<ToolDBFrame> toolDefaultValueData_Chap5 = new List<ToolDBFrame>();

    public static List<GrobalLanFrame> toolName = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> toolExp = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> friendName = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> friendExp = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> menuPopUpString = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> chapterExp = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> chapterName = new List<GrobalLanFrame>();

    public static List<GrobalLanFrame> toolName_special = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> toolExp_special = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> friendName_special = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> friendExp_special = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> chapterExp_special = new List<GrobalLanFrame>();
    public static List<GrobalLanFrame> chapterName_special = new List<GrobalLanFrame>();

    public static List<GrobalLanFrame> specialChapterScript = new List<GrobalLanFrame>();

    private void Awake()
    {
        instance = this;
        ToolDefaultValueDataSplit_Chap1();
        ToolDefaultValueDataSplit_Chap2();
        ToolDefaultValueDataSplit_Chap3();
        ToolDefaultValueDataSplit_Chap4();
        ToolDefaultValueDataSplit_Chap5();
        ToolDefaultValueDataSplit_Special();

        ToolNameDataSplit();
        ToolNameDataSplit_Special();
        ToolExpDataSplit();
        ToolExpDataSplit_Special();
        FriendNameDataSplit();
        FriendNameDataSplit_Special();
        FriendExpDataSplit();
        FriendExpDataSplit_Special();
        SpecialChapterScriptDAtaSplit();
        MenuPopUpStringDataSplit();
        ChapterExpDataSplit();
        ChapterExpDataSplit_Special();
        ChapterNameDataSplit();
        ChapterNameDataSplit_Special();
    }

    private void ToolDefaultValueDataSplit_Chap1()
    {
        var toolStr = Resources.Load<TextAsset>("Tool_KCSV_Chap1");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        //var toolStrSplit = removeR.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');

            var newData = new ToolDBFrame(int.Parse(str[0]), long.Parse(str[1]));
            toolDefaultValueData_Chap1.Add(newData);
        }
        toolDBLists.Add(toolDefaultValueData_Chap1);
    }

    private void ToolDefaultValueDataSplit_Chap2()
    {
        var toolStr = Resources.Load<TextAsset>("Tool_KCSV_Chap2");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        //var toolStrSplit = removeR.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');

            var newData = new ToolDBFrame(int.Parse(str[0]), long.Parse(str[1]));
            toolDefaultValueData_Chap2.Add(newData);
        }
        toolDBLists.Add(toolDefaultValueData_Chap2);
    }

    private void ToolDefaultValueDataSplit_Chap3()
    {
        var toolStr = Resources.Load<TextAsset>("Tool_KCSV_Chap3");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        //var toolStrSplit = removeR.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');

            var newData = new ToolDBFrame(int.Parse(str[0]), long.Parse(str[1]));
            toolDefaultValueData_Chap3.Add(newData);
        }
        toolDBLists.Add(toolDefaultValueData_Chap3);
    }

    private void ToolDefaultValueDataSplit_Chap4()
    {
        var toolStr = Resources.Load<TextAsset>("Tool_KCSV_Chap4");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        //var toolStrSplit = removeR.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');

            var newData = new ToolDBFrame(int.Parse(str[0]), long.Parse(str[1]));
            toolDefaultValueData_Chap4.Add(newData);
        }
        toolDBLists.Add(toolDefaultValueData_Chap4);
    }

    private void ToolDefaultValueDataSplit_Chap5()
    {
        var toolStr = Resources.Load<TextAsset>("Tool_KCSV_Chap5");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        //var toolStrSplit = removeR.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');

            var newData = new ToolDBFrame(int.Parse(str[0]), long.Parse(str[1]));
            toolDefaultValueData_Chap5.Add(newData);
        }
        toolDBLists.Add(toolDefaultValueData_Chap5);
    }

    private void ToolDefaultValueDataSplit_Special()
    {
        var toolStr = Resources.Load<TextAsset>("Tool_KCSV_Special");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        //var toolStrSplit = removeR.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');

            var newData = new ToolDBFrame(int.Parse(str[0]), long.Parse(str[1]));
            toolDefaultValueData_Special.Add(newData);
        }
    }

    public void ToolNameDataSplit()
    {
        var toolStr = Resources.Load<TextAsset>("ToolName");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            toolName.Add(newData);
        }
    }

    public void ToolNameDataSplit_Special()
    {
        var toolStr = Resources.Load<TextAsset>("ToolName_Special");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            toolName_special.Add(newData);
        }
    }

    public void ToolExpDataSplit()
    {
        var toolStr = Resources.Load<TextAsset>("ToolExp");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter_multiLine, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            toolExp.Add(newData);
        }
    }

    public void ToolExpDataSplit_Special()
    {
        var toolStr = Resources.Load<TextAsset>("ToolExp_Special");
        var removeR = toolStr.text.Replace("\r", string.Empty);
        var toolStrSplit = removeR.Split(enter_multiLine, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in toolStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            toolExp_special.Add(newData);
        }
    }

    public void FriendNameDataSplit()
    {
        var friendStr = Resources.Load<TextAsset>("FriendName");
        var removeR = friendStr.text.Replace("\r", string.Empty);
        var friendStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in friendStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            friendName.Add(newData);
        }
    }

    public void FriendNameDataSplit_Special()
    {
        var friendStr = Resources.Load<TextAsset>("FriendName_Special");
        var removeR = friendStr.text.Replace("\r", string.Empty);
        var friendStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in friendStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            friendName_special.Add(newData);
        }
    }

    public void FriendExpDataSplit()
    {
        var friendStr = Resources.Load<TextAsset>("FriendExp");
        var removeR = friendStr.text.Replace("\r", string.Empty);
        var friendStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in friendStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            friendExp.Add(newData);
        }
    }

    public void FriendExpDataSplit_Special()
    {
        var friendStr = Resources.Load<TextAsset>("FriendExp_Special");
        var removeR = friendStr.text.Replace("\r", string.Empty);
        var friendStrSplit = removeR.Split(enter_multiLine, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in friendStrSplit)
        {
            var str = item.Split('/');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            friendExp_special.Add(newData);
        }
    }

    public void SpecialChapterScriptDAtaSplit()
    {
        var scsStr = Resources.Load<TextAsset>("SpecialChapterScript");
        var removeR = scsStr.text.Replace("\r", string.Empty);
        var scsStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in scsStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            specialChapterScript.Add(newData);
        }
    }

    public void MenuPopUpStringDataSplit()
    {
        var mpsStr = Resources.Load<TextAsset>("MenuPopUpString");
        var removeR = mpsStr.text.Replace("\r", string.Empty);
        var mprStrSplit = removeR.Split(enter_multiLine, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in mprStrSplit)
        {
            var str = item.Split(',');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            menuPopUpString.Add(newData);
        }
    }

    public void ChapterExpDataSplit()
    {
        var ceStr = Resources.Load<TextAsset>("ChapterExp");
        var removeR = ceStr.text.Replace("\r", string.Empty);
        var ceStrSplit = removeR.Split(enter_multiLine, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ceStrSplit)
        {
            var str = item.Split('/');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            chapterExp.Add(newData);
        }
    }

    public void ChapterExpDataSplit_Special()
    {
        var ceStr = Resources.Load<TextAsset>("ChapterExp_Special");
        var removeR = ceStr.text.Replace("\r", string.Empty);
        var ceStrSplit = removeR.Split(enter_multiLine, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ceStrSplit)
        {
            var str = item.Split('/');
            var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
            chapterExp_special.Add(newData);
        }
    }

    public void ChapterNameDataSplit()
    {
        {
            var ceStr = Resources.Load<TextAsset>("ChapterName");
            var removeR = ceStr.text.Replace("\r", string.Empty);
            var ceStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in ceStrSplit)
            {
                var str = item.Split(',');
                var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
                chapterName.Add(newData);
            }
        }
    }

    public void ChapterNameDataSplit_Special()
    {
        {
            var ceStr = Resources.Load<TextAsset>("ChapterName_Special");
            var removeR = ceStr.text.Replace("\r", string.Empty);
            var ceStrSplit = removeR.Split(enter, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in ceStrSplit)
            {
                var str = item.Split(',');
                var newData = new GrobalLanFrame(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9]);
                chapterName_special.Add(newData);
            }
        }
    }
}