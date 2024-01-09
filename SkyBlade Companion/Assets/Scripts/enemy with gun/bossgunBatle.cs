using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossgunBatle : MonoBehaviour
{
    [Header("Moving Camera")]
    private cameraController theCam;
    public Transform camPosition;
    public float timeCounter;
    public float camSpeed;
    public Transform returnPoint;

    public int threshold1, threshold2;
    public float activeTime, fadeoutTime, inactiveTime;
    private float activeCounter, fadeCounter, inactiveCounter;
    public Transform[] spawnPoints;
    private Transform targetPoint;

    public float moveSpeed;
    public Animator anim;
    public Transform theBoss;
    public float timeBetweenShots1,timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;
    public GameObject winobject;

    private bool battleEnded;

    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType<cameraController>();
        theCam.enabled = false;
        activeCounter = activeTime;
        shotCounter = timeBetweenShots1;
        audioManager.instance.PlayBossMusic();
    }
       

    

    // Update is called once per frame
    void Update()
    {
        if (!battleEnded)
        {
            //modified code
            theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
            timeCounter -= Time.deltaTime;
            if(timeCounter <= 0)
            {
                timeCounter = 0;
                //theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, returnPoint.position, 6f * Time.deltaTime);
                theCam.transform.position = returnPoint.position;
                theCam.enabled = true;
            }
            
            if (bossgunhealthController.Instance.currentHealth > threshold1)
            {
                if (activeCounter > 0)
                {
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeoutTime;
                        anim.SetTrigger("vanish");
                    }
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots1;
                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }

                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        theBoss.gameObject.SetActive(true);
                        activeCounter = inactiveTime;

                        shotCounter = timeBetweenShots1;
                    }
                }
            }
            else
            {
                if (targetPoint == null)
                {
                    targetPoint = theBoss;
                    fadeCounter = fadeoutTime;
                    anim.SetTrigger("vanish");
                }
                else
                {
                    if (Vector3.Distance(theBoss.position, targetPoint.position) > .02f)
                    {
                        theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);

                        if (Vector3.Distance(theBoss.position, targetPoint.position) <= .02f)
                        {
                            fadeCounter = fadeoutTime;
                            anim.SetTrigger("vanish");
                        }
                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            if (playerhealthController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }

                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }



                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            theBoss.gameObject.SetActive(false);
                            inactiveCounter = inactiveTime;
                        }
                    }
                    else if (inactiveCounter > 0)
                    {
                        inactiveCounter -= Time.deltaTime;
                        if (inactiveCounter <= 0)
                        {
                            theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            int whileBreaker = 0;
                            while (targetPoint.position == theBoss.position && whileBreaker < 100)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                                whileBreaker++;
                            }



                            theBoss.gameObject.SetActive(true);


                            if (playerhealthController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }

                        }
                    }
                }
            }
        }
        else
        {
            fadeCounter -=Time.deltaTime;
            if(fadeCounter <= 0)
            {
                if(winobject != null)
                {
                    winobject.SetActive(true);
                    winobject.transform.SetParent(null);
                }
                theCam.enabled = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void EndBattle()
    {
        battleEnded = true;
        fadeCounter = fadeoutTime;
        anim.SetTrigger("vanish");
        theBoss.GetComponent<Collider2D>().enabled = false;

        bossBulletShot[] bullets = FindObjectsOfType<bossBulletShot>();
        if (bullets.Length > 0)
        {

            foreach(bossBulletShot boss in bullets)
            {
                Destroy(boss.gameObject);
            }

        }
    }
}
