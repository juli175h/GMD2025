using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private void Start()
    {
        SoundManager.PlaySoundLoop(SoundType.MENU_BACKGROUND,0.1f);
        Button firstButton = canvas.GetComponentInChildren<Button>();
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }
    public void StartGame()
    {
        SoundManager.PlaySound(SoundType.SELECT);
        SceneManager.LoadScene("MainScene");
       
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
