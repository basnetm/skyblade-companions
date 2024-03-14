using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class knight : MonoBehaviour
{
    public float moveSpeed;
    public float detectionRange;
    public float attackRange;
    public UnityEngine.Transform player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if player is within detection range
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            // Check if player is within attack range
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                // Attack the player (you can implement attack logic here)
                Debug.Log("Attacking player!");
            }
        }
        else
        {
            // If player is not in range, stop moving
            rb.velocity = Vector2.zero;
        }
    }
}