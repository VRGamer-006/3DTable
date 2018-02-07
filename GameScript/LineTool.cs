using UnityEngine;
using System.Collections;

public class LineTool : MonoBehaviour {

     private Quaternion rotation;      
	void Start () {
	    rotation = transform.rotation;
	}	
	void Update () {
        transform.rotation = rotation;
        transform.RotateAround(Vector3.up, GameLayer.totalRotation*Mathf.Deg2Rad);
	}
}
