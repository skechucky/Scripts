using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCollision : MonoBehaviour {

    // example : Camp Fire

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.isDead = true;
        }
    }

}
