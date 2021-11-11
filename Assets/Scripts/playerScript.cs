using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public static playerScript instance;
    private CharacterController cc;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public Animator anim;
    float h,p, width;
    Vector3  lastInputPos;
    public GameObject buildings;
    bool isGameOn = false;
    public Transform leftHead, rightHead;
    public int totalHead;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        width = Screen.width;

        gameManager.instance.onGameStarted += gameStarted;
    }

    void Update()
    {
        if (!isGameOn) return;

        groundedPlayer = cc.isGrounded;
        if (groundedPlayer)
        {
            if(playerVelocity.y < 0) playerVelocity.y = -1f;
            anim.SetBool("jump", false);
            anim.SetBool("run", true);
        }
        else {
            anim.SetBool("jump", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            h = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            lastInputPos = Input.mousePosition;
        }


        h = Mathf.Lerp(h,lastInputPos.x,Time.deltaTime*5);
        p = (h - lastInputPos.x) / width;


        p *= -65;



        Vector3 newPos= new Vector3(p,0, playerSpeed); 
        cc.Move(newPos * Time.deltaTime);

       

        playerVelocity.y += gravityValue * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
        buildings.transform.position = new Vector3(0f,0f,transform.position.z);


    }
    void gameStarted() {
        isGameOn = true;
    }

    public void jump() {
        if (groundedPlayer)
        {
            anim.SetBool("jump", true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickupHead"))
        {
            totalHead += 1;
            if (leftHead.childCount > rightHead.childCount)
            {
                addheadToLeftSide(false, other) ;
            }
            else if (leftHead.childCount < rightHead.childCount)
            {
                addheadToLeftSide(true, other);

            }
            else {
                addheadToLeftSide(false, other);

            }
        }
        if (other.CompareTag("Finish"))
        {
            Invoke("stopPlayer", .2f);
        }
    }
    void stopPlayer() {
        gameManager.instance.levlClearFn();
        cameraScript.instance.targetPos = null;
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        anim.SetBool("run",false);
        isGameOn = false;
        
    }
    void addheadToLeftSide(bool x,Collider other) {
        other.isTrigger = false;
        if (x)
        {
            //add in left side
            other.transform.SetParent(leftHead);
            Vector3 pos = leftHead.localPosition + Vector3.left * leftHead.childCount * .4f;
            
            other.transform.localPosition = pos;
            Instantiate(gameManager.instance.smokeEff, other.transform.position, Quaternion.identity);
        }
        else {
            other.transform.SetParent(rightHead);
            Vector3 pos = rightHead.localPosition + Vector3.right * rightHead.childCount * .4f;

            other.transform.localPosition = pos;
            Instantiate(gameManager.instance.smokeEff, other.transform.position, Quaternion.identity);

        }
    }
    
}
