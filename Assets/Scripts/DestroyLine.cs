using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Note"))
        {
            Destroy(other.gameObject);
            GameSceneData.sharedInstance.AddMiss();
        }
        else if(other.CompareTag("End"))
        {
            Destroy(other.transform.parent.gameObject);
            GameSceneData.sharedInstance.AddMiss();
        }
    }
}
