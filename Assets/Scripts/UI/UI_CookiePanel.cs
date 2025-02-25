using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CookiePanel : MonoBehaviour
{
    public GameObject cookiePrefab;
    public Button cookieButton;

    private void Start()
    {
        cookieButton.onClick.AddListener(PassCookie);
    }

    void PassCookie()
    {
        GameManager.Instance.SetCookie(cookiePrefab);
    }
}
