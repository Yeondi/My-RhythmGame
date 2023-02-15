using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideNote : MonoBehaviour
{
    float fVelocity = 25f;

    [SerializeField]
    private bool isPressed = false;
    [SerializeField]
    private bool isSuccess = false;

    public StartObject StartObject;
    public EndObject EndObject;

    void Update()
    {
        transform.localPosition += Vector3.forward * fVelocity * Time.deltaTime;

        // Start가 판정라인에 겹치기 시작할 때
        //if (IsStartOverlappingJudgmentLine())
        //{
        //    isPressed = true;
        //}

        // End가 판정라인에 겹치기 시작할 때
        //if (IsEndOverlappingJudgmentLine())
        //{
        //    Debug.Log("점수 추가");
        //}

        // 결과 판정
        //if (isSuccess)
        //{
        //    // 성공 처리
        //    Debug.Log("Slide Success");
        //    GameSceneData.sharedInstance.AddPerfect();
        //    isSuccess = false;
        //}
        //else
        //{
        //    // 실패 처리
        //    GameSceneData.sharedInstance.AddMiss();
        //    Debug.Log("Slide Failed");
        //}
    }

    public void IsStartOverlappingJudgmentLine()
    {
        // Start와 판정라인의 겹침 여부 판정 코드 구현
        if(StartObject.isOverlapped)
        {
            isPressed = true;
        }
    }

    public void IsEndOverlappingJudgmentLine()
    {
        if (EndObject.isSuccess)
        {
            isPressed = false;
            isSuccess = true;
            EndObject.isSuccess = false;
        }
    }

    public void Success()
    {
        Debug.Log("Slide Success");
        GameSceneData.sharedInstance.AddPerfect();
        isSuccess = false;
        gameObject.SetActive(false);
    }
}
