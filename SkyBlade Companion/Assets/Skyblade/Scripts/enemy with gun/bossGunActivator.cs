using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossGunActivator : MonoBehaviour
{
    public GameObject bossToActivate;
    public GameObject pet;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bossToActivate.SetActive(true);
            gameObject.SetActive(false);    
            pet.SetActive(false);
        }
    }
}
