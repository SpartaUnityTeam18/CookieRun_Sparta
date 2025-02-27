using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
            GameManager.Instance.ScoreUpdate();
            ShowClearUI();
        }
    }

    void ShowClearUI()
    {
        UIManager.Instance.ChangeState(UIState.Clear); // UI 활성화

        if(GameManager.Instance.stageNumber == 3)
        {
            nextButton.SetActive(false); // Next 버튼 숨기기
        }
        else
        {
            nextButton.SetActive(true); // 다른 스테이지에서는 Next 버튼 보이기
        }

        exitButton.SetActive(true);
        GameManager.Instance.isPlaying = false;
    }
}
