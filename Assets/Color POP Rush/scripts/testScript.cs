using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class testScript : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask layer;
    Vector3 mousePos;
    public Rigidbody box;
    bool onlyOnce=false;
    Vector3 offset;
    Vector3 masterPos;
    bool canShoot = true;
    public string ballToBurst;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mousePos = Camera.main.WorldToScreenPoint(transform.position);
        masterPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)&&canShoot)
        {
            mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2000, layer))
            {
                if (hit.collider.name == "ground")
                {
                    Vector3 mainpoint = hit.point;
                    mainpoint.y = transform.position.y;
                    if (!onlyOnce)
                    {
                        onlyOnce = true;
                        offset = transform.position - mainpoint;
                    }
                    masterPos = mainpoint + offset;
                   // rb.position = Vector3.Lerp(rb.position, mainpoint + offset, Time.deltaTime * 4);
                }
            }
        }

        rb.position = Vector3.Lerp(rb.position, masterPos, Time.deltaTime * 4);
        rb.velocity = new Vector3(rb.velocity.x,-10f,rb.velocity.z);
        box.position = rb.position;

        if (Input.GetMouseButtonUp(0))
        {
            onlyOnce = false;
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "out")
        {

            print("player out");
            *//*Time.timeScale = 0.2f;
            tValue = Time.fixedDeltaTime;
            Time.fixedDeltaTime = Time.timeScale * .02f;*//*


            popManager.instance.gameLose();
        }
        if (other.CompareTag("bubble"))
        {
            //print(other.gameObject.name);
            if (!other.gameObject.name.Contains(ballToBurst))//good
            {
                canShoot = false;
                gameObject.SetActive(false);

                Instantiate(popManager.instance.brokenBall, transform.position, transform.rotation);
                popManager.instance.gameLose();
            }

            CameraShaker.Instance.ShakeOnce(.6f, .6f, .2f, .2f);
            other.enabled = false;
            Taptic.Light();
            popManager.instance.playPopSound();
            other.transform.GetChild(0).gameObject.SetActive(false);
            GameObject eff = Instantiate(popManager.instance.bubbleBurst, other.transform.position, transform.rotation);
            // eff.GetComponentInChildren<ParticleSystem>().startColor = Color.red;
            Destroy(eff, 2f);
            GameObject eff3 = Instantiate(popManager.instance.ballEffect, other.transform.position + Vector3.up, transform.rotation, other.transform);
            // eff.GetComponentInChildren<ParticleSystem>().startColor = Color.red;
            Destroy(eff3, 2f);
        }
        if (other.CompareTag("Finish"))
        {
            canShoot = false;
            popManager.instance.gameWon();
        }
    }*/
}
