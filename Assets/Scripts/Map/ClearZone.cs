using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class ClearZone : MonoBehaviour
{
    public GameObject clearUI;

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
        clearUI.SetActive(true); // UI È°¼ºÈ­
    }
}
