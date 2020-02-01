using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int Level; // if it is 6 , enemy will patrol
    public float distance = 5f;
    public float speed2 = 1f;
    public float speed;
    public float RangeWalk;
    public float RangeAtack;
    public float distFromStart;
    public bool enemyDie;

    private int flip;
    private float Range;
    private bool iAmCalled;
    private bool isGoingLeft = false;
    private bool stopPatrol;

    public GameObject fireExpl;

    private Vector3 startPosition;
    private Transform Target;
    private Animator anime;
    private GameObject Playerr;
    private Vector3 velocity;


    void Start()
    {
        enemyDie = false;
        iAmCalled = false;
        anime = GetComponent<Animator>();
        Playerr = GameObject.FindGameObjectWithTag("Player");
        Target = Playerr.transform;
        flip = 1;

        if (Level == 6)
        {
            startPosition = gameObject.transform.position;
            velocity = new Vector3(speed2, 0, 0);
            transform.Translate(velocity.x * Time.deltaTime, 0, 0);
        }
    }

    void SwitchDirection()
    {
        isGoingLeft = !isGoingLeft;
    }

    void Update()
    {

        if (Level == 6 && stopPatrol==false)
        {
            distFromStart = transform.position.x - startPosition.x;
            anime.SetBool("walk", true);

            if (isGoingLeft)
            {
                if (flip == 1)
                {
                    Flip();
                    flip = 2;
                }

                if (distFromStart < -distance)
                    SwitchDirection();

                transform.Translate(-velocity.x * Time.deltaTime, 0, 0);
            }
            else
            {
                if (flip == 2)
                {
                    Flip();
                    flip = 1;
                }

                if (distFromStart > distance)
                    SwitchDirection();

                transform.Translate(velocity.x * Time.deltaTime, 0, 0);
            }
        }
        if (enemyDie == true && !iAmCalled )
        {
           
            enemyDie = false;
            Instantiate(fireExpl, transform.position, transform.rotation);
            iAmCalled = true;
            Destroy(gameObject);
        }

        Range = Vector2.Distance(transform.position, Playerr.transform.position);

        if (Range < RangeWalk)
        {
            stopPatrol = true;
            StartWalk();
            anime.SetBool("walk", true);
            anime.SetBool("atak", false);
        }else stopPatrol = false;

        if (Range <= RangeAtack)
        {
            
            StartCoroutine(wait(0.3f));
            anime.SetBool("atak", true);
            anime.SetBool("walk", false);
        }

        if ((Range >= RangeWalk) && Level!=6) 
        {
            anime.SetBool("walk", false);
            anime.SetBool("atak", false);            
        }
    }

    public void StartWalk()
    {
        
            if (transform.position.x > Target.transform.position.x) // enemy right
            {
                if (flip == 1)
                {
                    Flip();
                    flip = 2;
                }
                transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            }
            else if (transform.position.x < Target.transform.position.x) //enemy left
            {
                if (flip == 2)
                {
                    Flip();
                    flip = 1;
                }
                transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            } 
    }

    private IEnumerator wait(float a)
    {
        yield return new WaitForSeconds(a);

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(wait(0.3f));
            Player.isDead = true;
        }
        if (collision.tag == "Shuriken")
        {
            //Shuriken.campFire = true;
            StartCoroutine(wait(0.45f));
            //Player.isDead = true;
        }
        if (collision.tag == "CamFire")
        {
            enemyDie = true;
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = -theScale.x;
        transform.localScale = theScale;
    }

}

