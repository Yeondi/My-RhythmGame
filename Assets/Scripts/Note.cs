using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float fVelocity = 100f;

    public float perfectTiming = 0.05f;
    public float goodTiming = 0.1f;

    public bool isStaying = false;

    Vector3 m_DownPos;

    void Start()
    {
        
    }

    void Update()
    {
        transform.localPosition += Vector3.forward * fVelocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Check"))
        {
            isStaying = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Check")
        {
            isStaying = false;
        }
    }
}
