using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public static bool keyTaken; // verification if can change closed gate to opened gate -> Gate
    public static int level; // <- LevelLoadAndRespawn(take lvl)
    public int timeRotation = 22;


    void Start()
    {
        keyTaken = false;
    }


    void Update()
    {
        transform.Rotate(0, 0, timeRotation * Time.deltaTime); // anime rotation
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            keyTaken = true;
            Destroy(gameObject, 0.1f);
        }

        if (level == 5)
        {
            if (collision.tag == "Shuriken" || collision.tag == "redShuriken")
            {
                transform.position = new Vector2(transform.position.x + 0.9f * Time.deltaTime, transform.position.y);
            }
        }

    }


}
