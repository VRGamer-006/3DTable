using UnityEngine;
using System.Collections;

public class HelpLayer : MonoBehaviour {
    public Texture2D[] helpTexture;
    private float positionY;
    private float officerY;
    private Matrix4x4 guiMatrix;
    private int currentIndex;
    private Vector2 touchPoint ;
    private float currentDistance;
    private float scale;
    private bool isMoving;
    private float moveStep;
    private int stepHao;
    private Vector2 prePositon;
	void Start () {
        touchPoint = Vector2.zero;
        prePositon = Vector2.zero;
        currentIndex = 0;
        positionY = 0.0f;
        moveStep = 300;
        currentDistance = 0;
        isMoving = false;
        officerY = ConstOfMenu.desiginHeight;
        guiMatrix = ConstOfMenu.getMatrix();
        scale = Screen.height / 480.0f;
	}
    void Update(){
        if(!isMoving && Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {              
                touchPoint = touch.position;
                prePositon = touch.position;               

            }
            else if (touch.phase == TouchPhase.Moved)
            {               
                float newPositonY= positionY - touch.position.y + prePositon.y;
                positionY = (newPositonY > 0) ? 0 : (newPositonY > (-480 * 7) ? newPositonY : (-480 * 7));      
                prePositon = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isMoving = true;
                currentDistance = (touch.position.y - touchPoint.y) / scale;                
                stepHao = (Mathf.Abs(currentDistance) > 150.0f) ? (currentDistance > 0 ? 1 : (-1)) : 0;         
                moveStep = (Mathf.Abs(currentDistance) > 150.0f) ? (currentDistance > 0 ? -Mathf.Abs(moveStep) : Mathf.Abs(moveStep)) : (currentDistance > 0 ? Mathf.Abs(moveStep) : -Mathf.Abs(moveStep));          
                indexChange(stepHao);         
            }    
        }       
    }
    void OnGUI(){
        GUI.matrix = guiMatrix;
        for (int i = 0; i < helpTexture.Length; i++){
            if (Mathf.Abs(currentIndex - i) < 2){
                GUI.DrawTexture(new Rect(0, positionY + officerY * i, ConstOfMenu.desiginWidth, ConstOfMenu.desiginHeight), helpTexture[i]);
            }
        }
        if(isMoving){
           textureMove();  
        }
    }
    void textureMove(){
        float positionYOfNew = positionY + moveStep * Time.deltaTime;
        float minDistance = -480*Mathf.Abs(currentIndex);
        if (stepHao==1)
        {
            positionY = Mathf.Max(positionYOfNew, minDistance);           
        }
        else if (stepHao == -1)
        {
            positionY = Mathf.Min(positionYOfNew, minDistance);                   
        }
        else
        {
            if (moveStep > 0)
            {
                positionY = Mathf.Min(positionYOfNew, minDistance);              
            }
            else
            {
                positionY = Mathf.Max(positionYOfNew, minDistance);       
            }
        }
        isMoving = !(positionY == minDistance);       
    }
    void indexChange(int step){
        int newIndex = currentIndex+step;
        if (newIndex>7||newIndex<0)
        {
            return;
        }
        currentIndex = newIndex;
        isMoving = true;
    }
    public void restData()
    {
        currentIndex = 0;
        positionY = 0.0f;
        moveStep = 300;
        currentDistance = 0;        
        isMoving = false;

    }
}
