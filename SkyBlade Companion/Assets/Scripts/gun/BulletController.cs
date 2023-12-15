using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDir;
    public GameObject impactEffect;
   // public int attackDamage;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveDir * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        //if (collision.CompareTag("Enemy"))
        //{
        //    collision.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        //}
        Destroy(gameObject);

    }

    //private void AttackEnemy(int damage)
    //{
    //    attackDamage = damage;

    //}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
