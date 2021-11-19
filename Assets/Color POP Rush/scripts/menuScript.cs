using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("firstTime") == 0)
        {
            PlayerPrefs.SetInt("firstTime", 100);
            PlayerPrefs.SetInt("levelPointer", 1);
        }
        int l = PlayerPrefs.GetInt("levelPointer");
        SceneManager.LoadScene(l);
    }

  
}
