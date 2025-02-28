using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class ClearZone : MonoBehaviour
{
    public GameObject clearUI;
    private GameObject nextButton;
    private GameObject exitButton;

    private void Start()
    {
        nextButton = clearUI.transform.Find("NextButton").gameObject;
        exitButton = clearUI.transform.Find("ExitButton").gameObject; 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Cookie"))
        {
            GameManager.Instance.isPlaying = false;
            GameManager.Instance.ScoreUpdate();
            ShowClearUI();
        }
    }

    void ShowClearUI()
    {
        UIManager.Instance.ChangeState(UIState.Clear); // UI 활성화

        nextButton.SetActive(false); // Next 버튼 숨기기

        if (GameManager.Instance.stageNumber == 1 && GameManager.Instance.LoadAchievement("Dodge"))
        {
            nextButton.SetActive(true);
        }
        else if (GameManager.Instance.stageNumber == 2 && GameManager.Instance.LoadAchievement("Score"))
        {
            nextButton.SetActive(true);
        }
        else if (GameManager.Instance.stageNumber == 3)
        {
            nextButton.SetActive(false); // Next 버튼 숨기기
        }

        exitButton.SetActive(true);
    }
}
