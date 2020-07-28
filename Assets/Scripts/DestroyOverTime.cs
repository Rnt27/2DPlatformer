using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime = lifeTime - Time.deltaTime;           //Time.deltaTime is every update loop, the amount of time it takes for a fram to happen.

        //Debug is never shown to the player
        //Debug.Log(Time.deltaTime);                      //To send message to yourself (send a message to the console that we want to read)

        if(lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
