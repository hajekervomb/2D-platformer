using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour
{          
    public int fallBoundary = -20;

    private AudioManager audioManager;

    [SerializeField] private string playerDeathSound = "PlayerDeath";
    [SerializeField] private string[] playerHitSounds = new string[] { "PlayerHit1", "PlayerHit2"};

    [SerializeField] private StatusIndicator statusIndicator;

    private PlayerStats stats;

    private void Start()
    {
        stats = PlayerStats.Instance;

        stats.CurHealth = stats.maxHealth;

        GameMaster.instance.onToggleUpgradeMenu += OnToggleUpgradeMenu;

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.CurHealth, stats.maxHealth);
        }

        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene");
        }

        InvokeRepeating("HealthRegen", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);
    }

    private void Update()
    {
        if (transform.position.y <= fallBoundary)
            DamagePlayer(99999999);
    }

    public void HealthRegen()
    {
        stats.CurHealth += 1;
        if (statusIndicator == null)
        {
            Debug.LogError("WARNING! NO STATUS INDICATOR REFERENCED!");
        }
        else
        {
            statusIndicator.SetHealth(stats.CurHealth, stats.maxHealth);
        }
    }

    private void OnToggleUpgradeMenu(bool active)
    {
        // handle the active state of Player script component
        // when upgrade menu is toggled
                
        GetComponent<Platformer2DUserControl>().enabled = !active;
        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null)
            _weapon.enabled = !active;               

    }

    public void DamagePlayer (int damage)
    {             
        stats.CurHealth -= damage;
        if(stats.CurHealth <= 0)
        {
            //play death sound
            audioManager.PlaySound(playerDeathSound);
            GameMaster.KillPlayer(this);
            Debug.Log("KILL PLAYER"); 
        }
        else
        {
            //play hit sound
            audioManager.PlaySound(playerHitSounds[Random.Range(0, playerHitSounds.Length)]);
        }

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.CurHealth, stats.maxHealth);
        }
    }

    private void OnDestroy()
    {
        GameMaster.instance.onToggleUpgradeMenu -= OnToggleUpgradeMenu;
    }
}
