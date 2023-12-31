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
       audioManager.instance.PlayMainMusic();
    }
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
        audioManager.instance.PlayLevelMusic();
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
