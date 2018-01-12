using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int health;
    public float speed;
    public int numberOfPickups;
    public int dps;
    public Text countText;
    public Text winText;
    public Text timerText;
    public Text healthText;
    public Text scoreText;

    private Rigidbody2D rb2d;
    private int count;
    private float timer = 0.0f;
    private int seconds;
    private bool complete = false;
    private float score = 0.0f;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        health = 100;
        winText.text = "";
        timerText.text = "";
        scoreText.text = "";
        SetHealthText();
        SetCountText();
    }

    void FixedUpdate()
    {
        if (complete == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
            timer += Time.deltaTime;
            seconds = (int) timer;
        }
        
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (count < numberOfPickups)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                count++;
                SetCountText();
            }

            if (other.gameObject.CompareTag("Background"))
            {
                health = health - dps;
                SetHealthText();
            }
        }
                   
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= numberOfPickups)
        {
            complete = true;
            winText.text = "You Win!";
            timerText.text = "Level Completed in " + timer.ToString("0.00") + " seconds";
            score = health * 1 / timer * 1000;
            score = (int)score;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void SetHealthText()
    {
        healthText.text = health.ToString();
    }
}

