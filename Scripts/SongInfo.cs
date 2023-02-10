using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongInfo : MonoBehaviour
{
    public Image image;
    public TMP_Text title;
    public TMP_Text albumTitle;
    public TMP_Text singerName;

    public AudioClip SongFile;
    public AudioClip PreviewFile;

    public bool isSelected;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = PreviewFile;
    }

    private void Update()
    {
        if (isSelected)
        {
            if (audioSource.isPlaying)
                return;
            else
                audioSource.Play();
        }
        else if (!isSelected)
        {
            audioSource.Stop();
        }
    }

}
