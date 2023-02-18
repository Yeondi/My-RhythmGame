using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class NoteDataLoader : MonoBehaviour
{
    List<string> jsonFiles = new List<string>();

    private void Start()
    {
        string[] filePaths = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, "NoteData"), "*.json");

        foreach (string filePath in filePaths)
        {
            jsonFiles.Add(Path.GetFileName(filePath));
        }


        foreach (string jsonFile in jsonFiles)
        {
            string sourceFilePath = Path.Combine(Application.streamingAssetsPath, "NoteData", jsonFile);
            string targetFilePath = Path.Combine(Application.persistentDataPath, jsonFile);
            StartCoroutine(CopyFile(sourceFilePath, targetFilePath));
        }
    }

    IEnumerator CopyFile(string sourceFilePath, string targetFilePath)
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            using(UnityWebRequest www = UnityWebRequest.Get(sourceFilePath))
            {
                yield return www.SendWebRequest();

                if(www.result == UnityWebRequest.Result.Success)
                {
                    File.WriteAllBytes(targetFilePath, www.downloadHandler.data);
                }
                else
                {
                    Debug.LogError($"Failed to copy {sourceFilePath} to {targetFilePath}");
                }
            }
        }
        else
        {
            File.Copy(sourceFilePath, targetFilePath, true);
        }
    }

    private void Update()
    {
        Debug.Log(jsonFiles.Count);
    }
}
