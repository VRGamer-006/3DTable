using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {
    Vector3 parentPosition;
	void Update () {        
        parentPosition = transform.parent.position;
        transform.rotation = new Quaternion(1,0,0,-Mathf.PI*0.32f);
        transform.position = new Vector3(parentPosition.x, 0.55f, parentPosition.z - 0.4f);
	}
}
