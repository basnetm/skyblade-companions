using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamescenemanager : MonoBehaviour
{

    public string level1;
    public string back;
   

    public void loadlevel( )
    {
        SceneManager.LoadScene(level1);
    }
    public void Back()
    {
        SceneManager.LoadScene(back);
    }

    
}
