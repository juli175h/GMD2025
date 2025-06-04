using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public float countdownTime = 300f; // 5 minutes = 300 seconds
    private bool isTimerRunning = true;
    private float currentTime;
    public GameObject PauseCanvas;
    public GameObject GameOverCanvas;
    public InputAction PauseGameAction;
    public bool isPaused = false; 

    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerDisplay();
        PauseGameAction.Enable();
        PauseGameAction.performed += OnPauseGamePressed;
        SoundManager.PlaySoundLoop(SoundType.GAME_BACKGROUND, 0.05f);
    }

    void Update()
    {
        if (!isTimerRunning) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            GameOver(true);
            isTimerRunning = false;
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:0} : {1:00}", minutes, seconds);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (PauseCanvas.gameObject.active)
            PauseCanvas.gameObject.SetActive(false);


    }

    public void GameOver(bool survived)
    {
        PauseGameAction.Disable();
        Pause();
        SoundManager.StopLoopingSound();
        GameOverCanvas.gameObject.SetActive(true);
        TextMeshProUGUI text = GameOverCanvas.GetComponentInChildren<TextMeshProUGUI>();
        if (survived)
        {
            SoundManager.PlaySound(SoundType.WIN, 0.2f);
            text.text = "You survived!";

        }
        else
        {
            SoundManager.PlaySound(SoundType.LOSE, 0.2f);
            text.text = "You died!";
        }
            Button firstButton = GameOverCanvas.GetComponentInChildren<Button>();
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }

    public void ExitToMeny()
    {
        SoundManager.PlaySound(SoundType.SELECT);
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        SoundManager.PlaySound(SoundType.SELECT);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void OnPauseGamePressed(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
            PauseCanvas.gameObject.SetActive(false);
        }
        else
        {
            Pause();
            PauseCanvas.gameObject.SetActive(true);
            Button firstButton = PauseCanvas.GetComponentInChildren<Button>();
            if(firstButton != null)
            {
                EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
            }
        }
            
    }

}
