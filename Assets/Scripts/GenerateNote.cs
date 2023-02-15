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
    GameObject goSlideNote;

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
        Note newNote = null;
        newNote = Instantiate(goSingleNote).GetComponent<Note>();
        newNote.gameObject.SetActive(false);
        newNote.transform.SetParent(transform);
        return newNote;
    }

    SlideNote CreateSlideNote()
    {
        SlideNote newNote = null;
        newNote = Instantiate(goSlideNote).GetComponent<SlideNote>();
        newNote.gameObject.SetActive(false);
        newNote.transform.SetParent(transform);
        return newNote;
    }

    Note GetNote()
    {
        Note newNote = null;
        newNote = instance.CreateNewNote();
        newNote.gameObject.SetActive(false);
        newNote.transform.SetParent(null);
        return newNote;
    }

    SlideNote GetSlideNote()
    {
        SlideNote newSlideNote = null;
        newSlideNote = instance.CreateSlideNote();
        newSlideNote.gameObject.SetActive(true);
        newSlideNote.transform.SetParent(null);
        return newSlideNote;
    }

    void Update()
    {
        if (isFinish)
        {
            isFinish = false;

            for (int i = 0; i < player.nIndex.Count; i++)
            {
                if (player.nType[i] == 1 && player.nType[i + 1] == 2)
                    continue;

                StartCoroutine(InstantiateNoteAtTime(player, i));
            }
        }
    }

    IEnumerator InstantiateNoteAtTime(SaveData savedData, int __index)
    {
        yield return new WaitForSeconds((float)savedData.dTime[__index]);

        Note note = null;
        SlideNote slideNote = null;
        if (savedData.nType[__index] == 1 && savedData.nType[__index + 1] == 1)
        {
            note = GetNote();
            if (savedData.nLane[__index] == 1)
                note.transform.position = goGenerationLane[0].transform.position;
            else if (savedData.nLane[__index] == 2)
                note.transform.position = goGenerationLane[1].transform.position;
            else if (savedData.nLane[__index] == 3)
                note.transform.position = goGenerationLane[2].transform.position;
            else if (savedData.nLane[__index] == 4)
                note.transform.position = goGenerationLane[3].transform.position;
        }
        else if (savedData.nType[__index] == 2 && savedData.nType[__index - 1] == 1)
        {
            slideNote = GetSlideNote(); // 슬라이드 노트 타입
            float fSlideScale = (float)savedData.dTime[__index] - (float)savedData.dTime[__index - 1]; // 슬라이드노트 스케일 값 구하기 , 키를 누른 길이
            float fLocalScale_Z = slideNote.transform.localScale.z;
            slideNote.transform.localScale = new Vector3(2.25f, 1.3f, fLocalScale_Z * fSlideScale);

            if (savedData.nLane[__index] == 1)
                slideNote.transform.position = goGenerationLane[0].transform.position;
            else if (savedData.nLane[__index] == 2)
                slideNote.transform.position = goGenerationLane[1].transform.position;
            else if (savedData.nLane[__index] == 3)
                slideNote.transform.position = goGenerationLane[2].transform.position;
            else if (savedData.nLane[__index] == 4)
                slideNote.transform.position = goGenerationLane[3].transform.position;
        }
    }
    public void LoadData()
    {
        string data = LoadFile(GetPath(GameSceneData.sharedInstance.GetAudioName()));

        player = JsonToData(data);

        isFinish = true;
    }

    static string GetPath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".json");
        //return Path.Combine(Application.dataPath, "Record/" + name + ".json");
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
