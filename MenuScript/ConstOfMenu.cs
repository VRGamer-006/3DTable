using UnityEngine;
using System.Collections;
public class ConstOfMenu : MonoBehaviour
{
    public static float desiginWidth = 800.0f;
    public static float desiginHeight = 480.0f;
    public static int START_BUTTON = 0;
    public static int MUSSIC_BUTTON = 1;
    public static int HELP_BUTTON = 2;
    public static int ABOUT_BUTTON = 3;
    public static int EIGHT_BUTTON = 0;
    public static int NINE_BUTTON = 1;
    public static int COUNTDOW_BUTTON = 0;
    public static int PRACTICE_BUTTON = 1;
    public static int RANK_BUTTON = 2;
    public static float movingSpeed = 80f;
    public static float[] ButtonPositionOfX = new float[4] { 128, 416, 128, 416 };
    public static float[] ButtonMovingStep = new float[4] { 1, -1, 1, -1 };
    public static float[] BPositionXOfMode = new float[3] { 128, 416, 128 };
    public static float[] BMovingXStepOfMode = new float[3] { -1, 1, -1 };
    public static float movingSpeedOFMode = 80f;
    public static int MainID = 1;
    public static int ChoiceID = 2;
    public static int SoundID = 3;
    public static int HelpID = 4;
    public static int AboutID = 5;
    public static int ModeChoiceID = 6;
    public static int RankID = 7;   
    public static Matrix4x4 getMatrix()
    {
        Matrix4x4 guiMatrix = Matrix4x4.identity;
        float lux = (Screen.width - ConstOfMenu.desiginWidth * Screen.height / ConstOfMenu.desiginHeight) / 2.0f;
        guiMatrix.SetTRS(new Vector3(lux,0,0), Quaternion.identity, new Vector3(Screen.height / ConstOfMenu.desiginHeight, Screen.height / ConstOfMenu.desiginHeight, 1));
        return guiMatrix;
    }
    public static Matrix4x4 getInvertMatrix()
    {
        Matrix4x4 guiInverseMatrix = getMatrix();
        guiInverseMatrix = Matrix4x4.Inverse(guiInverseMatrix);
        return guiInverseMatrix;
    }
}
