using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDetectionTest2 : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(1f, 1f, 1f);
    public LayerMask noteLayer;
    public float detectionDistance = 1f;

    void Update()
    {
        RaycastHit hit;
        bool isHit = Physics.BoxCast(transform.position, boxSize, transform.forward, out hit, transform.rotation, detectionDistance, noteLayer);

        if(isHit)
        {
            hit.collider.gameObject.SetActive(false);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + transform.forward * detectionDistance / 2f, boxSize);
    }
}
