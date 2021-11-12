using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;
using UnityEngine.SceneManagement;
public class popManager : MonoBehaviour
{
    public delegate void pointerUp();
    public  event pointerUp onPointerUp;

    public delegate void pointerDownd();
    public event pointerDownd onPointerDown;

    public delegate void gs();
    public event gs onGameStart;

    public delegate void ge();
    public event ge onGameEnd;

    public static bool pointerDown=false;
    public static popManager instance;

    public GameObject instruction,wonScreen,loseScreen,bubbleBurst;
    bool gameStarted=false;
    public GameObject popSound, gemSound;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void pointerDownFn() {
        if (!gameStarted)
        {
            Taptic.Medium();
            gameStarted = true;
            instruction.SetActive(false);
            onGameStart?.Invoke();
        }
        onPointerDown?.Invoke();
        pointerDown = true;
    }
    public void pointerUpFn() {
        pointerDown = false;
        onPointerUp?.Invoke();
    }

    public void gameLose() {
        onGameEnd?.Invoke();
        Taptic.Medium();
        CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 3f);
        loseScreen.SetActive(true);
    }

    public void gameWon() {
        Taptic.Medium();
        wonScreen.SetActive(false);
    }

    public void nextLevelBtnPressed() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void restartClicked() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #region sounds
    public void playPopSound() {
        GameObject pop = Instantiate(popSound, transform);
        Destroy(pop,.5f);
    }
    public void playGemSound() {
        GameObject pop = Instantiate(gemSound, transform);
        Destroy(pop, 1f);
    }
    #endregion
}
