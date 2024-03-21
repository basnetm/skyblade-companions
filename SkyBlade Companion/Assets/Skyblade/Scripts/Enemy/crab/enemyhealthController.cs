using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhealthController : MonoBehaviour
{
    public int totalHealth = 3;
    public GameObject deathEffect;

//
  //  private Animator anim;


    private void Start()
    {
       // anim = GetComponent<Animator>();

    }
    //
    public void DamageEnemy(int damageAmount)
    {
    totalHealth -= damageAmount;
        if(totalHealth <= 0)
        {   
            if(deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
                //anim.SetBool("Isdead", true);//addesd
            }

            Destroy(gameObject);
            audioManager.instance.PlaySFXAdjusted(5);
        }
    }
}
