using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float fVelocity = 100f;

    public float perfectTiming = 0.05f;
    public float goodTiming = 0.1f;

    bool isStaying = false;

    void Start()
    {
        
    }

    void Update()
    {
        transform.localPosition += Vector3.forward * fVelocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Check")
        {
            isStaying = true;

            float timing = transform.position.z + 2.25f;

            if(timing <= perfectTiming)
            {
                GameSceneData.sharedInstance.AddPerfect();
            }
            else if(timing <= goodTiming)
            {
                GameSceneData.sharedInstance.AddGood();
            }
            else
            {
                GameSceneData.sharedInstance.AddMiss();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Check")
        {
            isStaying = true;

            float timing = transform.position.z + 2.25f;

            if (timing <= perfectTiming)
            {
                GameSceneData.sharedInstance.AddPerfect();
            }
            else if (timing <= goodTiming)
            {
                GameSceneData.sharedInstance.AddGood();
            }
            else
            {
                GameSceneData.sharedInstance.AddMiss();
            }
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
