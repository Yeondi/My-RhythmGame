using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObject : MonoBehaviour
{
    public bool isOverlapped = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Check"))
        {
            Debug.Log("StartObject");
            isOverlapped = true;
        }
    }
}
