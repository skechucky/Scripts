using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenRed : MonoBehaviour
{
    public static bool shurikenInPlayer2; //-> Player
    public static bool shurikenRedGone; //-> Buton/Buton2

    private bool campFire;

    public GameObject fire;

    void Start()
    {
        campFire = false;
        shurikenInPlayer2 = false;
        shurikenRedGone = false;
    }

    void Update()
    {
        if (campFire == true)
        {
            Instantiate(fire, transform.position, transform.rotation);
        }

        if (Player.canTakeShurikenBack2 == true && shurikenInPlayer2 == true)
        {
            Player.canTakeShurikenBack2 = false;
            shurikenInPlayer2 = false;
            shurikenRedGone = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision2)
    {
        if (collision2.tag == "Player")
        {
            shurikenInPlayer2 = true;
        }
        if (collision2.tag == "CamFire")
        {
            campFire = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision2)
    {
        if (collision2.tag == "Player")
        {
            shurikenInPlayer2 = false;
        }
        if (collision2.tag == "CamFire")
        {
            campFire = false;
        }
    }

}