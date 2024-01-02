using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossGunActivator : MonoBehaviour
{
    public GameObject bossToActivate;

    internal void EndBattle()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bossToActivate.SetActive(true);
            gameObject.SetActive(false);    

        }
    }
}
