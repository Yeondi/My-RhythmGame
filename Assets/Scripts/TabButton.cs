using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : Check
{
    [SerializeField]
    private string keyId = "D";

    Vector3 m_DownPos;

    [SerializeField]
    Material[] materials_YG;

    public string NoteLayer = "Note";

    bool isStaying = false;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
        }
#endif

        //ÅÍÄ¡¿ë
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
//#if !UNITY_EDITOR
//            m_DownPos = Input.GetTouch(0).position;
//            if (Input.GetTouch(0).phase != TouchPhase.Began)
//                return;
//#endif

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.collider.tag == "Note")
                {
                    Debug.Log("Hit note");
                    GameSceneData.sharedInstance.AddPerfect();
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer(NoteLayer))
    //    {
    //        isStaying = true;
            
    //    }

    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.layer == LayerMask.NameToLayer(NoteLayer))
    //    {
    //        isStaying = false;
    //    }
    //    //if (other.tag == "Note")
    //    //{
    //    //    isStaying = false;
    //    //    GenerateNote.instance.ReturnObject(other.GetComponent<Note>());
    //    //    GameSceneData.sharedInstance.AddMiss();
    //    //}
    //}

    public string GetKeyId()
    {
        return keyId;
    }


}
