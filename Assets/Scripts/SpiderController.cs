using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public float moveSpeed;

    private bool canMove;

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)                    //when it can move
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y);
        }
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillPlane")
        {
            //don't make it destroyed, set it to false
            //Destroy(gameObject);

            gameObject.SetActive(false);
        }
    }

    //to solve bool moving problem of spider
    void OnEnable()
    {
        //since spider is waking up for the first time
        canMove = false;
    }
}
