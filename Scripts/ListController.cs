using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListController : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;

    int nCurrentSongNumber = 0;

    public GameObject Content;

    public GameObject[] Songs;

    float fixedDistance = 500f;
    float timeDuration = 3f;

    public Scrollbar scroll;

    int m_nDirection = 0;

    int nos;

    //넘길떄마다 posX 500씩 깎기

    private void Awake()
    {
        leftButton.onClick.AddListener(delegate { ChangeSong(1); });
        rightButton.onClick.AddListener(delegate { ChangeSong(2); });
        Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(1050, 0);

        Songs[nCurrentSongNumber].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        nos = Songs.Length;

        Songs[0].GetComponent<SongInfo>().isSelected = true;
    }

    private void Update()
    {
        Debug.Log("현재 번호 : " + nCurrentSongNumber);

        if (m_nDirection == 1) // 왼쪽 눌렀을때
        {
            if (nCurrentSongNumber == 0) // 0번이라 맨 끝으로 넘어가야 할때
            {
                Content.transform.position = new Vector2(Mathf.Lerp(Content.transform.position.x, Content.transform.position.x - (fixedDistance * (nos-1)), timeDuration), Content.transform.position.y);
                Songs[nCurrentSongNumber].transform.localScale = Vector3.one;
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = false;
                nCurrentSongNumber = nos - 1;
                Songs[nCurrentSongNumber].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = true;
            }
            else
            {
                Content.transform.position = new Vector2(Mathf.Lerp(Content.transform.position.x, Content.transform.position.x + fixedDistance, timeDuration), Content.transform.position.y);
                Songs[nCurrentSongNumber].transform.localScale = Vector3.one;
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = false;
                nCurrentSongNumber--;
                Songs[nCurrentSongNumber].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = true;
            }
            m_nDirection = 0;
            SelectSceneData.sharedInstance.setCurrentNumber(nCurrentSongNumber);
        }
        else if (m_nDirection == 2)
        {
            if (nCurrentSongNumber == nos - 1)
            {
                Content.transform.position = new Vector2(Mathf.Lerp(Content.transform.position.x, Content.transform.position.x + (fixedDistance * (nos - 1)), timeDuration), Content.transform.position.y);
                Songs[nCurrentSongNumber].transform.localScale = Vector3.one;
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = false;
                nCurrentSongNumber = 0;
                Songs[nCurrentSongNumber].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = true;
            }
            else
            {
                Content.transform.position = new Vector2(Mathf.Lerp(Content.transform.position.x, Content.transform.position.x - fixedDistance, timeDuration), Content.transform.position.y);
                Songs[nCurrentSongNumber].transform.localScale = Vector3.one;
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = false;
                nCurrentSongNumber++;
                Songs[nCurrentSongNumber].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Songs[nCurrentSongNumber].GetComponent<SongInfo>().isSelected = true;
            }
            m_nDirection = 0;
            SelectSceneData.sharedInstance.setCurrentNumber(nCurrentSongNumber);
        }
    }

    private void ChangeSong(int nDirection)
    {
        if (nDirection == 1)
        {
            m_nDirection = nDirection;
            Debug.Log("왼쪽 눌림");

        }
        else if (nDirection == 2)
        {
            m_nDirection = nDirection;
            Debug.Log("오른쪽 눌림");

        }
    }

    public void TossData()
    {
        Debug.Log(Songs[nCurrentSongNumber].name + " 클릭됨");
        SelectSceneData.sharedInstance.setSong(Songs[nCurrentSongNumber].GetComponent<SongInfo>());
        SelectSceneData.sharedInstance.setGoingToNextScene(true);
    }
}
