using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public Transform target;
    public float range;
    bool detected = false;
    Vector2 direction;
    public float attackCooldown = 1f; // Cooldown between attacks
    float attackTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Calculate direction to the target
        direction = target.position - transform.position;

        // Check if the target (player) is within range
        if (direction.magnitude <= range)
        {
            // If the target is within range, check if it's the player
            RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range);
            if (rayInfo.collider != null && rayInfo.collider.gameObject.tag == "Player")
            {
                // Player detected, set detected to true
                detected = true;

                // Attack the player if cooldown is over
                if (attackTimer <= 0f)
                {
                    Attack();
                    attackTimer = attackCooldown; // Reset the cooldown timer
                }
                else
                {
                    attackTimer -= Time.deltaTime; // Decrease cooldown timer
                }
            }
            else
            {
                detected = false;
            }
        }
        else
        {
            detected = false;
        }
    }

    // Method to perform an attack
    void Attack()
    {
        // This is where you would put your attack logic, like shooting a bullet or applying damage to the player
        Debug.Log("Attacking Player!");
    }
}
