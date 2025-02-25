using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // 튜토리얼 ui 연결
    public GameObject tutorialUI;
    // 다음 씬
    public string nextSceneName = "Stage_1";
    // ui 번호
    private int number = 0;
    // ui Active 여부
    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 쿠키가 충돌했고  ui가 꺼져있을때
        if (collision != null && collision.CompareTag("Cookie") && !isActive)
        {
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        if (number >= tutorialUI.transform.childCount)
        {
            LoadNextScene(); // 마지막 UI를 넘어서면 씬 전환
            return;
        }

        // 모든 UI를 비활성화
        foreach (Transform child in tutorialUI.transform)
        {
            child.gameObject.SetActive(false);
        }

        // 현재 number에 해당하는 UI 활성화
        if (number < tutorialUI.transform.childCount)
        {
            GameManager.Instance.isPlaying = false;
            tutorialUI.transform.GetChild(number).gameObject.SetActive(true);
            number++;
        }

        isActive = true;
    }

    private void Update()
    {
        // 엔터키 입력시 넘어감
        if (isActive && Input.GetKeyDown(KeyCode.Return))
        {
            if (number >= tutorialUI.transform.childCount)
            {
                LoadNextScene(); // 마지막 UI를 넘어서면 씬 전환
                return;
            }
            else
            {
                HideTutorial();
            }
        }
    }

    void HideTutorial()
    {
        // 모든 튜토리얼 UI 비활성화
        foreach (Transform child in tutorialUI.transform)
        {
            child.gameObject.SetActive(false);
        }

        GameManager.Instance.isPlaying = true;

        isActive = false;
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
            GameManager.Instance.isPlaying = true;
        }
    }
}
