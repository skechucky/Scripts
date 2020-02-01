using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public static bool isDead; // start corotine ->menuScript  ->DeadCollision
    public static bool canTakeShurikenBack; // -> Shuriken
    public static bool canTakeShurikenBack2; // -> ShurikenRed
    public static bool shurikenIsThrown; // -> Gate

    public int Level;
    public float powerShootingShuriken; //370 // 330
    public float jumpHeight = 2f; // 2 squares
    public float jumpTimeToRoof = .4f; // 0.4 seconds

    private bool cantMoveOnDeath;
    private bool shurikenIsThrown2;
    private bool isFlipped;
    private bool throwAnimate;
    private int horizontal;
    private float TimeAir = .2f; //Mathf.SmoothDamp --float time
    private float TimeGround = .1f; //Mathf.SmoothDamp --float time
    private float moveSpeed = 3f;
    private float gravity;
    private float jumpVelocity;
    private float velocityXSmoothing;

    public GameObject shurikenPrefab;
    public GameObject shuriken2Prefab;
    public AudioSource jump;
    public AudioSource shurik;
    public AudioSource dead;

    private GameObject shurikenInstance;
    private GameObject shurikenInstance2;
    private GameObject shootingPoint1;
    private GameObject shootingPoint2;
    private Vector3 velocity;
    private Vector3 syncWithPlatform;
    private Animator anime;
    private Controller2D controller;

    void Start()
    {
        canTakeShurikenBack = false;
        shurikenIsThrown = false;
        isDead = false;
        cantMoveOnDeath = false;
        syncWithPlatform = new Vector3(MovablePlatform.speed, 0f, 0f);
        
        controller = GetComponent<Controller2D>();
        anime = GetComponent<Animator>();
        shootingPoint1 = GameObject.FindGameObjectWithTag("shootingPoint1");
        shootingPoint2 = GameObject.FindGameObjectWithTag("shootingPoint2");

        if (Level == 3)
        {
            shurikenIsThrown2 = true;
            canTakeShurikenBack2 = false;
        }
        // deltaMovement = velocityInitial * time + (acceleration * time^2)/2
        // jumpHeight = (gravity * jumpTimeToRoof^2)/
        // 2 * jumpHeight = gravity * jumpTimeToRoof^2
        // (2 * jumpHeight) / gravity = jumpTimeToRoof ^ 2
        // gravity / 2 * jumpHeight = 1 / jumpTimeToRoof
        // gravity = 2 * jumpHeight / jumpTimeToRoof^2

        // velocityFinal = velocityInitial + acceleration * time
        // jumpVelocity = gravity * jumpTimeToRoof
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpTimeToRoof, 2);
        jumpVelocity = Mathf.Abs(gravity) * jumpTimeToRoof;
    }

    void Update()
    {
        HorizontalSet(); // -1/0/1
        Esc(); // back to main menu
        Animations();
        if (JumpTrigger.jumpOn)  // check if is on Movable Platform
        {
            if (MovablePlatform.isGoingLeft)
            {
                transform.Translate(-syncWithPlatform.x * Time.deltaTime, 0f, 0f);
            }else
                transform.Translate(syncWithPlatform.x * Time.deltaTime, 0f, 0f);
        }

        //print("Above: " + controller.collisions.above + "  Below: " + controller.collisions.below);
        // print("Left: " + controller.collisions.left + "  Right: " + controller.collisions.right);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if ((Input.GetKeyDown(KeyCode.W) && controller.collisions.below)||(Input.GetKeyDown(KeyCode.UpArrow) && controller.collisions.below))
        {
            jump.Play();
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;

        if (isDead==false && cantMoveOnDeath==false)
        {
            FlipVerification();
            ChuckThatKnife();

            if (controller.collisions.below)
            {
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, TimeGround);
            }else
             {
                 velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, TimeAir);
             }       
        }else if (isDead && cantMoveOnDeath)
         {
             velocity.x = 0;
             dead.Play();
             isDead = false;
         }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Animations()
    {
        if (horizontal == 0)
        {
            anime.SetBool("Idle", true);
            anime.SetBool("Walk", false);
            anime.SetBool("Jump", false);
            anime.SetBool("Throw", false);
        }else
        {
            anime.SetBool("Idle", false);
            anime.SetBool("Walk", true);
            anime.SetBool("Jump", false);
            anime.SetBool("Throw", false);
        }

        if (controller.collisions.below == false)
        {
            anime.SetBool("Idle", false);
            anime.SetBool("Walk", false);
            anime.SetBool("Jump", true);
            anime.SetBool("Throw", false);
        }

        if (throwAnimate == true)
        {
            if (isDead == true)
            {
                cantMoveOnDeath = true;
                anime.SetBool("Dead", true);
                anime.SetBool("Throw", false);
                anime.SetBool("Walk", false);
                anime.SetBool("Jump", false);
                anime.SetBool("Idle", false);
            }else{
                anime.SetBool("Throw", true);
                anime.SetBool("Idle", false);
                anime.SetBool("Walk", false);
                anime.SetBool("Jump", false);
                throwAnimate = false;
              }
        }

        if (isDead == true)
        {
            cantMoveOnDeath = true;
            anime.SetBool("Dead", true);
            anime.SetBool("Throw", false);
            anime.SetBool("Walk", false);
            anime.SetBool("Jump", false);
            anime.SetBool("Idle", false);
        }
    }

    void HorizontalSet()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
            horizontal = -1;
        else horizontal = 1;

        if (Input.GetAxisRaw("Horizontal") == 0)
            horizontal = 0;
    }

    void FlipVerification()
    {
        if (velocity.x < 0 && isFlipped == false)
        {
            Flip();
            isFlipped = true;
        }
        else if (velocity.x > 0 && isFlipped == true)
        {
            Flip();
            isFlipped = false;
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = -theScale.x;
        transform.localScale = theScale;
    }

    void ChuckThatKnife()
    {
        if (Level == 3) // ShurikenRed
        {
            if (Input.GetKeyDown(KeyCode.Space) && shurikenIsThrown2 == false)
            {
                shurik.Play();
                shurikenIsThrown2 = true;

                if (horizontal == 0)
                {
                    shurikenInstance2 = Instantiate(shuriken2Prefab, shootingPoint1.transform.position, shootingPoint1.transform.rotation) as GameObject;
                    shurikenInstance2.GetComponent<Rigidbody2D>().AddForce(shuriken2Prefab.transform.up * 280f * Time.deltaTime, ForceMode2D.Impulse);
                }
                else if (horizontal == 1)
                {
                    shurikenInstance2 = Instantiate(shuriken2Prefab, shootingPoint2.transform.position, shootingPoint2.transform.rotation) as GameObject;
                    shurikenInstance2.GetComponent<Rigidbody2D>().AddForce(shurikenInstance2.transform.right * powerShootingShuriken * Time.deltaTime, ForceMode2D.Impulse);
                }
                else if (horizontal == -1)
                {
                    shurikenInstance2 = Instantiate(shuriken2Prefab, shootingPoint2.transform.position, shootingPoint2.transform.rotation) as GameObject;
                    shurikenInstance2.GetComponent<Rigidbody2D>().AddForce(-shurikenInstance2.transform.right * powerShootingShuriken * Time.deltaTime, ForceMode2D.Impulse);
                }

                throwAnimate = true;

            }
            else if (Input.GetKeyDown(KeyCode.Space) && shurikenIsThrown2 == true && ShurikenRed.shurikenInPlayer2 == true)
            {
                canTakeShurikenBack2 = true;
                shurikenIsThrown2 = false;
            }


        }
            if (Input.GetKeyDown(KeyCode.Space) && shurikenIsThrown == false)
            {
                shurik.Play();
                shurikenIsThrown = true;

                if (horizontal == 0)
                {
                    shurikenInstance = Instantiate(shurikenPrefab, shootingPoint1.transform.position, shootingPoint1.transform.rotation) as GameObject;
                    shurikenInstance.GetComponent<Rigidbody2D>().AddForce(shurikenPrefab.transform.up * 280f * Time.deltaTime, ForceMode2D.Impulse);
                }
                else if (horizontal == 1)
                {
                    shurikenInstance = Instantiate(shurikenPrefab, shootingPoint2.transform.position, shootingPoint2.transform.rotation) as GameObject;
                    shurikenInstance.GetComponent<Rigidbody2D>().AddForce(shurikenInstance.transform.right * powerShootingShuriken * Time.deltaTime, ForceMode2D.Impulse);
                }
                else if (horizontal == -1)
                {
                    shurikenInstance = Instantiate(shurikenPrefab, shootingPoint2.transform.position, shootingPoint2.transform.rotation) as GameObject;
                    shurikenInstance.GetComponent<Rigidbody2D>().AddForce(-shurikenInstance.transform.right * powerShootingShuriken * Time.deltaTime, ForceMode2D.Impulse);
                }

                throwAnimate = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && shurikenIsThrown == true && Shuriken.shurikenInPlayer == true)
            {
                canTakeShurikenBack = true;
                shurikenIsThrown = false;
            }
    }

    void Esc()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            
        }
    }

}

        
