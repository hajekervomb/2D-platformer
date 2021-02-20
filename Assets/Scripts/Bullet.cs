using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{     
        
    [SerializeField]
    private int bulletDamage = 40;
    [SerializeField]
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
                
    }
    
    private void OnTriggerEnter2D(Collider2D _colInfo)
    {
        //hit player - do damage player method
        //else destroy bullet
        Player _player = _colInfo.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(bulletDamage);
            Debug.Log("Deal " + bulletDamage + "  damage");
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }   
    
}
