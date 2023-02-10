using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDetectionTest : MonoBehaviour
{
    public float detectionRadius = 1f;
    public LayerMask noteLayer;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, noteLayer);

        if(hitColliders.Length > 0)
        {
            for(int i=0;i<hitColliders.Length;i++)
            {
                hitColliders[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
