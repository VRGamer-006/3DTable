using UnityEngine;
using System.Collections;

public class MusicLayer : MonoBehaviour
{

    public Texture backgroundOfMusicLayer;
    public Texture2D[] musicBtns;
    public Texture2D[] musicTex;
    public Texture2D[] effectBtns;
    public Texture2D[] effectTex;
    private int effectIndex;
    private int musicIndex;
    public GUIStyle btStyle;
    private Matrix4x4 guiMatrix;
    void Start()
    {
        effectIndex = PlayerPrefs.GetInt("offEffect");
        musicIndex = PlayerPrefs.GetInt("offMusic");
        guiMatrix = ConstOfMenu.getMatrix();
    }
    void OnGUI()
    {
        GUI.matrix = guiMatrix;
        GUI.DrawTexture(new Rect(0, 0, ConstOfMenu.desiginWidth, ConstOfMenu.desiginHeight), backgroundOfMusicLayer);
        GUI.DrawTexture(new Rect(200, 180, 273, 80), musicTex[musicIndex % 2]);
        if (GUI.Button(new Rect(473, 190, 110, 80), musicBtns[musicIndex % 2], btStyle))
        {
            musicIndex++;
            PlayerPrefs.SetInt("offMusic", musicIndex % 2);

        }
        GUI.DrawTexture(new Rect(200, 320, 273, 80), effectTex[effectIndex % 2]);
        if (GUI.Button(new Rect(473, 330, 110, 80), effectBtns[effectIndex % 2], btStyle))
        {
            effectIndex++;
            PlayerPrefs.SetInt("offEffect", effectIndex % 2);

        }
    }
}