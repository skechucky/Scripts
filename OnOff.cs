using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public static bool leverOn; // used here + -> Wall2FromLever + <-button level 3

    private int ok; // used in trigger functions

    public Sprite sprite1;
    public Sprite sprite2;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        ok = 1;
        leverOn = false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = sprite1;
        }

    }

    void Update()
    {
        if (leverOn == true)
        {
            spriteRenderer.sprite = sprite2;
        }

        if (leverOn == false)
        {
            spriteRenderer.sprite = sprite1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player")&&(ok==1))
        {   
            leverOn = true;
        }
        if ((collision.tag == "Player") && (ok == 0))
        {
            leverOn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (leverOn == true)
        {
            ok = 0;
        }
        else ok = 1;
    }

}
