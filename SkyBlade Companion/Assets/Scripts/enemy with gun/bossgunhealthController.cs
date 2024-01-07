using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossgunhealthController : MonoBehaviour
{
    public static bossgunhealthController Instance;
    private void Awake()
    {
        Instance = this; 
    }
    public Slider bossHealthSlider;
    public int currentHealth = 30;
    public bossgunBatle theboss;
    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

   public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            theboss.EndBattle();


            audioManager.instance.PlayAFX(0);
        }
        else
        {
            audioManager.instance.PlayAFX(1);
        }

        bossHealthSlider.value = currentHealth;

    }
       
}
