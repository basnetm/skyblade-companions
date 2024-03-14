using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pausemenumanager : MonoBehaviour
{
    public string quit;
    public string resume;
    public GameObject conformmessage;
    public GameObject pausemenu;
    public bool isgamepaused=false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isgamepaused)
            {
                Resumegame();
            }
            else
            {
                pausegame();
            }
            
           
        }
        
        

       
    }
    public void pausegame()
    {
        isgamepaused = true;
        enablepausemenu();
       
        Time.timeScale = 0f;
       
        
    }
    public void Resumegame()
    {
        isgamepaused = false;
        disablepausemenu();
        Time.timeScale = 1f;
        
        
    }

    public void Quitgame()
    {
        disablepausemenu();
        
        conformmessage.SetActive(true);
    }


    public void backtogame()
    {
        conformmessage.SetActive(false);
        Resumegame();
         



    }


    public void disablepausemenu()
    {
        //if (!isgamepaused)
        //{
        //    pausemenu.SetActive(false); 
        //}
        pausemenu.SetActive(false);
    }


    public void enablepausemenu()
    {
        //if (isgamepaused)
        //{
        //    pausemenu.SetActive(true);
        //}
        pausemenu.SetActive(true);
    }



}
