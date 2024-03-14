using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretcontroller : MonoBehaviour
{
    public Transform target; // Player's transform
    public float range = 5f; // Range within which the turret detects the player
    public float rotationSpeed = 5f; // Speed at which the turret rotates towards the player
    public GameObject bulletPrefab; // Prefab of the bullet the turret fires
    public Transform firePoint; // Point from which the turret fires
    public float fireRate = 1f; // Rate of fire (bullets per second)

    private float fireTimer = 0f; // Timer to control the firing rate

    void Update()
    {
        if (target == null)
            return;

        // Check if the player is within range
        if (Vector2.Distance(transform.position, target.position) <= range)
        {
            // Rotate towards the player
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // Fire at the player if the fire rate allows
            if (fireTimer <= 0f)
            {
                Fire();
                fireTimer = 1f / fireRate;
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }
        }
    }

    void Fire()
    {
        // Instantiate a bullet at the fire point
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        // Add your bullet logic here
    }
}