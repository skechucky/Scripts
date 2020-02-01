using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static bool canPassGate; //verification if can pass gate -> LevelLoad

    private bool playerInGateTrigg;

    public Sprite closed;
    public Sprite open;
    public Sprite openWithoutShuriken;

    private SpriteRenderer spriteRenderer;
    private Vector3 initialPos;

    void Start()
    {
        Key.keyTaken = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closed;
        initialPos = transform.position;
        canPassGate = false;
    }

    void Update()
    {
        if ((Key.keyTaken == true)&&(spriteRenderer.sprite == closed))      
        {
            spriteRenderer.sprite = open;
        }

        if (playerInGateTrigg == true && spriteRenderer.sprite == open && Player.shurikenIsThrown==true)
        {
            transform.position = new Vector2(transform.position.x - 0.6154f, transform.position.y + 0.5f);
            spriteRenderer.sprite = openWithoutShuriken;
        }

        if (playerInGateTrigg == false && spriteRenderer.sprite == openWithoutShuriken)
        {
            transform.position = initialPos;
            spriteRenderer.sprite = open;
        }

        if (playerInGateTrigg == true && Player.shurikenIsThrown == false && Key.keyTaken==true)
        {
            transform.position = initialPos;
            spriteRenderer.sprite = open;
            canPassGate = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            playerInGateTrigg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInGateTrigg = false;
        }
    }

}