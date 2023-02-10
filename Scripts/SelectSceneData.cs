using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneData : MonoBehaviour
{
    public static SelectSceneData sharedInstance = null;

    [SerializeField]
    int nCurrentNumber;

    bool goingToNextScene = false;

    string GameScene = "GameScene";

    [SerializeField]
    SongInfo song;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
            Destroy(gameObject);
        else
            sharedInstance = this;

        DontDestroyOnLoad(gameObject);


    }

    private void Update()
    {
        if(goingToNextScene)
        {
            SceneManager.LoadScene(GameScene);
        }
    }

    public void setCurrentNumber(int nData)
    {
        nCurrentNumber = nData;
    }

    public int getCurrentNumber()
    {
        return nCurrentNumber;
    }

    public void setGoingToNextScene(bool signal)
    {
        goingToNextScene = signal;
    }

    public bool getGoingToNextScene()
    {
        return goingToNextScene;
    }

    public void setSong(SongInfo _song)
    {
        song = _song;
    }

    public SongInfo getSong()
    {
        return song;
    }
}
