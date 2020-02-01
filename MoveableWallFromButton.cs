using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableWallFromButton : MonoBehaviour
{
    public float X;
    public float Y;
    public int level;

    private Vector3 targetArrive;
    private Vector3 targetStart;

    void Start()
    {
        targetArrive = new Vector3(transform.position.x + X, transform.position.y + Y);
        targetStart = transform.position;
    }

    void Update()
    {
        if (level != 3)
        {
            if (Buton.buttonPushed == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetArrive, 2 * Time.deltaTime);
            }

            if (Buton.buttonPushed == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetStart, 10 * Time.deltaTime);
            }
        }

        if (level == 3)
        {
            if (Buton2.buttonPushed == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetArrive, 2 * Time.deltaTime);
            }

            if (Buton2.buttonPushed == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetStart, 10 * Time.deltaTime);
            }
        }

    }

}