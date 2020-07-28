using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRigidBody;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;                  //for ground

    public bool isGrounded;

    private Animator myAnim;                        //to animate

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;            //reference to LevelManager

    public GameObject stompBox;

    //variable to control knockback
    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter; //amount of time that we can be knockout

    //longer invincibility than knockback
    public float invincibilityLength;
    private float invincibilityCounter;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        respawnPosition = transform.position;           //setting first respawn position

        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);    //check if player is grounded, if all of that is true or false.

        //if there's no knockback happening
        if (knockbackCounter <= 0)
        {

            if (Input.GetAxisRaw("Horizontal") > 0f) //+1 to the right, -1 left, 0f means 0 float
            {
                myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y);      //Vector3 is an Object that has x,y,z values
                transform.localScale = new Vector3(1f, 1f, 1f);                             //Facing to the right

            }
            else if (Input.GetAxisRaw("Horizontal") < 0f) //-1 to the left, 0f means 0 float
            {
                myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y);      //Object that has x,y,z values
                transform.localScale = new Vector3(-1f, 1f, 1f);                             //Facing to the left

            }
            else
            {
                myRigidBody.velocity = new Vector3(0f, myRigidBody.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed);
            }
                        
        }

        if (knockbackCounter > 0)
        {

            knockbackCounter -= Time.deltaTime;

            //knock opposite to where it was hit
            //if facing right
            if (transform.localScale.x > 0)
            {
                myRigidBody.velocity = new Vector3(-knockbackForce, knockbackForce, 0F);
            }
            else
            {
                myRigidBody.velocity = new Vector3(knockbackForce, knockbackForce, 0F);
            }

        }

        if(invincibilityCounter > 0)
        {

            invincibilityCounter -= Time.deltaTime;

        }

        if(invincibilityCounter <= 0)
        {

            theLevelManager.invincible = false;

        }

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);

        //activating stomp box only when going down
        if(myRigidBody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }

    }

    //used whenever the player is hurt
    public void Knockback()
    {
        knockbackCounter = knockbackLength;

        invincibilityCounter = invincibilityLength;

        theLevelManager.invincible = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "KillPlane")            //Checking if the tag is the trigger that we want
        {

            //gameObject.SetActive(false);        //set character as inactive

            //transform.position = respawnPosition;         //Player on itself to respawn, but now we have a better respawn

            //theLevelManager.Respawn();                      //Using theLevelManager respawn

            //more precise respawn to fix bug
            theLevelManager.Respawn(0);

        }

        if(other.tag == "CheckPoint")           //For checkpoing
        {

            respawnPosition = other.transform.position;
             
        }

    }

    //Moving Platform is not a trigger, so we'll use on collision enter
    private void OnCollisionEnter2D(Collision2D other)                          //whenever player collides against another object
    {
        
        if(other.gameObject.tag == "MovingPlatform")                            //collision is not the same as collider
        {
            transform.parent = other.transform;
        }

    }

    private void OnCollisionExit2D(Collision2D other)                           //When we stop colliding with this object
    {
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;                                            //removing parent
        }
    }

}
