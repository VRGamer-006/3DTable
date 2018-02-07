using UnityEngine;
using System.Collections;

public class RankLayer : MonoBehaviour {
    public float groupX = 177, groupY = 0, groupW = 300, groupH = 240;
    private int maxHeight;
    private int numSize = 19;
    public Texture2D bg;
    public Texture2D box;
    public Texture2D date;
    public Texture2D score;
    public Texture2D[] textures;
    public GUIStyle style;
    private string txt = "";
    private float oldPosY;
    private float currPosY;
    private string[] showRecords;
    private Matrix4x4 guiMatrix;
    void Start()
    {
        guiMatrix = ConstOfMenu.getMatrix();
        showRecords = Result.LoadData();
        maxHeight = numSize * showRecords.Length - 192;        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            oldPosY = Input.mousePosition.y;
        }
        if (Input.GetMouseButton(0))
        {
            currPosY = Input.mousePosition.y;
            groupY = Mathf.Clamp((groupY - currPosY + oldPosY), -maxHeight, 0);
            oldPosY = currPosY;
        }
    }
    void OnGUI()
    {
        GUI.matrix = guiMatrix;
        GUI.DrawTexture(new Rect(0, 0, 800, 480), bg);
        GUI.DrawTexture(new Rect(150, 150, 530, 294), box);
        if (GUI.Button(new Rect(230, 180, 130, 40), date, style))
        {
            string[] records = Result.LoadData();
            showRecords = records;
        }
        if (GUI.Button(new Rect(470, 180, 130, 40), score, style))
        {
            string[] records = Result.LoadData();
            RecordsSort(ref records);
            showRecords = records;
        }

        GUI.BeginGroup(new Rect(177, 220, 476, 192));
        GUI.BeginGroup(new Rect(0, groupY, 476, numSize * showRecords.Length));
        if (showRecords[0] != "")
        {
            DrawRecrods(showRecords);
        }
      
        GUI.EndGroup();
        GUI.EndGroup();
    }

    public void DrawRecrods(string[] records)
    {     
        for (int i = 0; i < records.Length; i++)
        {          
            string date = records[i].Split(',')[0];
            string score = records[i].Split(',')[1];
            int[] dateNum = StringToNumber(date);
            int[] scoreNum = StringToNumber(score);
            for (int j = 0; j < dateNum.Length; j++)
            {
                GUI.DrawTexture(new Rect((j + 1) * numSize, i * numSize, numSize, numSize), textures[dateNum[j]]);
            }
            for (int j = 0; j < scoreNum.Length; j++)
            {
                GUI.DrawTexture(new Rect((j + 17) * numSize, i * numSize, numSize, numSize), textures[scoreNum[j]]);
            }
        }
    }
    public void RecordsSort(ref string[] records)
    {

        for (int i = 0; i < records.Length - 1; i++)
        {
            for (int j = i + 1; j < records.Length; j++)
            {
                if (int.Parse(records[i].Split(',')[1]) < int.Parse(records[j].Split(',')[1]))
                {
                    string tempRecord = records[i];
                    records[i] = records[j];
                    records[j] = tempRecord;
                }
            }
        }
    }
    public static int[] StringToNumber(string str)
    {
        int[] result = new int[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (c == '-')
            {
                result[i] = 10;
            }
            else
            {
                result[i] = str[i] - '0';
            }
        }
        return result;
    }
}
