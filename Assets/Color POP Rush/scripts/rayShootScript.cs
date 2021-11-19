using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class rayShootScript : MonoBehaviour
{
    Vector3 offset;
    bool calculateOffset=true;
    Rigidbody rb;
    public string ballToBurst;
    bool canShoot=true;
    float mag;
    public Transform ball;
    Vector3 oldPos, newPos;
    void Start()
    {
       
        
           /* Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.2f;*/
        
        
        rb =GetComponent<Rigidbody>();
        popManager.instance.onPointerUp += pointerUpFn;
        
    }

    // Update is called once per frame
    void Update()
    {
        //ball.transform.Rotate(rb.velocity.x*500,0,rb.velocity.z*500);
        rb.velocity = new Vector3(rb.velocity.x,-20,rb.velocity.z);
        if (popManager.pointerDown && canShoot)
        {
            mag = rb.velocity.magnitude;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ground"))
                {
                    if (calculateOffset) offset = transform.position - hit.point;

                    calculateOffset = false;
                    Vector3 newPos = hit.point + offset;
                    newPos.y = transform.position.y;
                    newPos.x = Mathf.Clamp(newPos.x, -5f, 5f);
                    newPos.z = Mathf.Clamp(newPos.z, -8f, 18f);
                   // newPos.z = transform.position.z;
                    transform.position = Vector3.Lerp(transform.position, newPos, 16 * Time.deltaTime);
                }
            }
            

        }
      /*  if (Input.GetMouseButtonUp(0))
        {
            print(rb.velocity * mag);
            rb.AddForce(rb.velocity * mag);
        }*/
        
       
    }
   
    bool onlyOnce=true;
    Vector3 masterPoint = Vector3.zero;
    /*private void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, -20, rb.velocity.z);
        if (popManager.pointerDown && canShoot) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ground"))
                {
                    
                    if (calculateOffset) offset = transform.position - hit.point;

                    calculateOffset = false;
                    Vector3 np = hit.point + offset;
                    masterPoint = np;
                    if (onlyOnce)
                    {
                        onlyOnce = false;
                         oldPos = np;
                        InvokeRepeating("checkOldPos",.5f,.5f);
                    }

                    newPos = np;
                    np.y = transform.position.y;
                    np.x = Mathf.Clamp(newPos.x, -5f, 5f);
                    np.z = Mathf.Clamp(newPos.z, -8f, 18f);
                    transform.position = Vector3.Lerp(transform.position, np, 16 * Time.deltaTime);
                }
            }
        }
        
    }*/

    void checkOldPos() { oldPos = masterPoint; }
    public static float tValue;
    void pointerUpFn() {
        CancelInvoke("checkOldPos");
        rb.velocity = Vector3.zero;
        calculateOffset = true;
        onlyOnce = true;
        Vector3 dir = newPos - oldPos;
        dir.y = 0;
        print(dir);
        // rb.AddForce(dir*1000);
        rb.velocity = dir * 10;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hurdle"))
        {
            gameLost();
        }
        if (other.gameObject.name=="out")
        {
            
            print("player out");
            /*Time.timeScale = 0.2f;
            tValue = Time.fixedDeltaTime;
            Time.fixedDeltaTime = Time.timeScale * .02f;*/

            
            popManager.instance.gameLose();
        }
        if (other.CompareTag("bubble"))
        {
            //print(other.gameObject.name);
            if (!other.gameObject.name.Contains(ballToBurst))//good
            {
                gameLost();
            }
           
            CameraShaker.Instance.ShakeOnce(.6f, .6f, .2f, .2f);
            other.enabled = false;
            Taptic.Light();
            popManager.instance.playPopSound();
            other.transform.GetChild(0).gameObject.SetActive(false);
            GameObject eff = Instantiate(popManager.instance.bubbleBurst, other.transform.position,transform.rotation);
            // eff.GetComponentInChildren<ParticleSystem>().startColor = Color.red;
            Destroy(eff,2f);
            GameObject eff3 = Instantiate(popManager.instance.ballEffect, other.transform.position+Vector3.up, transform.rotation, other.transform);
            // eff.GetComponentInChildren<ParticleSystem>().startColor = Color.red;
            Destroy(eff3, 2f);
        }
        if (other.CompareTag("Finish"))
        {
            canShoot = false;
            popManager.instance.gameWon();
        }
    }

    void gameLost() {
        canShoot = false;
        gameObject.SetActive(false);

        
       GameObject bb= Instantiate(popManager.instance.brokenBall, transform.position, transform.rotation);
        for (int i = 0; i < bb.transform.childCount; i++)
        {
            bb.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = popManager.instance.mainBallColor;
        }
        popManager.instance.gameLose();
    }
}
