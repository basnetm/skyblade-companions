using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firebreadther : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform detectionpoint;


    public float shootrange;
    public Transform shootpoint;
    public bool shotactive=false;
    public float delay;
    public LayerMask playerLayer;
    public bool playerinrange=false;
 
    private Animator anim;
    public GameObject projectile;
    private Coroutine turretroutine;
  

    private void Start()
    {
       anim=GetComponent<Animator>(); 


    }

    private void Update()
    {
        playdetection();
        Attack();

        
    }

    void playdetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(detectionpoint.position, detectionRange, playerLayer);
        if(colliders.Length > 0 )
        {
          
           playerinrange = true;
            //if(playerinrange )
            //{
                anim.SetBool("active", true);
           

            //}

            
        }
        else
        {
            playerinrange=false;
            anim.SetBool("active", false);
        }
    }
   
   

    private void Attack()
    {
      
       
        
          Collider2D[] colliders = Physics2D.OverlapCircleAll(shootpoint.position, shootrange, playerLayer);



        if (colliders.Length>0)
        {
            if (turretroutine != null)
            {

                StopCoroutine(turretroutine);
                turretroutine = null;
            }


            turretroutine = StartCoroutine(turretRoutine()); 
        }
        
    }

    private void shoot()
    {
        if (playerinrange && shotactive)
        {
            Instantiate(projectile, attackPoint.position, Quaternion.identity);
        }
        
    }
    private IEnumerator turretRoutine()
    {

        shotactive = true;

        anim.SetBool("shoot", true);
        shoot();
        yield return new WaitForSeconds(delay);

        shotactive = false;
        anim.SetBool("shoot", false);

       
    }
        private void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionpoint.position, detectionRange);
        Gizmos.DrawWireSphere(shootpoint.position, shootrange);
    }


}
