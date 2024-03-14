using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonAttack : MonoBehaviour
{
    public Transform firstpoint;
    public GameObject bullet;
    float timebetween;
    public float starttimebetween;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(timebetween<=0)
        {
            Instantiate(bullet,firstpoint.position,firstpoint.rotation);
            timebetween = starttimebetween;
        }
        else
        {
            timebetween-=Time.deltaTime;
        }
    }
}
