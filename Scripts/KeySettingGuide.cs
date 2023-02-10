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
        text.text = "조작법 : \n화살표키 좌,우로 선택\nSpace Bar : 곡 선택\nESC : 게임종료";
#elif UNITY_ANDROID
        text.text = "조작법 : \n화면 양 끝 터치시 곡 이동\n곡 터치시 실행";
#endif
    }
}