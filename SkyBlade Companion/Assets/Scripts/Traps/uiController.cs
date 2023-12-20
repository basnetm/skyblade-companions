using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiController : MonoBehaviour
{

    public static uiController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//this helps to load the same player until its life finished
        }
        else
        {
            Destroy(gameObject);
        }
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
