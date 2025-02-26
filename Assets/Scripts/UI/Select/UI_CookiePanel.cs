using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CookiePanel : MonoBehaviour
{
    public GameObject cookiePrefab;
    public Button cookieButton;

    public GameObject cookieLocked;

    public Image cookieImage;
    public TextMeshProUGUI cookieName;

    private void Start()
    {
        cookieButton.onClick.AddListener(PassCookie);

        cookieImage.sprite = cookiePrefab.GetComponent<Cookie>().cookieSprite;
        cookieName.text = cookiePrefab.GetComponent<Cookie>().cookieName;
    }

    void PassCookie()//쿠키 선택하면 게임매니저에 알려줌
    {
        GameManager.Instance.SetCookie(cookiePrefab);
    }

    public void SetLock(bool flag)
    {
        cookieLocked.SetActive(!flag);
    }
}
