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

    int nPoolSize = 30;

    Queue<Note> poolingObjectQueue = new Queue<Note>();

    List<Note> notes = new List<Note>();

    void Start()
    {
        instance = this;
        Initialize();
        LoadData();

        //GameSceneData.sharedInstance.PlayMusic();

        notes = poolingObjectQueue.ToList();
    }

    void Initialize()
    {
        for(int i=0;i<nPoolSize;i++)
        {
            poolingObjectQueue.Enqueue(CreateNewNote());
        }
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
        if(instance.poolingObjectQueue.Count > 0)
        {
            var obj = instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            var newObj = instance.CreateNewNote();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public void ReturnObject(Note obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueue.Enqueue(obj);
    }

    void Update()
    {
        if(isFinish)
        {
            isFinish = false;

            for(int i=0;i<player.nIndex.Count;i++)
            {
                StartCoroutine(InstantiateNoteAtTime(player,i));
            }
        }
    }

    IEnumerator InstantiateNoteAtTime(SaveData savedData,int __index)
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

    public void CheckTiming(string keyBinding = "")
    {
        for(int i=0;i< poolingObjectQueue.Count;i++)
        {
            float zPos = notes[i].transform.localPosition.z;

            if (zPos >= 23.35f && zPos <= 24.25f)
            {
                GameSceneData.sharedInstance.AddPerfect();
                ReturnObject(notes[i]);
            }
            //else if((zPos >= 20.29f && zPos <= 23.34) || (zPos >= 24.26 && zPos <= 26.06))
            else
            {
                //GameSceneData.sharedInstance.AddMiss();
            }
        }
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

    public List<Note> GetNotes()
    {
        return notes;
    }
}
