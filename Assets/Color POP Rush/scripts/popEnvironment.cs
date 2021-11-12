using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popEnvironment : MonoBehaviour
{
    bool startNow = false;
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
            transform.Translate(Vector3.back*2f*Time.deltaTime);
        }
    }
    void gameStartedFn() {
        startNow = true;
    }
    void gameEndedFn()
    {
        startNow = false;
    }
}
