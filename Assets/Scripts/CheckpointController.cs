﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Sprite flagClosed;
    public Sprite flagOpen;

    private SpriteRenderer theSpriteRenderer;                   //To keep the same sprite renderer

    public bool checkpointActive;                               //to check if checkpoint is active

    // Start is called before the first frame update
    void Start()
    {

        theSpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player")
        {
            theSpriteRenderer.sprite = flagOpen;
            checkpointActive = true;
        }
        
    }

}
