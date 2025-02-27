using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialZone : MonoBehaviour
{
    // 튜토리얼 ui 연결 > tutorialzone에서 연결해서 사용
    public GameObject tutorialUI;
    // 다음 씬 지정 > 선택 씬으로 변경
    public string nextSceneName = "Select";
    // ui 번호
    private int number = 0;
    // ui Active 여부
    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 쿠키가 충돌했고  ui가 꺼져있을때 isTutorialScene이 true 일때
        if (collision != null && collision.CompareTag("Cookie") && !isActive && GameManager.Instance.isTutorialScene)
        {
            // UI 활성화
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        // 먼저 모든 UI를 비활성화
        foreach (Transform child in tutorialUI.transform)
        {
            child.gameObject.SetActive(false);
        }

        // 현재 number에 해당하는 UI 활성화
        // transform.childCount > gameobject는 다 가지고 있는 transform에 있는 childCount를 사용해서 자식 객체의 수를 알 수 있음.
        if (number < tutorialUI.transform.childCount)
        {
            // 타임스케일 0로 해서 멈춰!
            Time.timeScale = 0f;

            // 하위 UI 중 number에 해당되는 녀석 활성화 해줌
            tutorialUI.transform.GetChild(number).gameObject.SetActive(true);

            // number를 올려줘서 다시 메서드 호출 시 다음번 UI를 활성화 하게 해줌
            number++;
        }
        // UI 활성화 상태기 때문에 true
        isActive = true;
    }

    void HideTutorial()
    {
        // 모든 튜토리얼 UI 비활성화
        foreach (Transform child in tutorialUI.transform)
        {
            child.gameObject.SetActive(false);
        }

        //타임스케일 1로 돌려놓기
        Time.timeScale = 1f;

        // isActive를 false로 해줘서 다음에 충돌했을 때 UI가 나올 수 있게 해줌
        isActive = false;
    }

    void LoadNextScene()
    {
        // 해당 문자열 Null인지 체크 > 아니면 false 반환 > ! 라서 true
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // 지정한 씬으로 전환, 타임스케일 1로 돌려놓기
            SceneManager.LoadScene(nextSceneName);
            Time.timeScale = 1f;
        }
    }

    void OnEnter()
    {
        // 엔터키 입력시 넘어감
        if (isActive)
        {
            // UI가 마지막이거나 넘어섰을때
            if (number >= tutorialUI.transform.childCount)
            {
                // 지정한 다음 씬으로 전환
                SoundManager.Instance.StopAllSound();
                LoadNextScene();
                return;
            }
            else
            {
                // 아니면 UI 가려줌
                HideTutorial();
            }
        }
    }
}
