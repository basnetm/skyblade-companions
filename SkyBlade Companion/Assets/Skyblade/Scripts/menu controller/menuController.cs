using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class menuController : MonoBehaviour
{
    [Header("levels to load")]
    public string newgamelevel;
    private string leveltoload;
    [SerializeField] private GameObject noSaveGameDilogue = null;

    public void newgameDilogueyes()
    {
        SceneManager.LoadScene(newgamelevel);
    }

    public void LoadGameDilogueYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            leveltoload = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(newgamelevel);
        }
        else
        {
            noSaveGameDilogue.SetActive(true);
        }
        
    }
    public void ExitButton()
    {
        Application.Quit();
    }



}
