using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                           //To be able to use the UI things, not put automatically

public class LevelManager : MonoBehaviour                   //Controls things outside of the ability of objects, like deactivating object
{
    public float waitToRespawn;
    public PlayerController thePlayer;                  //Reference to an object that is PlayerController which we will call thePlayer

    public GameObject deathSplosion;                    //Reference to the particle explosion

    public int coinCount;                           //Keeping track on how many coins we have

    public Text cointText;                          //IU for coin count

    public Image heart1;                            //Keeping track of the hearts and their sprites
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;                           //different heart tracks
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int healthCount;                             //Current health

    private bool respawning;

    //want to find every object in the world that has ResetOnSpawn, need an array
    private ResetOnRespawn[] objectsToReset;

    //invincible on knockback
    public bool invincible;

    //keeping track of lives
    public int currentLives;
    public int startingLives;
    public Text livesText;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();             //Find object that has PlayerController script attached to it.

        cointText.text = "Coins: " + coinCount;                         //starting with countCount;

        healthCount = maxHealth;

        //setting up the array
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        currentLives = startingLives;
        livesText.text = "Lives x " + currentLives;

        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(healthCount <= 0 && !respawning)                            //if no health and we are not respawning
        {
            Respawn();
            respawning = true;
        }
    }

    public void Respawn()
    {
        //taking out lives when player dies
        currentLives -= 1;
        livesText.text = "Lives x " + currentLives;

        if (currentLives > 0)
        {

            StartCoroutine("RespawnCo");                                //How to start a Co-routine

        }
        else
        {
            //out of lives, deactivate player
            thePlayer.gameObject.SetActive(false);

            gameOverScreen.SetActive(true);
        }
    }

    //respawn overload to fix the killplane issue
    public void Respawn(int health)
    {
        healthCount = health;
        UpdateHeartMeter();

    }

    public IEnumerator RespawnCo()                                      //Co-routine, can add time running outside of game in order not to break it.
    {
        //Now can mess with time a bit
        thePlayer.gameObject.SetActive(false);                          //deactivating Player

        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);                //to create an object, in this case the particle explosion, related to player

        yield return new WaitForSeconds(waitToRespawn);                 //Game waits as much time as waitToRespawn, then moves and reactivates

        healthCount = maxHealth;                                        //Needs respawning boolean to avoid looping forever, otherwise character appears with 0 hp and dies and this loops
        UpdateHeartMeter();

        //Punishing player for dying
        coinCount = 0;
        cointText.text = "Coins: " + coinCount;

        respawning = false;                                             //allowed to respawn

        thePlayer.transform.position = thePlayer.respawnPosition;       //respawnPosition is in Player script as public
        thePlayer.gameObject.SetActive(true);

        //looping through array
        for(int i = 0; i < objectsToReset.Length; i++)
        {
            //resetting each, calling reset first is not good, need to set it active first 
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
            
        }

    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;                                          //adding coinCount and coinstoAdd

        cointText.text = "Coins: " + coinCount;
    }

    public void HurtPlayer(int damageToTake)                                                //when player gets hurt
    {
        if (!invincible)
        {

            healthCount -= damageToTake;                                            //removing damage from health

            UpdateHeartMeter();

            //knockback
            thePlayer.Knockback();
        }
    }

    public void GiveHealth(int healthToGive)
    {

        healthCount += healthToGive;                                            //removing damage from health

        //don't go above max health
        if(healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }

        UpdateHeartMeter();
    }

    public void UpdateHeartMeter()                                       //Helper function to update the heart meter
    {
        switch (healthCount)                                            //Covering all the other cases for health
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }

    public void AddLives(int livesToAdd)
    {

        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;

    }

}
