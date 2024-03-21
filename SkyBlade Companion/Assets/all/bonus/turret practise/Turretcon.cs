

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Turretcon : MonoBehaviour
{

    public LayerMask playerLayer; // Layer mask for the player
    public float rotationSpeed = 5f;
    public float firedelay = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float detectionRange = 5f;
    private Animator anim;
    public bool isPlayerDetected = false;
    public bool isShooting = false;
    //public int damageamount;
    private Coroutine turretroutine;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for player detection
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);

        if (colliders.Length > 0)

        {
            Debug.Log(colliders.Length);
            isPlayerDetected = true;
            RotateTowardsPlayer(colliders[0].transform.position);


            // If the turret is not already shooting, start shooting coroutine
            if (!isShooting)
            {
                if (turretroutine != null)
                {

                    StopCoroutine(turretroutine);
                    turretroutine = null;
                }

                turretroutine = StartCoroutine(ShootCoroutine());
            }
        }
        else
        {
            isPlayerDetected = false;

        }
    }

    private void RotateTowardsPlayer(Vector2 playerPosition)
    {
        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ShootCoroutine()
    {

        isShooting = true;
        anim.SetBool("turretactive", true);
        yield return new WaitForSeconds(firedelay);

        if (isPlayerDetected)
        {
            Shoot();
        }
        anim.SetBool("turretactive", false);

        isShooting = false;
    }

    private void Shoot()
    {
        // Instantiate bullet
        
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        
    }
    
    

    private void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

