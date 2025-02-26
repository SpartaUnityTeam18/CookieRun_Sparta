using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Start : MonoBehaviour
{
    public Image cookieImage;
    public Image mapImage;

    public TextMeshProUGUI cookieText;
    public TextMeshProUGUI mapText;

    public Button cookieButton;
    public Button mapButton;
    public Button startButton;

    public GameObject cookieSelection;
    public GameObject mapSelection;
    public GameObject cookiePanels;

    public Button cookieExitButton;
    public Button mapExitButton;

    void Start()
    {
        cookieImage.sprite = GameManager.Instance.cookiePrefab.GetComponent<Cookie>().cookieSprite;
        cookieText.text = GameManager.Instance.cookiePrefab.GetComponent<Cookie>().cookieName;

        cookieSelection.SetActive(false);
        mapSelection.SetActive(false);

        cookieButton.onClick.AddListener(CookieButton);
        cookieExitButton.onClick.AddListener(CookieExitButton);
        mapButton.onClick.AddListener(MapButton);
        mapExitButton.onClick.AddListener(MapExitButton);
        startButton.onClick.AddListener(StartButton);

        FindCookiePanels();
    }

    void FindCookiePanels()
    {
        for (int i = 0; i < cookiePanels.transform.childCount; i++) 
        {
            Button button = cookiePanels.transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => OnClickCookiePanelClicked(button));
        }
    }

    void OnClickCookiePanelClicked(Button btn)
    {
        Cookie cookie = btn.gameObject.GetComponent<UI_CookiePanel>().cookiePrefab.GetComponent<Cookie>();
        cookieImage.sprite = cookie.cookieSprite;
        cookieText.text = cookie.cookieName;
        CookieExitButton();
    }

    void CookieButton()
    {
        cookieSelection.SetActive(true);
    }

    void CookieExitButton()
    {
        cookieSelection.SetActive(false);
    }

    void MapButton()
    {
        mapSelection.SetActive(true);
    }

    void MapExitButton()
    {
        mapSelection.SetActive(false);
    }

    void StartButton()
    {
        SceneManager.LoadScene("Stage_1");
    }
}
