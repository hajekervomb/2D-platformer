using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int maxHealth = 100;

    private int curHealth;
    public int CurHealth
    {
        get { return curHealth; }
        set { curHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float healthRegenRate = 2f;

    public float maxSpeed = 10f;

    public float healthMultiplyer = 1.2f;
    public float speedMultiplyer = 1.2f;

    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
                
    }
}
