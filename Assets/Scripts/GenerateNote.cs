using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GenerateNote : MonoBehaviour
{
    public static GenerateNote instance;

    [SerializeField]
    GameObject goSingleNote;

    [SerializeField]
    GameObject[] goGenerationLane;

    SaveData player;

    bool isFinish = false;

    void Start()
    {
        instance = this;
        LoadData();

    }

    Note CreateNewNote()
    {
        var newNote = Instantiate(goSingleNote).GetComponent<Note>();
        newNote.gameObject.SetActive(false);
        newNote.transform.SetParent(transform);
        return newNote;
    }

    static Note GetNote()
    {
        var newObj = instance.CreateNewNote();
        newObj.gameObject.SetActive(true);
        newObj.transform.SetParent(null);
        return newObj;
    }
    void Update()
    {
        if (isFinish)
        {
            isFinish = false;

            for (int i = 0; i < player.nIndex.Count; i++)
            {
                StartCoroutine(InstantiateNoteAtTime(player, i));
            }
        }
    }

    IEnumerator InstantiateNoteAtTime(SaveData savedData, int __index)
    {
        yield return new WaitForSeconds((float)savedData.dTime[__index]);

        //Transform transform = null;
        var note = GetNote();

        if (savedData.nLane[__index] == 1)
            //transform = goGenerationLane[0].transform;
            note.transform.position = goGenerationLane[0].transform.position;
        else if (savedData.nLane[__index] == 2)
            //transform = goGenerationLane[1].transform;
            note.transform.position = goGenerationLane[1].transform.position;
        else if (savedData.nLane[__index] == 3)
            //transform = goGenerationLane[2].transform;
            note.transform.position = goGenerationLane[2].transform.position;
        else if (savedData.nLane[__index] == 4)
            //transform = goGenerationLane[3].transform;
            note.transform.position = goGenerationLane[3].transform.position;

        //Instantiate(goSingleNote, transform.position, transform.rotation);
    }
    public void LoadData()
    {
        string data = LoadFile(GetPath(GameSceneData.sharedInstance.getAudioName()));

        player = JsonToData(data);

        isFinish = true;
    }

    static string GetPath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".json");
    }

    private static string LoadFile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);

            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            return jsonString;
        }
    }

    private SaveData JsonToData(string jsonData)
    {
        SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

        return data;
    }
}
