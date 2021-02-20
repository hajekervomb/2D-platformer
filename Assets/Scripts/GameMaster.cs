using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public static int maxLives = 3;
    [SerializeField] private static int _remainingLives;

    public static int RemainingLives
    {
        get { return _remainingLives; }
        set { _remainingLives = value; }
        
    }

    [SerializeField] private int startingMoney;
    public static int Money = 100;
       

    private void Awake()
    {        
        if (instance == null)
        {
            instance = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 3.5f;
    public Transform spawnPrefab;

    public CameraShake cameraShake;

    [SerializeField] private GameObject gameOverUI;

    //cache 
    private AudioManager audioManager;

    public string respawnCountDownName = "RespawnCountDown";
    public string spawnSoundName = "Spawn";
    [SerializeField] private string gameOverSoundName = "GameOver";

    [SerializeField] private GameObject upgradeMenu;

    [SerializeField] private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    private void Start()
    {
        Money = startingMoney;

        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referenced on Game Master");
        }

        _remainingLives = maxLives;

        //the black square fade in when we start the scene with this object
        if (LevelChanger.Instance != null)
        {
            LevelChanger.Instance.FadeIn();
        }

        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    private void ToggleUpgradeMenu()
    {        
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        // turn off methods:
        // player movement 
        // player shooting
        // enemy movement and shooting
        // wave countdown
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);

        //disable wave spawner
        waveSpawner.enabled = !upgradeMenu.activeSelf;
    }

    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName);
        Debug.Log("No lives remaining! End game!");
        //activate game ove UI
        gameOverUI.SetActive(true); 

        //play game over sound
    }

    public IEnumerator _RespawnPlayer()
    {
        audioManager.PlaySound(respawnCountDownName);
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        //spawn particles
        Transform spawnPrefabClone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(spawnPrefabClone.gameObject, 3.0f);

        //play spawn sound
        audioManager.PlaySound(spawnSoundName);
        
    }
    public static void KillPlayer(Player player)
    {
        
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            instance.EndGame();
            
            
        }
        else
        {
            instance.StartCoroutine(instance._RespawnPlayer());
        }
        
    }

    public static void KillEnemy(Enemy enemy)
    {
        instance._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        //play enemy death sound
        audioManager.PlaySound(_enemy.enemyDeathSoundName);

       //some camera shake
        cameraShake.Shake(_enemy.shakeAmount, _enemy.shakeLength);

        //spawn particles
        GameObject _clone = (GameObject)Instantiate(_enemy.enemyDeathParticles.gameObject, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone, 2f);

        //desttoy enemy
        Destroy(_enemy.gameObject);

        //add money
        Money += _enemy.moneyDropValue;
        audioManager.PlaySound("UpgradeComplete");
    }
        
}
