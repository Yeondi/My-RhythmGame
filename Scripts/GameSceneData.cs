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
    int Perfect = 0;
    [SerializeField]
    int Good = 0;
    [SerializeField]
    int Miss = 0;

    public GameObject PopUp;
    public TMP_Text tmpScore;
    public TMP_Text tmpMiss;

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
            yield return new WaitForSeconds(3.5f);
        else if(AudioSource.clip.name == "Inuyasha")
            yield return new WaitForSeconds(7.3f);
        else if(AudioSource.clip.name == "SweetDreams, My dear")
            yield return new WaitForSeconds(7.3f);
        else if(AudioSource.clip.name == "Dynamite")
            yield return new WaitForSeconds(7.3f);
        else if(AudioSource.clip.name == "ANTIFRAGILE")
            yield return new WaitForSeconds(7.3f);
            
        AudioSource.Play();
    }

    private void Update()
    {
        //d f j k


        UpdateScore();
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
        tmpScore.text = "Perfect : " + Perfect;
        tmpMiss.text = "Miss : " + Miss;
    }

    public string getAudioName()
    {
        if (AudioSource == null)
            return testInfo.SongFile.name;
        return AudioSource.clip.name;
    }

    public void AddPerfect()
    {
        Perfect++;
    }

    public int GetPerfect()
    {
        return Perfect;
    }

    public void AddGood()
    {
        Good++;
    }

    public int GetGood()
    {
        return Good;
    }

    public void AddMiss()
    {
        Miss++;
    }

    public int GetMiss()
    {
        return Miss;
    }
}
