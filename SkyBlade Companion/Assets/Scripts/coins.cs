using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    
    //public static coins instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);//this helps to load the same player until its life finished
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}


    [SerializeField]
    private int coinValue;

    public bool isCoin;

    public int coinCount=0;

    //public int cointAmount;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(isCoin)
            {
               coinCount = coinValue++;
                Destroy(gameObject);
            }

           


            //if(isCoin)
            //{
            //    coinCount = coinCount + coinValue;

            //    //coinCount = 
            //    Destroy(gameObject);
            //}

        }
    }

    //public void CoinCount()
    //{
    //   int coin = coinValue++;
    //}
}
