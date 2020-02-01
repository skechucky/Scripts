using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {
   
    public static bool jumpOn; //-> Player


	void Start () {
        jumpOn = true;
	}
	

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jumpOn = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jumpOn = false;
        }
    }

}
