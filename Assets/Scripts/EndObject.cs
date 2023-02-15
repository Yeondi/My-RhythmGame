using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    public bool isSuccess = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Check"))
        {
            Debug.Log("StartObject");
            isSuccess = true;
        }
    }
}
