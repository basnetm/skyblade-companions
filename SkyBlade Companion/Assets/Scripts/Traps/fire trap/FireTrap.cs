using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
   // [SerializeField] private int damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    //[SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;
   // playerhealthController player;

    private bool triggered; //when the trap gets triggered
    //private bool active; //when the trap is active and can hurt the player


    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        //active = false;
        triggered = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
                //if (active)
                //{
                //    player = collision.GetComponent<playerhealthController>();
                //    if (player == null)
                //    {
                //        Debug.Log("player not found");


                //    }
                //    player.DamagePlayer(damage);

                //}
            }
                


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopCoroutine(ActivateFiretrap());
            //active = false;
            triggered = false;
            anim.SetBool("activated", false);
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn the sprite back to its initial color
        //active = true;
        anim.SetBool("activated", true);

        //Wait until X seconds, deactivate trap and reset all variables and animator
        //yield return new WaitForSeconds(activeTime);
        //active = false;
        //triggered = false;
        //anim.SetBool("activated", false);
    }
}