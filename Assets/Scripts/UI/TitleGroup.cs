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
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.StopAllSound();
            SceneManager.LoadScene("Stage_1");
        }
        Debug.Log("다음 씬이 설정되지 않았습니다.");
    }
}
