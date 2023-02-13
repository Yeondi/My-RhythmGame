using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSceneData : MonoBehaviour
{
    public static GameSceneData sharedInstance;

    SongInfo songInfo;
    public GameObject song;

    TMP_Text title;
    TMP_Text album_Title;
    TMP_Text singer;

    AudioSource AudioSource;

    [SerializeField]
    int nPerfect = 0;
    [SerializeField]
    int nGood = 0;
    [SerializeField]
    int nMiss = 0;
    [SerializeField]
    int nTotalScore = 0;

    public GameObject PopUp;
    public TMP_Text tmpPerfect;
    public TMP_Text tmpGood;
    public TMP_Text tmpMiss;
    public TMP_Text tmpScore;

    bool bStarted = false;

    [SerializeField]
    GameObject goScorePopUp;
    public TMP_Text resultPerfect;
    public TMP_Text resultGood;
    public TMP_Text resultMiss;
    public TMP_Text resultTotalScore;

    #region Test
    bool isTest = false;
    [SerializeField]
    SongInfo testInfo;
    #endregion

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
            Destroy(gameObject);
        else
            sharedInstance = this;

        if (!isTest)
            songInfo = SelectSceneData.sharedInstance.getSong();
        else
            songInfo = testInfo;

        if (!isTest)
            Destroy(SelectSceneData.sharedInstance.gameObject);

        title = song.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        album_Title = song.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        singer = song.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();

        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = songInfo.SongFile;

        setData();
    }

    private void Start()
    {
        StartCoroutine(PlayMusic());
    }

    public IEnumerator PlayMusic()
    {
        if (AudioSource.clip.name == "FlowerDance")
        {
            yield return new WaitForSeconds(3.5f);
            bStarted = true;
        }
        else if (AudioSource.clip.name == "Inuyasha")
        {
            yield return new WaitForSeconds(7.3f);
            bStarted = true;
        }
        else if (AudioSource.clip.name == "SweetDreams, My dear")
        {
            yield return new WaitForSeconds(7.3f);
            bStarted = true;
        }
        else if (AudioSource.clip.name == "Dynamite")
        {
            yield return new WaitForSeconds(7.3f);
            bStarted = true;
        }
        else if (AudioSource.clip.name == "ANTIFRAGILE")
        {
            yield return new WaitForSeconds(7.3f);
            bStarted = true;
        }
        
        AudioSource.Play();
    }

    private void Update()
    {
        //d f j k


        UpdateScore();

        if(AudioSource.isPlaying == false && bStarted)
        {
            goScorePopUp.SetActive(true);
            ResultGame();
        }
    }

    void setData()
    {
        song.GetComponent<Image>().sprite = songInfo.image.sprite;
        title.text = songInfo.title.text;
        album_Title.text = songInfo.albumTitle.text;
        singer.text = songInfo.singerName.text;
    }
     
    public void PauseGame()
    {
        AudioSource.Pause();
        Time.timeScale = 0f;
        PopUp.SetActive(true);
    }

    public void ResultGame()
    {
        resultPerfect.text = tmpPerfect.text;
        resultGood.text = tmpGood.text;
        resultMiss.text = tmpMiss.text;
        resultTotalScore.text = tmpScore.text;
    }

    public void ResumeGame()
    {
        AudioSource.UnPause();
        Time.timeScale = 1f;
        PopUp.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("SelectScene");
    }

    void UpdateScore()
    {
        tmpPerfect.text = "Perfect : " + nPerfect;
        tmpMiss.text = "Miss : " + nMiss;
        tmpGood.text = "Good : " + nGood;

        nTotalScore = (nPerfect * 10) + (nGood * 5) - (nMiss * 5);
        tmpScore.text = "Total Score : " + nTotalScore;
    }

    public string getAudioName()
    {
        if (AudioSource == null)
            return testInfo.SongFile.name;
        return AudioSource.clip.name;
    }

    public void AddPerfect()
    {
        nPerfect++;
    }

    public int GetPerfect()
    {
        return nPerfect;
    }

    public void AddGood()
    {
        nGood++;
    }

    public int GetGood()
    {
        return nGood;
    }

    public void AddMiss()
    {
        nMiss++;
    }

    public int GetMiss()
    {
        return nMiss;
    }
}
