using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretprojectile : MonoBehaviour
{
  
    private float flightTime = 0f;
    public float speed;
    public float projectilelife;
    private Rigidbody2D rb;
    

    private GameObject target; // Reference to the player's transform

    void Start()
    {
        // Find the player's transform at the start of the projectile's instantiation
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player;
        }
    }

    
    private void Update()
    {
        if (target == null )
        {
            Destroy(gameObject);

        }


        Vector2 direction = (target.transform.position - transform.position).normalized;
        float distanceThisFrame=speed*Time.deltaTime;
        transform.Translate(direction * distanceThisFrame, Space.World);
        //if (direction.magnitude <= distanceThisFrame)
        //{
            
        //}

        flightTime += Time.deltaTime;

        // If flight time exceeds the maximum flight time, destroy the projectile
        if (flightTime >= projectilelife)
        {
            Destroy(gameObject);
        }
    }
   




}
