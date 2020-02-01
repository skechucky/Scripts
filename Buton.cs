using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buton : MonoBehaviour
{
    public static bool buttonPushed;  // ->WallFomButon activate wall

    public int Level;

    private bool shurikenInTrigg;
    private bool redShurikenInTrigg;
    private bool butonTrigg;
    private bool playerInTrigger;

    public Sprite sprite1;
    public Sprite sprite2;

    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        shurikenInTrigg = false;
        redShurikenInTrigg = false;

        if (Level == 3)
        {
            OnOff.leverOn = false;
        }
        else buttonPushed = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInTrigger = false;
    }


    void Update()
    {
        if (butonTrigg == true)
        {
            //Debug.Log("1 2");
            spriteRenderer.sprite = sprite2;
            if (Level != 3)
            {
                buttonPushed = true;
            }
            else
            {
                OnOff.leverOn = true;
            }
        }

        if (butonTrigg == false && spriteRenderer.sprite == sprite2)
        {

            //Debug.Log("2 1");
            spriteRenderer.sprite = sprite1;
            if (Level != 3)
            {
                buttonPushed = false;
            }
            else
            {
                OnOff.leverOn = false;
            }
        }

        if (ShurikenRed.shurikenRedGone && butonTrigg == true && redShurikenInTrigg == true)
        {
            redShurikenInTrigg = false;
            butonTrigg = false;
            spriteRenderer.sprite = sprite1;
            //Debug.Log("3 1 red");
            if (Level != 3)
            {
                buttonPushed = false;
            }
            else
            {
                OnOff.leverOn = false;
            }
        }

        if (Shuriken.shurikenGone && butonTrigg == true && shurikenInTrigg == true && playerInTrigger == false)
        {

            shurikenInTrigg = false;
            butonTrigg = false;
            spriteRenderer.sprite = sprite1;
            Debug.Log("3 1");
            if (Level != 3)
            {
                buttonPushed = false;
            }
            else
            {
                OnOff.leverOn = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Shuriken" || collision.tag == "redShuriken")
        {
           // Debug.Log("P or S in trigger");
            butonTrigg = true;
        }
        if (collision.tag == "Player")
        {
            playerInTrigger = true;
        }
        if (collision.tag == "redShuriken")
        {
           // Debug.Log("red S in trigger");
            redShurikenInTrigg = true;
        }
        if (collision.tag == "Shuriken")
        {
           // Debug.Log(" S in trigger");
            shurikenInTrigg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (butonTrigg == true)
        {
            butonTrigg = false;
        }
        if (collision.tag == "Player")
        {
            //Debug.Log("P out of trigger");
            playerInTrigger = false;
        }
        if (collision.tag == "redShuriken")
        {
          //  Debug.Log("red S out of trigger");
            redShurikenInTrigg = false;
        }
        if (collision.tag == "Shuriken")
        {
           // Debug.Log(" S out of trigger");
            shurikenInTrigg = false;
        }
    }

}