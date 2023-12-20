using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealthPickUp : MonoBehaviour
{
    public int healthAmount;
    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerhealthController.instance.HealPlayer(healthAmount);
            if(pickupEffect != null)
            {
                Instantiate(pickupEffect,transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
