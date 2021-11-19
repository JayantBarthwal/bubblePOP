using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popEnvironment : MonoBehaviour
{
    public static popEnvironment instance;
    bool startNow = false;

    [HideInInspector]public float speed;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    void Start()
    {
        popManager.instance.onGameStart += gameStartedFn;
        popManager.instance.onGameEnd += gameEndedFn;
    }

    // Update is called once per frame
    void Update()
    {
        if (startNow)
        {
            transform.Translate(Vector3.back*speed*Time.deltaTime);
        }
        if (slow)
        {
            speed = Mathf.Lerp(speed,0,Time.deltaTime*.5f);
        }
    }
    void gameStartedFn() {
        startNow = true;
    }
    void gameEndedFn()
    {
        startNow = false;
    }
    bool slow = false;
    public void slowStop() {
        slow = true;
    }
}
