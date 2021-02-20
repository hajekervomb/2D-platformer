using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviourSingletonPersistent<LevelChanger>
{
    
    [SerializeField] private Animator animator;
    private int levelToLoad;
       

    public void FadeToLevel (int buildIndex)
    {
        levelToLoad = buildIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
}
