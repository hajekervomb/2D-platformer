using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienWeapon : MonoBehaviour
{
    //fire rate of the weapon
    [SerializeField]
    private float fireRate = 2f;
    [SerializeField]
    private int bulletSpeed = 10;
    //weapon damage
    public int damage = 40;
    //what to hit
    public LayerMask whatToHit;

    public Rigidbody2D bulletPrefab;
    public Transform hitPrefab;
   
    [SerializeField] //where to spawn bulletPrefab
    private Transform firePoint;

    //player
    [SerializeField]
    private Transform player;
    [SerializeField]
    private int rotationOffset = 0;
    
    //?
    private float timeToSpawnEffect = 0f;
    public float effectSpawnRate = 10f;

    //insert here prefab of muzzle flash
    [SerializeField]
    private Transform muzzleFlashPrefab;

    //looking for player boolean
    private bool searchingForPlayer = false;

    //no camera shaking 

    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Start()
    {
        if (firePoint == null)
        {
            Debug.LogError("Warning! The firepoint not found!");
        }

        if (player == null)
        {
            Debug.LogError("AlienWeapon cannot find player");
        }

        //TODO: Seatching for Player and Firepoint if no matched gameobjects in the scene
 
        

        StartCoroutine(SearchForPlayer());
    }

    

    private IEnumerator Shoot()
    {
        
        if (player == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }    

        if (player != null)
        {
            Shooting();
            WeaponRotation();
        }

        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Shoot());
    }

    private IEnumerator SearchForPlayer()
    {
        //searching for player. IF null - continue searching
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if (sResult == null)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(SearchForPlayer());
        }
        else //if find player gameObject
        {
            //start do shoot method
            player = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine(Shoot());
            yield return false;
        }
    }

    

    private void Shooting()
    {       
        Rigidbody2D bulletClone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector2 shootDir = (player.position - firePoint.position).normalized;
        bulletClone.velocity = new Vector2(shootDir.x, shootDir.y) * bulletSpeed;
        
    }

    private void WeaponRotation()
    {
        
        // substracting the position of the Alien Weapon from mouse position
        Vector3 difference = player.transform.position - transform.position;
        // normalizing the vector. Meaning that all the sum of the vector will be equal to 1.
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //find the tangle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
