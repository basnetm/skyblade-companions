using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    
   public string playscene;

    private void Start()
    {
        audioManager.instance.PlayMainMusic();  
    }
    public void Play()

    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;

        }
            SceneManager.LoadScene(playscene);
        
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("game quit");
    }

   
}
