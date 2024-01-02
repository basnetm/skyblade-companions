using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioLoader : MonoBehaviour
{
    public audioManager theAM;
    private void Awake()
    {
        if (audioManager.instance == null)
        {
            audioManager newAM=Instantiate(theAM);
            audioManager.instance = newAM;
            DontDestroyOnLoad(newAM.gameObject);

        }   
    }
}
