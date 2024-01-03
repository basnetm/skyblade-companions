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
    public int damageAmount = 1;

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
        if (collision.tag == "enemy")
        {
            collision.GetComponent<enemyhealthController>().DamageEnemy(damageAmount);
        }
        if(collision.tag =="Boss")
        {
             bossgunhealthController.Instance.TakeDamage(damageAmount);
            //collision.GetComponent<bossgunhealthController>().TakeDamage(damageAmount);
        }
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        audioManager.instance.PlaySFXAdjusted(4);
      
        Destroy(gameObject);

    }

   
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
