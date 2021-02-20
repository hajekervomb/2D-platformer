using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning, //0
        Waiting, //1
        Counting  //2
    }

    //what a wave should be
    [System.Serializable]    
    public class Wave
    {
        public string name;
        public Transform enemy;
        public Transform bigEnemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;

    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public float timeBetweenWaves = 5f;
    [SerializeField]
    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }
    [SerializeField]
    private SpawnState state = SpawnState.Counting;
    public SpawnState State
    {
        get { return state; }
    }

    private float searchCountdown = 1f;
    [SerializeField]
    private Transform[] spawnPoints;

    private void Start()
    {
        //check for spawn poits on the scene
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point referenced");
        }
        //countdown of wave = the time between waves
        waveCountdown = timeBetweenWaves;        
    }

    private void Update()
    {
        //checking for enemies alive
        if (state == SpawnState.Waiting)
        {
            //check if enemies still alive
            if (!IsEnemyAlive())
            {
                //begin new round if no enemies alive
                WaveCompleted();                
            }
            else //if enemies still alive
            {
                return;
            }

        }

        //if wave countdown equals zero: spawn enemyes
        if (waveCountdown <= 0)
        {
            // Is spawn state equal spawning?
            if (state != SpawnState.Spawning)
            {
                //start spawn enemyes
                StartCoroutine("SpawnWave", waves[nextWave]);
            }
        }
        else //check the wave countdown time
        {
            waveCountdown -= Time.deltaTime;
        }
        
        
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave completed!");
        
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;
        
        if (nextWave + 1 > waves.Length - 1)
        {
            //if all waves completed - start again
            nextWave = 0;
            Debug.Log("All waves completed! Looping");
        }
        else
        {
            nextWave++;
        }
    }

    private bool IsEnemyAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }                     
                       
        }
        return true;

    }
    //TODO: toggle menu stop spawning enemies
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        state = SpawnState.Spawning;    //start spawning

        //start spawning enemyes with for loop, where count = number of enemyes that we want to spawn
        for (int i = 0; i < _wave.count; i++)
        {
            //spawn enemy once
            SpawnEnemy(_wave.enemy);
                        
            //wait certain amount of time after spawn an enemy

            //spawn
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        //spawn big Alien
        if (_wave.name == "RUN while you can!")
        {
            SpawnEnemy(_wave.bigEnemy);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    private void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawnign Enemy: " + _enemy.name);
        
        //choose spawn point 
        Transform _randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //spawn enemy code
        Instantiate(_enemy, _randomSpawnPoint.position, _randomSpawnPoint.rotation);
        
    }

}
