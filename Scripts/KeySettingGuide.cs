using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeySettingGuide : MonoBehaviour
{
    public TMP_Text text;

    private void Awake()
    {
#if UNITY_EDITOR || PLATFORM_STANDALONE_WIN
        text.text = "���۹� : \nȭ��ǥŰ ��,��� ����\nSpace Bar : �� ����\nESC : ��������";
#elif UNITY_ANDROID
        text.text = "���۹� : \nȭ�� �� �� ��ġ�� �� �̵�\n�� ��ġ�� ����";
#endif
    }
}