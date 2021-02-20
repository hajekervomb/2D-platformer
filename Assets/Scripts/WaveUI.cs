using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    private WaveSpawner spawner;

    [SerializeField]
    private Animator waveAnimator;

    [SerializeField]
    private Text waveCountdownText;

    [SerializeField]
    private Text waveCountText;

    private WaveSpawner.SpawnState previousState;

    private void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("No spawner referenced!");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced!");
            this.enabled = false;
        }
        if (waveCountdownText == null)
        {
            Debug.LogError("No waveCountdownText referenced!");
            this.enabled = false;
        }
        if (waveCountText == null)
        {
            Debug.LogError("No waveCountText referenced!");
            this.enabled = false;
        }       
    }

    private void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.Spawning:
                UpdateSpawningUI();
                break;
            
            case WaveSpawner.SpawnState.Counting:
                UpdateCountingUI();
                break;
            
        }

        previousState = spawner.State;
    }

    private void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.Counting)
        {
            Debug.Log("COUNTING");
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }

    private void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.Spawning)
        {
            Debug.Log("SPAWNING");
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCountText.text = spawner.NextWave.ToString();
        }
        
    }
}
