using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private string mouseHoverSound = "ButtonHover";
    [SerializeField] private string pressButtonSound = "ButtonPress";

    private AudioManager audioManager;
    

    private void Start()
    {
        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No audiomanager found in the scene!");
        }
    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Debug.Log("APPLICATION QUIT");
        Application.Quit();
    }

    public void Retry()
    {
        audioManager.PlaySound(pressButtonSound);
        LevelChanger.Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex);            
    }

    public void OnMouseEnter()
    {
        audioManager.PlaySound(mouseHoverSound);
    }
}
