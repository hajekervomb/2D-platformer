using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Text healthText;

    [SerializeField] private Text speedText;

    [SerializeField] private int upgradeCost = 50;

    private PlayerStats stats;
    
    private void OnEnable()
    {
        stats = PlayerStats.Instance;
        
        UpdateText();
        
    }

    public void UpgradeHealth()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.Instance.PlaySound("NoMoney");
            return;
        }
        stats.maxHealth = (int)(stats.maxHealth * stats.healthMultiplyer);
        GameMaster.Money -= upgradeCost;
        AudioManager.Instance.PlaySound("UpgradeComplete");
        UpdateText();

    }

    public void UpgradeSpeed()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.Instance.PlaySound("NoMoney");
            return;
        }
        stats.maxSpeed = Mathf.Round((int)(stats.maxSpeed * stats.speedMultiplyer));
        GameMaster.Money -= upgradeCost;
        AudioManager.Instance.PlaySound("UpgradeComplete");
        UpdateText();  
    }

    private void UpdateText()
    {
        healthText.text = "HEALTH: " + stats.maxHealth;
        speedText.text = "SPEED: " + stats.maxSpeed;
    }
}
