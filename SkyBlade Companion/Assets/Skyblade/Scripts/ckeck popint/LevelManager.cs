using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;
    private PlayerController player;

    public GameObject DeathParticle;
    public GameObject RespawnParticle;
    public float respawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RespawnPlayer()
    {
        StartCoroutine("respawnPlayerCo");
    }
    public IEnumerator respawnPlayerCo()
    {
        Instantiate(DeathParticle, player.transform.position, player.transform.rotation);

        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;

        // Get the Rigidbody2D component and set its velocity to zero
        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;

        Debug.Log("Player respawn");

        yield return new WaitForSeconds(respawnDelay);
        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        Instantiate(RespawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }

}
