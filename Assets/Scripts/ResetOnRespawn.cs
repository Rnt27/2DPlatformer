using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {

        startPosition = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        //what if the object doesn't have rigid body?
        if (GetComponent<Rigidbody2D>() != null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //to reset object
    public void ResetObject()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        //if we didn't set a value to myRigidBody
        if (myRigidbody != null)
        {
            //Setting it to zero
            myRigidbody.velocity = Vector3.zero;
        }
    }
}
