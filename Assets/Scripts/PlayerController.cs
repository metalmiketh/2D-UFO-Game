using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int health;
    public int damageFromWalls;
    public int MovementModifier;
    public int inputModifier;
    public int numberOfPickups;    
    public Text countText;
    public Text winText;
    public Text timerText;
    public Text healthText;
    public Text scoreText;
    public Button quitButton;

    private Rigidbody2D rb2d;
    private int PickupsCollected;    
    private int CurrentSpeed;
    private float Score = 0.0f;
    private float Timer = 0.0f;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PickupsCollected = 0;
        health = 100;
        winText.text = "";
        timerText.text = "";
        scoreText.text = "";
        SetHealthText();
        SetCountText();
        quitButton.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (PickupsCollected < numberOfPickups)
        {
            if (health > 0)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                moveHorizontal *= inputModifier;
                moveVertical *= inputModifier;
                Vector2 movement = new Vector2(moveHorizontal, moveVertical);
                rb2d.AddForce(movement * MovementModifier);
                CurrentSpeed = (int)movement.magnitude;
                Timer += Time.deltaTime;
            }
            else if (health <= 0)
            {
                winText.text = "GAME OVER";
                health = 0;
                winText.fontSize = 50;
                SetHealthText();
                quitButton.gameObject.SetActive(true);
            }
        }
        else
        {
            winText.text = "You Win!";
            timerText.text = "Level Completed in " + Timer.ToString("0.00") + " seconds";
            Score = health * (1 / Timer) * 1000;
            Score = (int)Score;
            quitButton.gameObject.SetActive(true);
            if (Score > 999)
            {
                scoreText.text = "Score: " + Score.ToString("0,000");
            }
            else
            {
                scoreText.text = "Score: " + Score.ToString();
            }
        }
              
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (PickupsCollected < numberOfPickups)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                PickupsCollected++;
                SetCountText();
            }

            if (other.gameObject.CompareTag("Background"))
            {                
                health -= (damageFromWalls * CurrentSpeed);
                SetHealthText();
            }
        }                   
    }

    void SetCountText()
    {
        countText.text = "Count: " + PickupsCollected.ToString();
    }

    void SetHealthText()
    {
        if (health <= 0)
        {
            healthText.text = "FUCKED IT!";
        } else
        {
            healthText.text = "Health: " + health.ToString();
        }        
    }
}