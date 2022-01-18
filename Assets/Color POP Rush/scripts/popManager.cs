using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct allColor { 
public Color ball,ground,hurdle,sideGround;
}
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
    public GameObject popSound, gemSound,crackSound;
    public GameObject ballEffect,brokenBall;
    GameObject finish,eff;
    public float lvlSpeed=4;
    public Text lvlText;
    public allColor[] clr;
    public Material ballMat,groundMat,hurdleMat,sideGroundMat,h1, h2;
    [HideInInspector]public Color mainBallColor;

    public int colorIndex;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    void Start()
    {
        setColors();
        lvlText.text = "LEVEL :"+SceneManager.GetActiveScene().name;
        popEnvironment.instance.speed = lvlSpeed;
        finish = GameObject.Find("Finish");
        eff = finish.transform.Find("effects").gameObject;
    }
    void setColors() {
        int n = int.Parse(SceneManager.GetActiveScene().name);
        if(n>10)n = n / 10;
        colorIndex = n-1;
        mainBallColor = clr[colorIndex].ball;
        ballMat.color = clr[colorIndex].ball;
        groundMat.color = clr[colorIndex].ground;
        hurdleMat.color = clr[colorIndex].hurdle;
        sideGroundMat.color = clr[colorIndex].sideGround;

        h1.color = hurdleMat.color;
        h2.color = Color.white;

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
        eff.SetActive(true);
        popEnvironment.instance.slowStop();
        wonScreen.SetActive(true);
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void nextLevelBtnPressed() {
        int myBuildIndex = SceneManager.GetActiveScene().buildIndex;//next level btn
        myBuildIndex += 1;
        int totalScene = SceneManager.sceneCountInBuildSettings;
        print(myBuildIndex + "|" + totalScene);
        if (myBuildIndex == totalScene)
        {
            myBuildIndex = 1;//0 is menu
        }
        PlayerPrefs.SetInt("levelPointer", myBuildIndex);
        SceneManager.LoadScene(myBuildIndex);
    }
    public void restartClicked() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #region sounds
    public void playPopSound() {
        GameObject pop = Instantiate(popSound, transform);
        Destroy(pop,.5f);
    }
    public void playCrackSound()
    {
        GameObject pop = Instantiate(crackSound);
        Destroy(pop, 2f);
    }
    public void playGemSound() {
        GameObject pop = Instantiate(gemSound, transform);
        Destroy(pop, 1f);
    }
    #endregion
}
