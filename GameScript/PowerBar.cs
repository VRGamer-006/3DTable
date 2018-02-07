using UnityEngine;
using System.Collections;

public class PowerBar : MonoBehaviour
{
    public static int tipIndex;
    public Texture2D[] tipTexture;
    public Texture2D bg;
    public Texture2D bar;
    private int groupX = 0, groupY = 120, groupWidth = 100, groupHeight = 230,
               barX = 5, barY = 5, barW = 40, barH = 220;
    private float texX = 0, texY = 0, texW = 1, texH = 1;
    private int totalBars = 22;
    private int barWidth;
    private Rect groupRect;
    public  static int restBars = 22;
    private Matrix4x4 invertMatrix;
    Vector3 movePosition;
    Vector3 startPositon;
    public Texture2D[] textures;
    public bool isStartTime;
    private  int totalTime;
    private  int countTime;
    public static  int showTime = 720;
    private int startTime;
    private  int x = 300, y = 30, numWidth = 32, numHeight = 32, span = 6;
    private Result result;
	 
    void Start()
    {
          result = GetComponent("Result") as Result;
        if (PlayerPrefs.GetInt("billiard") == 8)
        {
            tipIndex = 0;
        }
        else
        {
            tipIndex = 3;
        }       
        startTime = (int)Time.time;
        countTime = 0;
        totalTime = 720;
        isStartTime = PlayerPrefs.GetInt("isTime") > 0;
        startPositon = Vector3.zero;
        movePosition = Vector3.zero;
        invertMatrix = ConstOfMenu.getInvertMatrix();
        groupRect = new Rect(groupX, groupY, groupWidth, groupHeight + 100);
        barWidth = barH / totalBars;
    }
    void Update()
    {
        if (isStartTime)
        {
            countTime = (int)Time.time - startTime;
            showTime = totalTime - countTime;
            if (showTime<=0)
            {
                result.goLoseScene();
            }
        }
        if (GameLayer.TOTAL_FLAG)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CamControl.prePosition = Input.mousePosition;
                CamControl.touchFlag = false;
                startPositon = invertMatrix.MultiplyPoint3x4(Input.mousePosition);
                movePosition = startPositon;
            }
            if (Input.GetMouseButton(0))
            {
                movePosition = invertMatrix.MultiplyPoint3x4(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                CamControl.touchFlag = false;
            }
        }        
    }
    void OnGUI()
    {
        GUI.matrix = ConstOfMenu.getMatrix();
        GUI.DrawTexture(new Rect(272, 5, 256, 16), tipTexture[tipIndex]);       
        if (isStartTime)
        {
            DrawTime(showTime);
        }        
        GUI.BeginGroup(groupRect);
        GUI.DrawTexture(new Rect(0, 0, groupWidth, groupHeight), bg);
        GUI.DrawTextureWithTexCoords(new Rect(barX, barY + barWidth * (totalBars - restBars), barW, barWidth * restBars), bar, new Rect(texX, texY, texW, texH * restBars / totalBars));
        GUI.EndGroup();
        if (new Rect(barX + groupX, barY + groupY, barW, barH).Contains(new Vector2(startPositon.x, 480.0f - startPositon.y)))
        {           
            CamControl.touchFlag = false;
            restBars = Mathf.Clamp(totalBars - (int)(480.0f - movePosition.y - barY - groupY) / barWidth, 1, 22);        
        }
        else
        {
            if (new Rect(0, 420, 800, 60).Contains(new Vector2(movePosition.x, 480.0f - movePosition.y)))
            {
                if (new Rect(0, 420, 800, 60).Contains(new Vector2(startPositon.x, 480.0f - startPositon.y)))
                {                
                    CamControl.touchFlag = false;
                }              
            }
            else if (new Rect(730, 10, 30, 30).Contains(new Vector2(movePosition.x, 480.0f - movePosition.y)))
            {
                if (new Rect(730, 10, 30, 30).Contains(new Vector2(startPositon.x, 480.0f - startPositon.y)))
                {
                    CamControl.touchFlag = false;
                } 
            }
            else
            {
                CamControl.touchFlag = true;
            }
        }
    }
    void DrawTime(int time)
    {
        int minute = time / 60;
        int seconds = time % 60;
        int num1 = minute / 10;
        int num2 = minute % 10;
        int num3 = seconds / 10;
        int num4 = seconds % 10;
        GUI.BeginGroup(new Rect(x, y, 5 * (numWidth + span), numHeight));
        GUI.DrawTexture(new Rect(0, 0, numWidth, numHeight), textures[num1]);
        GUI.DrawTexture(new Rect((numWidth + span), 0, numWidth, numHeight), textures[num2]);
        GUI.DrawTexture(new Rect(2 * (numWidth + span), 0, numWidth, numHeight), textures[textures.Length - 1]);
        GUI.DrawTexture(new Rect(3 * (numWidth + span), 0, numWidth, numHeight), textures[num3]);
        GUI.DrawTexture(new Rect(4 * (numWidth + span), 0, numWidth, numHeight), textures[num4]);
        GUI.EndGroup();
    }
}