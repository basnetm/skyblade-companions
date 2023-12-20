using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawanController : MonoBehaviour
{
    public static respawanController instance;
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

    private Vector3 respawnPoint;
    public float waitToRespawn;
    private GameObject thePlayer;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = playerhealthController.instance.gameObject;
        respawnPoint=thePlayer.transform.position;
    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint=newPosition;
    }


    public void Respawn()
    {
        StartCoroutine(RespawnCo());

    }
    IEnumerator RespawnCo()
    {
        thePlayer.SetActive(false);

        if(deathEffect != null)
        {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }

        yield return new WaitForSeconds(waitToRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        thePlayer.transform.position = respawnPoint;
        thePlayer.SetActive(true);

        playerhealthController.instance.FillHealth();

    }

}
