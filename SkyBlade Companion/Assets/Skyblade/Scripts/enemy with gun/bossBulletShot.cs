using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBulletShot : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public int damageAmount;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = transform.position - playerhealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        
        {
            playerhealthController.instance.DamagePlayer(damageAmount);
        }
        if(impactEffect!=null)
        {
            Instantiate(impactEffect, transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
