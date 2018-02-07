using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
    public LayerMask mask = -1;
    public GameObject cueBall;
    private float total_RotationX;
    public float freeViewRotationMatrixY = 0;
    Logic logic; 
    public GameObject []cameras;
    public static int curCam = 1;
    public static Vector3 prePosition = Vector3.zero;
    public static bool touchFlag = true;
    Matrix4x4 inverse;
    Quaternion qua;
    Vector3 vec;
	void Start () {
        qua = cameras[4].transform.rotation;
        vec = cameras[4].transform.position;
        for (int i = 0; i < 3; i++)
        {
            cameras[i].camera.aspect = 800.0f / 480.0f;
            float lux = (Screen.width - ConstOfMenu.desiginWidth * Screen.height / ConstOfMenu.desiginHeight) / 2.0f;
            cameras[i].camera.pixelRect = new Rect(lux, 0, Screen.width - 2 * lux, Screen.height);
        }
        total_RotationX = 13;    
        logic = GetComponent("Logic") as Logic;
        inverse = ConstOfMenu.getInvertMatrix();
	}	
	void Update () {    
        if (!touchFlag)
        {
            return;
        }
        if (!GameLayer.TOTAL_FLAG)
        {
            return;
        }        
        if (Input.GetMouseButton(0))
        {   
            float angleY = (Input.mousePosition.x - prePosition.x)/ConstOfGame.SCALEX;    
            float angleX = (Input.mousePosition.y - prePosition.y) / ConstOfGame.SCALEY;
            Vector3 newPoint = ConstOfMenu.getInvertMatrix().MultiplyVector(Input.mousePosition);         
            switch (curCam)
            {
                case 0: mainFunction(Input.mousePosition); break;
                case 1: firstFunction(angleY, angleX); break;
                case 2: freeFunction(angleY, angleX); break;
            }       
            prePosition = Input.mousePosition;
        }
    }
    public void ChangeCam(int index) {
        setFreeCame();
        cameras[curCam].SetActive(false);
        cameras[index].SetActive(true);     
        curCam = index;
    }
    public void setFreeCame()
    {
        cameras[3].transform.rotation = cameras[4].transform.rotation;
        cameras[3].transform.position = cameras[4].transform.position;
        freeViewRotationMatrixY = GameLayer.totalRotation;
        total_RotationX = 13;
        cameras[2].transform.position = cameras[4].transform.position;
        cameras[2].transform.rotation = cameras[4].transform.rotation;
        cameras[1].transform.position = cameras[4].transform.position;
        cameras[1].transform.rotation = cameras[4].transform.rotation;
    }
    void mainFunction(Vector3 pos)
    {        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit, 100, mask.value))
        {
            cameras[3].transform.rotation = qua;
            cameras[3].transform.position = vec;
            Vector3 hitPoint = hit.point;
            Vector3 cubBallPoint = cueBall.transform.position;
            float angle = 180 - Mathf.Atan2(cubBallPoint.x - hitPoint.x, cubBallPoint.z - hitPoint.z) * Mathf.Rad2Deg;
            GameLayer.totalRotation = -angle;
            cameras[3].transform.transform.RotateAround(ConstOfGame.CUEBALL_POSITION, Vector3.up, GameLayer.totalRotation);
            logic.cueObject.transform.rotation = cameras[3].transform.rotation;            
        }
             
    }
    public void moveCame(int sign)
    {
        cameras[curCam].transform.Translate(new Vector3(0, 0, sign * Time.deltaTime));              
        Vector3 posCueBall= cameras[curCam].transform.InverseTransformPoint(cueBall.transform.position);
        if (posCueBall.z > 35 || posCueBall.z < 7)
        {
            cameras[curCam].transform.Translate(new Vector3(0, 0, -sign * Time.deltaTime));
        }        
    }
    void firstFunction(float angleY,float angleX)
    {
        if (Mathf.Abs(angleY) > Mathf.Abs(angleX) && Mathf.Abs(angleY)>1f)
        {
            GameLayer.totalRotation += angleY;
            logic.cueObject.transform.RotateAround(logic.cueBall.transform.position, Vector3.up, angleY);
        }
        else
        {
            if (total_RotationX + angleX > 10 && total_RotationX + angleX < 90)
            {
                if (Mathf.Abs(angleX) > 1f)
                {
                    Vector3 right = new Vector3(Mathf.Cos(-GameLayer.totalRotation / 180.0f * Mathf.PI), 0, Mathf.Sin(-GameLayer.totalRotation / 180.0f * Mathf.PI));
                    total_RotationX += angleX;
                    cameras[1].transform.RotateAround(logic.cueBall.transform.position, right, angleX);
                }                
            }        
        }   
    }
    void freeFunction(float angleY, float angleX)
    {
        if (Mathf.Abs(angleY) > 0.5f)
        {
            freeViewRotationMatrixY += angleY;
            cameras[2].transform.RotateAround(logic.cueBall.transform.position, Vector3.up, angleY);
        }
        else
        {
            if (total_RotationX + angleX > 10 && total_RotationX + angleX < 90f)
            {
                Vector3 right = cameras[curCam].transform.TransformDirection(Vector3.right);
                total_RotationX += angleX;
                cameras[2].transform.RotateAround(logic.cueBall.transform.position, right, angleX);
            }
        }       
    }
}

