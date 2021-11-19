using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name!="ground"&& other.gameObject.name != "out")
        {
            Destroy(other.gameObject);
        }
    }
}
