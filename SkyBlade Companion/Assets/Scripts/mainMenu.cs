using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
    
{
    public string newGameScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
