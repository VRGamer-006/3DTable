using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
    public static bool isMiniMap = true;
    private Texture2D[] textures;
    private Texture2D miniTable;
    private Texture2D cue;
    private float scale;
    private Vector2 pivotPoint;
    Matrix4x4 guiInvert;
    void Start()
    {       
        guiInvert = ConstOfMenu.getMatrix();
        scale = ConstOfGame.miniMapScale;
        InitMiniTexture(PlayerPrefs.GetInt("billiard"));
        miniTable = Resources.Load("minitable") as Texture2D;
        cue = Resources.Load("cueMini") as Texture2D;
    }
    public void drawMiniMap()
    {
        if (MiniMap.isMiniMap)
        {            
            GUI.DrawTexture(new Rect(0, 0, 283.0f / scale, 153.0f / scale), miniTable);
            for (int i = 0; i < GameLayer.BallGroup_TOTAL.Count; i++)
            {
                GameObject tran = GameLayer.BallGroup_TOTAL[i] as GameObject; 
                BallScript ballScript = tran.GetComponent("BallScript") as BallScript;
                Vector3 ballPosition = tran.transform.position;
                int ballId = ballScript.ballId;
                GUI.DrawTexture(new Rect(ballPosition.z * 5 + 70, ballPosition.x * 5 + 35f, 5, 5), textures[ballId]);
            }           
            if ((GameObject.Find("Cue") as GameObject).renderer.enabled)
            {         
                Vector3 cuePosition = (GameObject.Find("CueObject") as GameObject).transform.position;
                Vector3 cueBallPosition = (GameObject.Find("CueBall") as GameObject).transform.position;
                pivotPoint = new Vector2(cueBallPosition.z * 5 + 72.5f, cueBallPosition.x * 5 + 37f);
                Vector3 m = guiInvert.MultiplyPoint3x4(new Vector3(pivotPoint.x, pivotPoint.y,0));
                GUIUtility.RotateAroundPivot(GameLayer.totalRotation, new Vector2(m.x, m.y));
                GUI.DrawTexture(new Rect(cuePosition.z * 5 + 45, cuePosition.x * 5 + 37f, 20, 2), cue);
            }          
        } 
    }
    void InitMiniTexture(int billiard)
    {
         bool init = (billiard - 8) > 0;
         if (!init)
         {
             textures = new Texture2D[16];
             for (int i = 0; i < 16; i++)
             {   
                 textures[i] = Resources.Load("minimap" + i) as Texture2D;
             }
         }
         else
         {
             textures = new Texture2D[10];
             for (int i = 0; i < 10; i++)
             {   
                 textures[i] = Resources.Load("minimap" + i) as Texture2D;
             }
         }
    }
}
