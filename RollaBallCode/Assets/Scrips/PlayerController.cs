using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;
    public int maxPoints;
    public GameObject winTextObject;
    private Timer timer;
    private Button nextLevel;
    private TextMeshProUGUI finalTime;


    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        //SetCountText();
        winTextObject.SetActive(false);
        timer = Timer.instance;
        
        timer.SetTimerText(GameObject.Find("CountText").GetComponent<TextMeshProUGUI>());
        timer.StartTimer();
        try
        {
            nextLevel = GameObject.Find("NextLevelButton").GetComponent<Button>();
            nextLevel.gameObject.SetActive(false);

        }
        catch { nextLevel = null; }
        try
        {
            finalTime = GameObject.Find("FinalTimeLabel").GetComponent<TextMeshProUGUI>();
            finalTime.gameObject.SetActive(false);

        }
        catch { finalTime = null; }

    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        if (count >= maxPoints)
        {
            winTextObject.SetActive(true);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            //SetCountText();
            if (count >= maxPoints)
            {
                winTextObject.SetActive(true);
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(enemy);
                }
                timer.StopTimer();
                if (nextLevel != null)
                    nextLevel.gameObject.SetActive(true);
                else if(finalTime != null)
                {
                    timer.StopTimer();
                    finalTime.GetComponent<TextMeshProUGUI>().text = "Time: "+ timer.timerText.text;
                    finalTime.gameObject.SetActive(true);

                }
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            timer.StopTimer();
        }
    }
}