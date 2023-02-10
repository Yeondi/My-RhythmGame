using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    bool isStaying = false;

    [SerializeField]
    Material[] materials;

    Vector3 m_DownPos;

    [SerializeField]
    GameObject[] lines;

    [SerializeField]
    Material[] materials2;

    List<Note> m_Notes;

    public string NoteLayer = "Note";

    private void Start()
    {
        m_Notes = GenerateNote.instance.GetNotes();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.D))
        {
            lines[0].GetComponent<MeshRenderer>().material = materials2[1];
            GenerateNote.instance.CheckTiming(KeyCode.D.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            lines[1].GetComponent<MeshRenderer>().material = materials2[3];
            GenerateNote.instance.CheckTiming(KeyCode.F.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            lines[2].GetComponent<MeshRenderer>().material = materials2[1];
            GenerateNote.instance.CheckTiming(KeyCode.J.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            lines[3].GetComponent<MeshRenderer>().material = materials2[3];
            GenerateNote.instance.CheckTiming(KeyCode.K.ToString());
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            lines[0].GetComponent<MeshRenderer>().material = materials2[0];
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            lines[1].GetComponent<MeshRenderer>().material = materials2[2];
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            lines[2].GetComponent<MeshRenderer>().material = materials2[0];
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            lines[3].GetComponent<MeshRenderer>().material = materials2[2];
        }
#endif

        //ÅÍÄ¡¿ë
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
#if !UNITY_EDITOR
            m_DownPos = Input.GetTouch(0).position;
            if (Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            Ray ray = Camera.main.ScreenPointToRay(m_DownPos);

            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                if (hit.transform.tag == "Check")
                    hit.transform.GetComponent<MeshRenderer>().material = materials[1];
                CheckTiming();
            }
        }

        //if (Input.GetMouseButtonUp(0))
        //{
        //    gameObject.GetComponent<MeshRenderer>().material = materials[0];
        //}
    }

    public void CheckTiming(string keyBinding = "")
    {
        for (int i = 0; i < GenerateNote.instance.GetNotes().Count; i++)
        {
            float zPos = m_Notes[i].transform.localPosition.z;

            if (zPos >= 20.29f && zPos <= 26.06f)
            //if(zPos >= 23.35f && zPos <= 24.25f)
            {
                //GameSceneData.sharedInstance.AddPerfect();
                //GenerateNote.instance.ReturnObject(m_Notes[i]);
            }
            //else if((zPos >= 20.29f && zPos <= 23.34) || (zPos >= 24.26 && zPos <= 26.06))
            else
            {
                //GameSceneData.sharedInstance.AddMiss();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(NoteLayer))
        {
            other.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Note")
        //{
        //    isStaying = false;
        //    GenerateNote.instance.ReturnObject(other.GetComponent<Note>());
        //    GameSceneData.sharedInstance.AddMiss();
        //}
    }
}
