using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    public float bounceForce;

    public GameObject deathSplosion;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            //makes it disappear, it's better to set active false
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);

            Instantiate(deathSplosion, other.transform.position, other.transform.rotation);

            //adding bounce
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, bounceForce, 0F);
        }
    }
}
