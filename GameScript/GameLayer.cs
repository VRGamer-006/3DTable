using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameLayer : MonoBehaviour
{
    enum ButtonS
    {
        Go = 0, Far, Near, Left, Right, M, firstV, freeV, thirdV
    }
    public static ArrayList BallGroup_ONE_EIGHT = new ArrayList();
    public static ArrayList BallGroup_TWO_EIGHT = new ArrayList();
    public static ArrayList BallGroup_ONE_NINE = new ArrayList();
    public static ArrayList BallGroup_TOTAL = new ArrayList();
    public static int ballInNum = 0;
    public static float totalRotation = 0.0f;
    public static bool TOTAL_FLAG = true;
    public static bool isStartAction = false;
    private bool isFirstView;
    private bool isFirstActionOver;
    private bool isSecondActionOver;
    private int tbtIndex;
    Matrix4x4 guiMatrix;
    public GUIStyle[] btnStyle;
    public GUIStyle fbtnStyle;
    Logic logic;
    MiniMap miniMap;
    InitAllBalls initClass;
    public Texture2D[] nums;
    public AudioClip startSound;
    void Start()
    {
        isFirstView = true;
        isFirstActionOver = false;
        isSecondActionOver = false;
        GameLayer.BallGroup_TOTAL.Add(GameObject.Find("CueBall"));
        initClass = GetComponent("InitAllBalls") as InitAllBalls;
        miniMap = GetComponent("MiniMap") as MiniMap;
        initClass.initAllBalls(PlayerPrefs.GetInt("billiard"));
        logic = GetComponent("Logic") as Logic;
        if (PlayerPrefs.GetInt("offMusic") != 0)
        {
            audio.Pause();
        }
        guiMatrix = ConstOfMenu.getMatrix();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {  
            Application.LoadLevel("MenuScene");
        }
    }
    void OnGUI()
    {
        GUI.matrix = guiMatrix;
        GUI.DrawTexture(new Rect(770, 10, 30, 30), nums[GameLayer.ballInNum]);
        DrawButtons();
        miniMap.drawMiniMap();
        if (GameLayer.isStartAction)
        {
            cueRunAction();
        }
    }
    void DrawButtons()
    {
        if (GUI.Button(new Rect(0, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.Go]))
        { 
            if (GameLayer.TOTAL_FLAG)
            {
                GameLayer.TOTAL_FLAG = false;
                isFirstActionOver = false;
                isSecondActionOver = false;
                GameLayer.isStartAction = true;
                logic.cuePosition = logic.cueBall.transform.position;
            }
        }
        if (GUI.RepeatButton(new Rect(100, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.Far]))
        {
            if (GameLayer.TOTAL_FLAG)
            {
                (GetComponent("CamControl") as CamControl).moveCame(-5);
            }
        }
        if (GUI.RepeatButton(new Rect(200, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.Near]))
        {
            if (GameLayer.TOTAL_FLAG)
            {
                (GetComponent("CamControl") as CamControl).moveCame(5);
            }
        }
        if (GUI.RepeatButton(new Rect(300, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.Left]))
        {
            if (GameLayer.TOTAL_FLAG)
            {
                GameLayer.totalRotation -= ConstOfGame.rotationStep;
                logic.cueObject.transform.RotateAround(logic.cueBall.transform.position, Vector3.up, -ConstOfGame.rotationStep);
            }
        }
        if (GUI.RepeatButton(new Rect(460, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.Right]))
        {
            if (GameLayer.TOTAL_FLAG)
            {
                GameLayer.totalRotation += ConstOfGame.rotationStep;
                logic.cueObject.transform.RotateAround(logic.cueBall.transform.position, Vector3.up, ConstOfGame.rotationStep);
            }
        }
        if (GUI.Button(new Rect(550, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.M]))
        {
            if (GameLayer.TOTAL_FLAG)
            {
                MiniMap.isMiniMap = !MiniMap.isMiniMap;
            }
        }
        if (GUI.Button(new Rect(650, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", fbtnStyle))
        {
            if (GameLayer.TOTAL_FLAG)
            {
                logic.assistBall.SetActive(!logic.assistBall.activeSelf);
                logic.line.SetActive(!logic.line.activeSelf);
            }
        }

        if (isFirstView)
        {
            if (GUI.Button(new Rect(740, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.firstV]))
            {
                if (GameLayer.TOTAL_FLAG)
                {
                    (GetComponent("CamControl") as CamControl).ChangeCam(2);
                    isFirstView = !isFirstView;
                }
            }
        }
        else
        {
            if (GUI.Button(new Rect(740, ConstOfGame.btnPositonY, ConstOfGame.btnSize, ConstOfGame.btnSize), "", btnStyle[(int)ButtonS.freeV]))
            {
                if (GameLayer.TOTAL_FLAG)
                {
                    (GetComponent("CamControl") as CamControl).ChangeCam(1);
                    isFirstView = !isFirstView;
                }
            }
        }
        if (GUI.Button(new Rect(730, 10, 30, 30), "", btnStyle[(int)ButtonS.thirdV]))
        {
            if (GameLayer.TOTAL_FLAG)
            {
                (GetComponent("CamControl") as CamControl).ChangeCam(0);
            }
        }
    }
    void cueRunAction()
    {
        if (!isFirstActionOver)
        {
            logic.cue.transform.Translate(new Vector3(0, 0, Time.deltaTime));
            if (logic.cue.transform.localPosition.z <= -2)
            {
                isFirstActionOver = true;
            }
        }
        else if (!isSecondActionOver && isFirstActionOver)
        {
            logic.cue.transform.Translate(new Vector3(0, 0, -2 * Time.deltaTime));
            if (logic.cue.transform.localPosition.z >= -0.45f)
            {
                isSecondActionOver = true;
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("offEffect") == 0)
            {
                audio.PlayOneShot(startSound);
            }
            logic.cue.transform.localPosition = new Vector3(0, 0, -1);
            logic.cue.renderer.enabled = false;
            logic.assistBall.transform.position = new Vector3(100, 0.98f, 100);
            logic.line.renderer.enabled = false;
            logic.cueBall.rigidbody.velocity = new Vector3((PowerBar.restBars - 1) / 22.0f * ConstOfGame.MAX_SPEED * Mathf.Sin(GameLayer.totalRotation / 180.0f * Mathf.PI), 0, (PowerBar.restBars - 1) / 22.0f * ConstOfGame.MAX_SPEED * Mathf.Cos(GameLayer.totalRotation / 180.0f * Mathf.PI));
            GameLayer.isStartAction = false;
        }
    }    
    public static void resetAllStaticData()
    {
        BallGroup_ONE_EIGHT.Clear();
        BallGroup_TWO_EIGHT.Clear();
        BallGroup_ONE_NINE.Clear();
        BallGroup_TOTAL.Clear();
        ballInNum = 0;
        totalRotation = 0.0f;
        TOTAL_FLAG = true;
        isStartAction = false;
        CamControl.curCam = 1;
        CamControl.prePosition = Vector3.zero;
        CamControl.touchFlag = true;
        PowerBar.showTime = 720;
        PowerBar.restBars = 22;
    }
}