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
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        popManager.instance.onPointerUp += pointerUpFn;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x,-20,rb.velocity.z);
        if (popManager.pointerDown)
        {
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
                    newPos.x = Mathf.Clamp(newPos.x,-5f,5f); 
                    newPos.z = Mathf.Clamp(newPos.z, -8f, 18f);
                    transform.position = Vector3.Lerp(transform.position,newPos,16*Time.deltaTime);
                }

            }
        }
       
    }
    void pointerUpFn() { calculateOffset = true; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="out")
        {
            print("player out");
            popManager.instance.gameLose();
        }
        if (other.CompareTag("bubble"))
        {
            print(other.gameObject.name);
            if (!other.gameObject.name.Contains(ballToBurst))//good
            {
                popManager.instance.gameLose();
            }
           
            CameraShaker.Instance.ShakeOnce(.6f, .6f, .2f, .2f);
            other.enabled = false;
            Taptic.Light();
            popManager.instance.playPopSound();
            other.transform.GetChild(0).gameObject.SetActive(false);
            GameObject eff = Instantiate(popManager.instance.bubbleBurst, other.transform.position,transform.rotation);
           // eff.GetComponentInChildren<ParticleSystem>().startColor = Color.red;
            Destroy(eff,2f);
        }
    }
   
}
