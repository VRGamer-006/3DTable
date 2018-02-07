using UnityEngine;
using System.Collections;

public class AboutLayer : MonoBehaviour {

    public Texture backgroundOfAboutLayer;
    public Texture aboutOfAboutLayer;
    private Matrix4x4 guiMatrix;
	void Start () {
        guiMatrix = ConstOfMenu.getMatrix();
	}
    void OnGUI()
    {
        GUI.matrix = guiMatrix;
        GUI.DrawTexture(new Rect(0, 0, ConstOfMenu.desiginWidth, ConstOfMenu.desiginHeight), backgroundOfAboutLayer);
        GUI.DrawTexture(new Rect(148, 150, 504, 299), aboutOfAboutLayer); 
    }
}
