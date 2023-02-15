using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    [SerializeField]
    private string keyId = "D";

    [SerializeField]
    Material[] materials_YG;

    public string NoteLayer = "Note";

    //void Update()
    //{
    //    if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit hit;
    //        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
    //        {
    //            if(hit.collider.CompareTag("Note"))
    //            {
    //                Debug.Log("Hit note");
    //                GameSceneData.sharedInstance.AddPerfect();
    //                hit.transform.gameObject.SetActive(false);
    //            }
    //            else if(hit.collider.CompareTag("End"))
    //            {
    //                hit.transform.parent.gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //}

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
            {
                if(hit.collider.CompareTag("Note"))
                {
                    Debug.Log("Hit Note");
                    GameSceneData.sharedInstance.AddPerfect();
                    hit.transform.gameObject.SetActive(false);
                }
                else if(hit.collider.CompareTag("SlideNote") && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    hit.collider.GetComponent<SlideNote>().IsStartOverlappingJudgmentLine();
                }
                else if(hit.collider.CompareTag("End") && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    hit.transform.parent.GetComponent<SlideNote>().Success();
                }
            }
        }
    }

    public string GetKeyId()
    {
        return keyId;
    }


}
