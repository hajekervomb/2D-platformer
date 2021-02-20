using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        private int curHealth;
        public int CurHealth
        {
            get { return curHealth; }
            set { curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 10;
        
                
        public void Init ()
        {
            CurHealth = maxHealth;
        }
    }

    [Header("Optional:")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    public EnemyStats stats = new EnemyStats();

    public Transform enemyDeathParticles;

    public string enemyDeathSoundName = "Explosion";

    public float shakeAmount = 0.1f;
    public float shakeLength = 0.1f;

    public int moneyDropValue = 10;

    private void Start()
    {
        stats.Init();

        GameMaster.instance.onToggleUpgradeMenu += OnToggleUpgradeMenu;
        
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.CurHealth, stats.maxHealth);
        }

        if (enemyDeathParticles == null)
        {
            Debug.LogError("No death particles referenced on Enemy");
        }
    }

    private void OnToggleUpgradeMenu(bool active)
    {        
        GetComponent<EnemyAI>().enabled = !active;
    }

    public void DamageEnemy(int damage)
    {
        stats.CurHealth -= damage;
        if (stats.CurHealth <= 0)
        {
            GameMaster.KillEnemy(this);
            Debug.Log("KILL ENEMY");
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.CurHealth, stats.maxHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D _colInfo)
    {
        //if hit player - do method DamagePlayer
        //else - do nothing

        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DamageEnemy(10);
            //Debug.Log("Enemy died");
        }

        
        /*if (_colInfo.gameObject.tag == "Player")
        {
            
            DamageEnemy(10);
        }*/
    }

    private void OnDestroy()
    {
        GameMaster.instance.onToggleUpgradeMenu -= OnToggleUpgradeMenu;
    }
}
