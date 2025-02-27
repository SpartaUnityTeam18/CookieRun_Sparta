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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            ChangeScene();
        }
    }
    void ChangeScene()
    {
        PlayerPrefs.DeleteAll();
        if (Input.GetMouseButtonDown(0))
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
                SceneManager.LoadScene("Select");  // 선택화면 씬으로 이동
            }
        }
    }
}
