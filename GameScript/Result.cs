using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {

    private bool isResult;
    public Texture2D backGround;
    public Texture2D dialog;
    public Texture2D[] tipTexture;
    private int tipIndex;
    Matrix4x4 guiMatrix;
    public GUIStyle[] guiSytle;
    Logic logic;
    PowerBar powerBar;
    void Awake()
    {
        logic = GetComponent("Logic") as Logic;
        powerBar = GetComponent("PowerBar") as PowerBar;
        tipIndex = 0;
        guiMatrix = ConstOfMenu.getMatrix();
        isResult = false;
      }	
    public void goVectorScene()
    {
        powerBar.isStartTime = false;
        logic.enabled = false;
        for (int i = 0; i < GameLayer.BallGroup_TOTAL.Count; i++)
        {
            GameObject ball = GameLayer.BallGroup_TOTAL[i] as GameObject;
            ball.transform.rigidbody.velocity = Vector3.zero;
            ball.transform.rigidbody.angularVelocity = Vector3.zero;
        } 
        isResult = true;
        tipIndex = 1; 
        if (PowerBar.showTime != 720)
        {
            SaveData(PowerBar.showTime);
        }          
    }
    public void goLoseScene()
    {
        powerBar.isStartTime = false;
        logic.enabled = false;
        for (int i = 0; i < GameLayer.BallGroup_TOTAL.Count; i++)
        {
            GameObject ball = GameLayer.BallGroup_TOTAL[i] as GameObject;
            ball.transform.rigidbody.velocity = Vector3.zero;
            ball.transform.rigidbody.angularVelocity = Vector3.zero;
        }  
        isResult = true;
        tipIndex = 0;     
    }
    void OnGUI()
    {
        if (isResult)
        {
            GameLayer.TOTAL_FLAG = false; 
            GUI.matrix = guiMatrix;
            GUI.DrawTexture(new Rect(0, 0, 800, 480), backGround);
            GUI.BeginGroup(new Rect(200,150,400,180));
            GUI.DrawTexture(new Rect(0, 0, 400, 180), dialog);
            GUI.DrawTexture(new Rect(100, 20, 200,50), tipTexture[tipIndex]);
            if (GUI.Button(new Rect(30,100,150,50), "", guiSytle[0]))
            {
                GameLayer.resetAllStaticData();
                Application.LoadLevel("GameScene");
            }
            if (GUI.Button(new Rect(220, 100, 150, 50), "", guiSytle[1]))
            {
                Application.Quit();

            }
            GUI.EndGroup();
        }
    }
    public static void SaveData(int score)
    {   
        int year = System.DateTime.Now.Year;
        int month = System.DateTime.Now.Month;
        int day = System.DateTime.Now.Day;
        string date = year + "-" + month + "-" + day;
        string oldData = PlayerPrefs.GetString("gameData");
        string gameData = "";
        if (oldData == "")
        {
            gameData = date + "," + score;
        }
        else
        {
            gameData = oldData + ";" + date + "," + score;
        }
        PlayerPrefs.SetString("gameData", gameData);    
    }

    public static string[] LoadData()
    {
        string[] records = PlayerPrefs.GetString("gameData").Split(';');
        return records;
    }
}
