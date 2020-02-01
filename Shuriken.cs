using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public static bool shurikenInPlayer; // ->Player
    public static bool shurikenGone; // ->Buton

    private bool campFire;

    public GameObject fire;

    void Start()
    {
        campFire = false;
        shurikenInPlayer = false;
        shurikenGone = false;
    }

    void Update()
    {
        if (campFire == true)
        {
            Instantiate(fire, transform.position, transform.rotation);
            Player.canTakeShurikenBack = false;
            shurikenInPlayer = false;
            shurikenGone = true;
        }

        if (Player.canTakeShurikenBack == true && shurikenInPlayer == true)
        {
            Player.canTakeShurikenBack = false;
            shurikenInPlayer = false;
            Debug.Log("shuriken out");
            shurikenGone=true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shurikenInPlayer = true;
        }

        if (collision.tag == "CamFire" || collision.tag == "Enemy")
        {
            campFire = true;
            StartCoroutine(wait());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shurikenInPlayer = false;
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        Player.isDead = true;
    }

}