using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class SaveData
{
    public List<int> nIndex = new List<int>();
    public List<double> dTime = new List<double>();
    public List<int> nLane = new List<int>();
    public List<int> nType = new List<int>();
    public List<string> nKeyType = new List<string>(); // Down or Up
}

public class Record : MonoBehaviour
{
    [SerializeField]
    double dTime;

    Dictionary<int, string> NoteValues;
    int nIndex = 0;

    [SerializeField]
    bool OnAir = false;

    [SerializeField]
    int nType = 1;


    SaveData player = new SaveData();

    AudioSource AudioSource;

    SongInfo songInfo;
    [SerializeField]
    SongInfo testInfo;


    void Awake()
    {
        AudioSource = gameObject.GetComponent<AudioSource>();
        songInfo = testInfo;
        AudioSource.clip = songInfo.SongFile;
        AudioSource.Play();
        StartRecord();
    }

    public void ResetTime()
    {
        dTime = 0;
    }

    public void StartRecord()
    {
        // 저장방식 - 레인_시간초_타입
        // 예) 1_0.1432_1
        // D키를 0.1432에 단일을 눌렀다.
        // n초 이하 입력시 단일 / 이상 입력시 롱노트
        ResetTime();
        OnAir = true;
    }

    private static string SaveData_ToJson(SaveData __player)
    {
        string data = JsonUtility.ToJson(__player);

        return data;
    }

    private void SaveFile(string jsonData)
    {
        using (FileStream fs = new FileStream(GetPath(GetAudioName()), FileMode.Create, FileAccess.Write))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            fs.Write(bytes, 0, bytes.Length);
        }
    }

    static string GetPath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".json");
    }

    public void InputData(KeyCode __keyCode, int __nLane, int __nType, string __KeyType)
    {
        Debug.Log(__keyCode.ToString() + dTime);

        player.nIndex.Add(nIndex);
        player.nLane.Add(__nLane);
        player.dTime.Add(dTime);
        player.nType.Add(__nType);
        player.nKeyType.Add(__KeyType);

        nIndex++;
    }

    public void SaveData()
    {
        var jsonString = SaveData_ToJson(player);
        SaveFile(jsonString);
    }

    void Update()
    {
        dTime += Time.deltaTime;

        if (OnAir)
        {
            // 슬라이드노트 제작 방법 택1
            // 1. keyup시간 - keydown시간 = single인지 slide인지
            // 2. dfjk = single / erui = slide note
            // 2-1. keyup - keydown = 값  찰나를 0.1로 가정해서  값 / 0.1 -> A / slide Note 프리팹 스케일 값 * A ?
            if (Input.GetKeyDown(KeyCode.D))
                InputData(KeyCode.D, 1, 1, "Down");
            else if (Input.GetKeyDown(KeyCode.F))
                InputData(KeyCode.F, 2, 1, "Down");
            else if (Input.GetKeyDown(KeyCode.J))
                InputData(KeyCode.J, 3, 1, "Down");
            else if (Input.GetKeyDown(KeyCode.K))
                InputData(KeyCode.K, 4, 1, "Down");
            else if (Input.GetKeyDown(KeyCode.E))
                InputData(KeyCode.E, 1, 1, "Down");
            else if (Input.GetKeyDown(KeyCode.R))
                InputData(KeyCode.R, 2, 1, "Down");
            else if (Input.GetKeyDown(KeyCode.U))
                InputData(KeyCode.U, 3, 1, "Down");
            else if (Input.GetKeyDown(KeyCode.I))
                InputData(KeyCode.I, 4, 1, "Down");

            if (Input.GetKeyUp(KeyCode.E))
                InputData(KeyCode.E, 1, 2, "Up");
            else if (Input.GetKeyUp(KeyCode.R))
                InputData(KeyCode.R, 2, 2, "Up");
            else if (Input.GetKeyUp(KeyCode.U))
                InputData(KeyCode.U, 3, 2, "Up");
            else if (Input.GetKeyUp(KeyCode.I))
                InputData(KeyCode.I, 4, 2, "Up");
        }
    }

    public string GetAudioName()
    {
        return songInfo.SongFile.name;
    }
}
