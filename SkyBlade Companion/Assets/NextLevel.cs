using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    private IEnumerator LoadLevelCo()
    {
        uiController.instance.startFadeToBlack();
        yield return new WaitForSeconds(0.5f);
        uiController.instance.startFadeFromBlack();
        SceneManager.LoadScene(levelToLoad);
    }
}
