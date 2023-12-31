using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealthController : MonoBehaviour
{
    public static playerhealthController instance;

    private void Awake()
    {
        if (instance == null) { 
        instance = this;
        DontDestroyOnLoad(gameObject);//this helps to load the same player until its life finished
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //[HideInInspector]//helps to hide from inspector 
    public int currentHealth;
    public int maxHealth;
    public float invincibilityLength;
    private float invincCounter;
    public float flashLength;
    private float flashCounter;
    public SpriteRenderer[] playerSprites; 



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        uiController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       if(invincCounter>0) { 
        
        invincCounter -= Time.deltaTime;
        flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                foreach(SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }
            if (invincCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        } 
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincCounter <= 0)
        {



            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);

                respawanController.instance.Respawn();
                audioManager.instance.PlayAFX(9);

            }
            else
            {
                invincCounter = invincibilityLength;
                audioManager.instance.PlaySFXAdjusted(12);
            }
            uiController.instance.UpdateHealth(currentHealth, maxHealth);
        }
        }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        uiController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        uiController.instance.UpdateHealth(currentHealth,maxHealth);
    }


}
