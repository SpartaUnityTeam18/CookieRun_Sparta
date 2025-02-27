using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGroup : MonoBehaviour
{
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM("Bgm_Title");
    }

    void OnClick()
    {
        SoundManager.Instance.StopAllSound();
        if (!PlayerPrefs.HasKey("FirstPlay"))
        {
            PlayerPrefs.SetInt("FirstPlay", 1);  // 첫 실행 저장
            PlayerPrefs.Save();  // 저장 (필수)
            SceneManager.LoadScene("Tutorial");  // 튜토리얼 씬으로 이동
        }
        else
        {
            SceneManager.LoadScene("Lobby");  // 로비화면 씬으로 이동
        }
    }
}
