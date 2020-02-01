using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovablePlatform : MonoBehaviour
{
    public static bool isGoingLeft = false;  //-> Player sync
    public static float speed = 0.75f;       //-> Player sync

    public float distance = 1f;
    public float distFromStart;

    private Vector3 startPosition;
    private Vector3 velocity;

   

    public void Start()
    {
        startPosition = gameObject.transform.position;
        velocity = new Vector3(speed, 0, 0);
        transform.Translate(velocity.x * Time.deltaTime, 0, 0);
    }

    void Update()
    {
        distFromStart = transform.position.x - startPosition.x;

        if (JumpTrigger.jumpOn == true)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
        } else 
            GetComponent<CapsuleCollider2D>().enabled = false;

        if (isGoingLeft)
        {
            if (distFromStart < -distance)
            {
                SwitchDirection();
            }
            transform.Translate(-velocity.x * Time.deltaTime, 0, 0);       
        }

        else
        {
            if (distFromStart > distance)
            {
                SwitchDirection();
            }
            transform.Translate(velocity.x * Time.deltaTime, 0, 0);
        }
    }

    void SwitchDirection()
    {
        isGoingLeft = !isGoingLeft;
    }


}