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

        // Start�� �������ο� ��ġ�� ������ ��
        //if (IsStartOverlappingJudgmentLine())
        //{
        //    isPressed = true;
        //}

        // End�� �������ο� ��ġ�� ������ ��
        //if (IsEndOverlappingJudgmentLine())
        //{
        //    Debug.Log("���� �߰�");
        //}

        // ��� ����
        //if (isSuccess)
        //{
        //    // ���� ó��
        //    Debug.Log("Slide Success");
        //    GameSceneData.sharedInstance.AddPerfect();
        //    isSuccess = false;
        //}
        //else
        //{
        //    // ���� ó��
        //    GameSceneData.sharedInstance.AddMiss();
        //    Debug.Log("Slide Failed");
        //}
    }

    public void IsStartOverlappingJudgmentLine()
    {
        // Start�� ���������� ��ħ ���� ���� �ڵ� ����
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
