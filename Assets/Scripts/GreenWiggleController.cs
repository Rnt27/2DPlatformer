using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;

    public float moveSpeed;

    private Rigidbody2D myRigidbody;

    public bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (movingRight && transform.position.x > rightPoint.position.x)        //if we are moving right and our transform position is greater than our max right, we've gone too far
        {
            movingRight = false;
        }
        if(!movingRight && transform.position.x < leftPoint.position.x)
        {
            movingRight = true;
        }

        if (movingRight)                                                      //if we are moving right
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y);
        }
        else
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y);
        }

    }
}
