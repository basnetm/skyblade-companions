using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteturretscript : MonoBehaviour

{
    public float speed = 10f; // Speed of the bullet
    public float lifetime = 2f; // Time until the bullet is destroyed if it doesn't hit anything

    void Start()
    {
        // Set the velocity of the bullet
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        // Destroy the bullet after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collides with something other than the turret itself
        if (!other.CompareTag("turret"))
        {
            // If it collides with the player, deal damage, etc.
            if (other.CompareTag("Player"))
            {
                // Add your player damage logic here
                Debug.Log("Bullet hit player!");
            }

            // Destroy the bullet regardless of what it collides with
            Destroy(gameObject);
        }
    }
}
