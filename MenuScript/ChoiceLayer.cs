using UnityEngine;
using System.Collections;

public class ChoiceLayer : MonoBehaviour {
    public Texture backgroundOfChoiceMenu;
    public GUIStyle[] buttonStyleOfChoice;
    private bool scaleFlag;
    private float scaleFactor;
    private float buttonSize;
    private float buttonStartX;
    private float buttonStartY;
    private Matrix4x4 guiMatrix;
	void Start () {
        scaleFlag = true;
        scaleFactor = 0.0f;
        buttonSize = 120;
        buttonStartX = 200;
        buttonStartY = 220;
        guiMatrix = ConstOfMenu.getMatrix();
	}
    void OnGUI()
    {
        GUI.matrix = guiMatrix;
        GUI.DrawTexture(new Rect(0, 0, ConstOfMenu.desiginWidth, ConstOfMenu.desiginHeight), backgroundOfChoiceMenu);
        ButtonScale();
        if (GUI.Button(new Rect(buttonStartX, buttonStartY, buttonSize * scaleFactor, buttonSize * scaleFactor), "", buttonStyleOfChoice[ConstOfMenu.EIGHT_BUTTON]))
        {
            if (!scaleFlag)
            {
                PlayerPrefs.SetInt("billiard", 8);
                (GetComponent("Constroler") as Constroler).ChangeScrip(ConstOfMenu.ChoiceID, ConstOfMenu.ModeChoiceID);   
            }                     
        }
        if (GUI.Button(new Rect(buttonStartX+240.0f, buttonStartY, buttonSize * scaleFactor, buttonSize * scaleFactor), "", buttonStyleOfChoice[ConstOfMenu.NINE_BUTTON]))
        {
            if (!scaleFlag)
            {
                PlayerPrefs.SetInt("billiard", 9);
                (GetComponent("Constroler") as Constroler).ChangeScrip(ConstOfMenu.ChoiceID, ConstOfMenu.ModeChoiceID);
            }           
        }
    }
    void ButtonScale()
    {      
        scaleFactor = Mathf.Min(1.0f, scaleFactor + Time.deltaTime);
        scaleFlag = (scaleFactor != 1f);
    }
    public void restData()
    {
        scaleFlag = true;
        scaleFactor = 0.0f;
    }
}
