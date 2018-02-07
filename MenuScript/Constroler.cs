using UnityEngine;
using System.Collections;

public class Constroler : MonoBehaviour {
    private int currentID = ConstOfMenu.MainID;
    MonoBehaviour[] script;
    void Awake() {
        script = GetComponents<MonoBehaviour>();
    }	
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeEvent();
        }
	}
    public void ChangeScrip(int offID,int onID )
    {        
        restData();
        script[offID].enabled = false;
        script[onID].enabled = true;
        currentID = onID;
    }
    void EscapeEvent()
    {
        switch (currentID)
        {
            case 1: if ((GetComponent("MainLayer") as MainLayer).moveFlag)
                    {
                        break;
                    }Application.Quit(); break;
            case 2:
            case 3:
            case 4:
            case 5:
                ChangeScrip(currentID,ConstOfMenu.MainID); break;
            case 6:
                ChangeScrip(currentID,ConstOfMenu.ChoiceID); break;
            case 7:
                ChangeScrip(currentID,ConstOfMenu.ModeChoiceID); break;
        }
    }
    private void restData()
    {
        (GetComponent("MainLayer") as MainLayer).restData();
        (GetComponent("ChoiceLayer") as ChoiceLayer).restData();
        (GetComponent("ModeChoiceLayer") as ModeChoiceLayer).restData();
        HelpLayer helpLayer = GetComponent("HelpLayer") as HelpLayer;
        helpLayer.restData();

    }       
    
}
