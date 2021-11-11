using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomFace : MonoBehaviour
{
    public GameObject[] faces;
    
    void Start()
    {
       /* for (int i = 0; i < faces.Length; i++)
        {
            faces[i].SetActive(false);
        }
        faces[Random.Range(0,faces.Length)].SetActive(true);*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hurdle"))
        {
            Destroy(Instantiate(gameManager.instance.explosion, transform.position, Quaternion.identity),2f);
            playerScript.instance.totalHead -= 1;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.SetParent(null);
        }
    }
}
