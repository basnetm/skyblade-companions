using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class doorController : MonoBehaviour
{

    public Animator anim;
    public float distanceToOpen;
    private PlayerController thePlayer;
    private bool playerExisting;
    public Transform exitPoint;
    public float movePlayerSpeed;
    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer=playerhealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance(transform.position,thePlayer.transform.position)<distanceToOpen)
        {
            anim.SetBool("door_open", true);
        }
        else
        {
            anim.SetBool("door_open", false);


        }
        if (playerExisting)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            if (!playerExisting)
            {
                thePlayer.canMove = false;  
                StartCoroutine(UseDoorCo());
            }
        }
    }

    IEnumerator UseDoorCo()
    {
        playerExisting = true;
        thePlayer.anim.enabled = false;
        yield return new WaitForSeconds(1.5f);
        respawanController.instance.SetSpawn(exitPoint.position);   
        thePlayer.canMove=true;
        thePlayer.anim.enabled = true;
        SceneManager.LoadScene(levelToLoad);
      } 

}
