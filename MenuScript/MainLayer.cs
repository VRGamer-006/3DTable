using UnityEngine;
using System.Collections;

public class MainLayer : MonoBehaviour {
 
    public Texture backgroundOfMainMenu;
    public GUIStyle[] buttonStyleOfMain;
    private float[] ButtonPositionOfX = new float[4];
    private float buttonOfficerOfHeight;
    private float startYOfMainMenu;
    public bool moveFlag;
    private float buttonOfCurrentMovingDistance;
    private float buttonOfMaxDistance;
    private Matrix4x4 guiMatrix;
	void Start () {
        buttonOfficerOfHeight = 75;
        startYOfMainMenu = 150;
        moveFlag = true;     
        restData();
        buttonOfCurrentMovingDistance = 0;
        buttonOfMaxDistance = 80;
        guiMatrix = ConstOfMenu.getMatrix();	    
	}
    void OnGUI()
    {
        GUI.matrix = guiMatrix;
        GUI.DrawTexture(new Rect(0, 0, ConstOfMenu.desiginWidth, ConstOfMenu.desiginHeight), backgroundOfMainMenu);
        DrawMainMenu();
        if (moveFlag)
        {
            ButtonOfManiMenuMove();
        }
    }
    void DrawMainMenu() 
    {
        if (GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.START_BUTTON], startYOfMainMenu + buttonOfficerOfHeight * 0, 256, 64), "", buttonStyleOfMain[ConstOfMenu.START_BUTTON]))
        {
            if (!moveFlag)
            {
               (GetComponent("Constroler") as Constroler).ChangeScrip(ConstOfMenu.MainID, ConstOfMenu.ChoiceID);
            }
        }

        if (GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.MUSSIC_BUTTON], startYOfMainMenu + buttonOfficerOfHeight * 1, 256, 64), "", buttonStyleOfMain[ConstOfMenu.MUSSIC_BUTTON]))
        {
            if (!moveFlag)
            {
                (GetComponent("Constroler") as Constroler).ChangeScrip(ConstOfMenu.MainID, ConstOfMenu.SoundID);
            }
        }

        if (GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.HELP_BUTTON], startYOfMainMenu + buttonOfficerOfHeight * 2, 256, 64), "", buttonStyleOfMain[ConstOfMenu.HELP_BUTTON]))
        {
            if (!moveFlag)
            {
                (GetComponent("Constroler") as Constroler).ChangeScrip(ConstOfMenu.MainID, ConstOfMenu.HelpID);
            }

        }
        if (GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.ABOUT_BUTTON], startYOfMainMenu + buttonOfficerOfHeight * 3, 256, 64), "", buttonStyleOfMain[ConstOfMenu.ABOUT_BUTTON]))
        {
            if (!moveFlag)
            {
                (GetComponent("Constroler") as Constroler).ChangeScrip(ConstOfMenu.MainID, ConstOfMenu.AboutID);
            }
        }
    }
    void ButtonOfManiMenuMove()
    {
        float length = ConstOfMenu.movingSpeed * Time.deltaTime;
        buttonOfCurrentMovingDistance += length;
        for (int i = 0; i < ButtonPositionOfX.Length; i++)
        {
            ButtonPositionOfX[i] += (ConstOfMenu.ButtonMovingStep[i] * length);
        }    
        moveFlag = buttonOfCurrentMovingDistance < buttonOfMaxDistance;
    }
    public void restData()
    {
        for (int i = 0; i < ConstOfMenu.ButtonPositionOfX.Length; i++)
        {
            ButtonPositionOfX[i] = ConstOfMenu.ButtonPositionOfX[i];
        }
        buttonOfCurrentMovingDistance = 0;
        moveFlag = true;
    }	
}
