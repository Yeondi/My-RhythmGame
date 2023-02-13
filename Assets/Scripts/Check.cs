using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    Vector3 m_DownPos;

    [SerializeField]
    GameObject[] lines;

    [SerializeField]
    Material[] materials_Line;

    List<Note> m_Notes;

    

    private void Start()
    {
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.D))
        {
            lines[0].GetComponent<MeshRenderer>().material = materials_Line[1];
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            lines[1].GetComponent<MeshRenderer>().material = materials_Line[3];
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            lines[2].GetComponent<MeshRenderer>().material = materials_Line[1];
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            lines[3].GetComponent<MeshRenderer>().material = materials_Line[3];
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            lines[0].GetComponent<MeshRenderer>().material = materials_Line[0];
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            lines[1].GetComponent<MeshRenderer>().material = materials_Line[2];
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            lines[2].GetComponent<MeshRenderer>().material = materials_Line[0];
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            lines[3].GetComponent<MeshRenderer>().material = materials_Line[2];
        }
#endif
    }

    //public void CheckTiming(string keyBinding = "")
    //{
    //    for (int i = 0; i < GenerateNote.instance.GetNotes().Count; i++)
    //    {
    //        float zPos = m_Notes[i].transform.localPosition.z;

    //        if (zPos >= 20.29f && zPos <= 26.06f)
    //        //if(zPos >= 23.35f && zPos <= 24.25f)
    //        {
    //            //GameSceneData.sharedInstance.AddPerfect();
    //            //GenerateNote.instance.ReturnObject(m_Notes[i]);
    //        }
    //        //else if((zPos >= 20.29f && zPos <= 23.34) || (zPos >= 24.26 && zPos <= 26.06))
    //        else
    //        {
    //            //GameSceneData.sharedInstance.AddMiss();
    //        }
    //    }
    //}


}
