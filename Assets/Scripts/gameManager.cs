using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public delegate void gameStarted();//any argument
    public event gameStarted onGameStarted;

    bool isGameOn=false;
    public GameObject leftRight,levelClearFn,levelFailFn;

    public GameObject smokeEff,explosion;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
  
    public void pointerDown() {
        if (!isGameOn)
        {
            isGameOn = true;
            leftRight.SetActive(false);
            onGameStarted?.Invoke();
        }
    }
    public void pointerUp()
    {

    }

    public void levlFailFn() {
        levelFailFn.SetActive(true);
    }
    public void levlClearFn() {
        levelClearFn.SetActive(true);
    }
    public void nextBtnFn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void retryBtnFn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
