using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiController : MonoBehaviour
{

    public static uiController instance;
    private void Awake()
    {
        instance = this;    
    }
    public Slider healthslider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int currentHealth,int maxHealth)
    {
        healthslider.maxValue = maxHealth;
        healthslider.value = currentHealth;
    }

}
