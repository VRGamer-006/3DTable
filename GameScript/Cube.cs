using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    public AudioClip BallinEffect;
    void OnCollisionEnter(Collision other)
    {       
        if (other.gameObject.tag == "Balls")
        {         
            if (PlayerPrefs.GetInt("offEffect") == 0)
            {
                audio.PlayOneShot(BallinEffect);
            }
            (other.gameObject.GetComponent("BallScript") as BallScript).isAlowRemove = true;           
        }
    }
}
