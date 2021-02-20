using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string hoverOverSound = "ButtonHover";
    [SerializeField] private string pressButtonSound = "ButtonPress";

    private AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found!");
        }
    }

    public void StartGame ()
    {
        //play sounds
        audioManager.PlaySound(pressButtonSound);
        // load main game scene        
        LevelChanger.Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void Quit()
    {
        audioManager.PlaySound(pressButtonSound);
        // quit our game
        Debug.Log("APPLICATION QUIT");
        Application.Quit();
       
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
}
