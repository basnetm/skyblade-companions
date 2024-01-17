using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetProjectileController : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public int damage = 10;

    private Rigidbody2D rb;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindClosestEnemy();

        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
        else
        {
            rb.velocity = transform.right * projectileSpeed;
        }
    }

    void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    Transform FindClosestEnemy()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Find the closest enemy
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }
}
