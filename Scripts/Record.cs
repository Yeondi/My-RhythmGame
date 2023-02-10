using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SaveData
{
    public List<int> nIndex = new List<int>();
    public List<double> dTime = new List<double>();
    public List<int> nLane = new List<int>();
    public List<int> nType = new List<int>();
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

    void Start()
    {
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

    private static void SaveFile(string jsonData)
    {
        using (FileStream fs = new FileStream(GetPath(GameSceneData.sharedInstance.getAudioName()), FileMode.Create, FileAccess.Write))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            fs.Write(bytes, 0, bytes.Length);
        }
    }

    static string GetPath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".json");
    }

    public void InputData(KeyCode keyCode,int nLane)
    {
        Debug.Log(keyCode.ToString() + dTime);
        //NoteValues.Add(nIndex, nLane.ToString() + "_" + dTime.ToString() + "_" + nType);

        //player.nIndex[nIndex] = nIndex;
        //player.nLane[nIndex] = nLane;
        //player.dTime[nIndex] = dTime;
        //player.nType[nIndex] = nType;

        player.nIndex.Add(nIndex);
        player.nLane.Add(nLane);
        player.dTime.Add(dTime);
        player.nType.Add(nType);

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
            if (Input.GetKeyDown(KeyCode.D))
                InputData(KeyCode.D,1);
            else if (Input.GetKeyDown(KeyCode.F))
                InputData(KeyCode.F,2);
            else if (Input.GetKeyDown(KeyCode.J))
                InputData(KeyCode.J,3);
            else if (Input.GetKeyDown(KeyCode.K))
                InputData(KeyCode.K,4);
        }
    }
}
