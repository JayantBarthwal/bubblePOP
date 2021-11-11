using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public static cameraScript instance;
    public Transform targetPos;

    public float rotSpeed = 0.125f;
    public float moveSpeed = 5f;
    public bool canFollow = true;
    public Vector3 childLocalPos;
    public Transform child;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        targetPos = GameObject.Find("cameraPos").transform;
        InvokeRepeating(nameof(resetChild), .2f, .2f);
        childLocalPos = Vector3.zero;
    }
    void Update()
    {
          if (targetPos!=null)
          {
            Vector3 desiredPosition = targetPos.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetPos.rotation, rotSpeed * Time.deltaTime);
          }

      child.transform.localPosition = Vector3.Lerp(child.transform.localPosition,childLocalPos,Time.deltaTime*2);
    }


    void resetChild() {
        int num = playerScript.instance.totalHead;
        childLocalPos = new Vector3(0f,0f,-num*.2f);
    
    }
    
}

