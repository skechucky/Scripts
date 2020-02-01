using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableWall2FromLever : MonoBehaviour
{
    public int powerTargetArrive;
    public int powerTargetStart;
    public float X;
    public float Y;
    public int Level;

    private GameObject rock;
    private Vector3 targetArrive;
    private Vector3 targetStart;

    void Start()
    {
        targetArrive = new Vector3(transform.position.x + X, transform.position.y + Y);
        targetStart = transform.position;

        if (Level == 4)
        {
            rock = GameObject.FindGameObjectWithTag("rock");
        }
    }

    void Update()
    {
        if (OnOff.leverOn == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetArrive, powerTargetArrive * Time.deltaTime);
            GetComponent<BoxCollider2D>().enabled = true;
            if (Level == 4)
            {
                rock.SetActive(false);
                rock.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        if (OnOff.leverOn == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetStart, powerTargetStart * Time.deltaTime);
            GetComponent<BoxCollider2D>().enabled = false;
            if (Level == 4)
            {
                rock.GetComponent<BoxCollider2D>().enabled = true;
                rock.SetActive(true);
            }
        }

    }

}