using UnityEngine;
using System.Collections;

public class ModeChoiceLayer : MonoBehaviour {
    public Texture backgroundOfModeChoicerMenu;
    public GUIStyle[] buttonStyleOfModeChoice;
    private float[] ButtonPositionOfX = new float[3];
    private float buttonOfCurrentMovingDistance;
    private float buttonOfMaxDistance;
    private float startYOfModeChoice;
    private float buttonOfficerOfHeight;
    private bool moveFlag;
    private Matrix4x4 guiMatrix;
	void Start () {
        moveFlag = true;
        restData();
        buttonOfCurrentMovingDistance = 0;
        buttonOfMaxDistance = 144f;
        startYOfModeChoice = 180f;
        buttonOfficerOfHeight = 90f;
        guiMatrix = ConstOfMenu.getMatrix();       
	}
    void OnGUI()
    {   
        GUI.matrix = guiMatrix;
        GUI.DrawTexture(new Rect(0, 0, ConstOfMenu.desiginWidth, ConstOfMenu.desiginHeight), backgroundOfModeChoicerMenu);
        if (GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.COUNTDOW_BUTTON], startYOfModeChoice, 256, 64), "", buttonStyleOfModeChoice[ConstOfMenu.COUNTDOW_BUTTON]))
        {
            if (!moveFlag)
            {
                PlayerPrefs.SetInt("isTime", 1); 
                GameLayer.resetAllStaticData();
                Application.LoadLevel("GameScene");	
            }          
        }
        if (GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.PRACTICE_BUTTON], startYOfModeChoice + buttonOfficerOfHeight * 1, 256, 64), "", buttonStyleOfModeChoice[ConstOfMenu.PRACTICE_BUTTON]))
        {
            if (!moveFlag)
            {
                PlayerPrefs.SetInt("isTime", 0);
                GameLayer.resetAllStaticData();
                Application.LoadLevel("GameScene");	
            }           
        }
        if (GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.RANK_BUTTON], startYOfModeChoice + buttonOfficerOfHeight * 2, 256, 64), "", buttonStyleOfModeChoice[ConstOfMenu.RANK_BUTTON]))
        {
            if (!moveFlag)
            {
                (GetComponent("Constroler") as Constroler).ChangeScrip(ConstOfMenu.ModeChoiceID, ConstOfMenu.RankID);
            }            
        }
        if (moveFlag)
        {
            ButtonMove();
        }
    }
    void ButtonMove()
    {
        float length = ConstOfMenu.movingSpeedOFMode * Time.deltaTime;
        buttonOfCurrentMovingDistance += length; 
        for (int i = 0; i < ButtonPositionOfX.Length; i++)
        {
            ButtonPositionOfX[i] += (ConstOfMenu.ButtonMovingStep[i] * length);
        }
        moveFlag = buttonOfCurrentMovingDistance < buttonOfMaxDistance;
    }
    public void restData()
    {
        for (int i = 0; i < ConstOfMenu.BPositionXOfMode.Length; i++)
        {
            ButtonPositionOfX[i] = ConstOfMenu.BPositionXOfMode[i];
        }
        buttonOfCurrentMovingDistance = 0;
        moveFlag = true;

    }
}
