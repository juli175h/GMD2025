using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the UI Text component
    private static float elapsedTime = 0f; // Tracks the timer's time
    private bool isRunning = false; // Controls whether the timer is running
    public static Timer instance; // Singleton instance to persist across scenes

    void Awake()
    {
        // Ensure only one Timer instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Preserve the Timer across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        
    }


    void FixedUpdate()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    // Start the timer
    public void StartTimer()
    {
        if (!isRunning)
        isRunning = true;
    }

    // Stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }

    // Reset the timer (optional)
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }

    // Update the displayed time in mm:ss.ms format
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        float milliseconds = (elapsedTime % 1) * 100; // Get milliseconds as two decimal places

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, Mathf.FloorToInt(milliseconds));
        }
    }

    // Update the UI reference for the timer
    public void SetTimerText(TextMeshProUGUI newText)
    {
        timerText = newText;
    }

}
