using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    public GameObject clearUI;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Cookie"))
        {
            ShowClearUI();
        }
    }

    void ShowClearUI()
    {
        clearUI.SetActive(true); // UI 활성화
        Time.timeScale = 0f; // 게임 멈춤 (선택 사항)
    }
}
