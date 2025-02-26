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
    public GameObject cookiePanels;
    public GameObject mapSelection;
    public GameObject mapPanels;


    public Button cookieExitButton;
    public Button mapExitButton;

    void Start()
    {
        cookieImage.sprite = GameManager.Instance.cookiePrefab.GetComponent<Cookie>().cookieSprite;
        cookieText.text = GameManager.Instance.cookiePrefab.GetComponent<Cookie>().cookieName;
        mapImage.sprite = GameManager.Instance.sceneSprite;
        mapText.text = GameManager.Instance.sceneName;

        cookieSelection.SetActive(false);
        mapSelection.SetActive(false);

        cookieButton.onClick.AddListener(CookieButton);
        cookieExitButton.onClick.AddListener(CookieExitButton);
        mapButton.onClick.AddListener(MapButton);
        mapExitButton.onClick.AddListener(MapExitButton);
        startButton.onClick.AddListener(StartButton);

        FindCookiePanels();
        FindMapPanels();
    }

    void FindCookiePanels()
    {
        for (int i = 0; i < cookiePanels.transform.childCount; i++) 
        {
            Button button = cookiePanels.transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => OnClickCookiePanelClicked(button));
        }
    }

    void FindMapPanels()
    {
        for (int i = 0; i < mapPanels.transform.childCount; i++)
        {
            Button button = mapPanels.transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => OnClickMapPanelClicked(button));
        }
    }

    void OnClickCookiePanelClicked(Button btn)
    {
        Cookie cookie = btn.gameObject.GetComponent<UI_CookiePanel>().cookiePrefab.GetComponent<Cookie>();
        cookieImage.sprite = cookie.cookieSprite;
        cookieText.text = cookie.cookieName;
        CookieExitButton();
    }

    void OnClickMapPanelClicked(Button btn)
    {
        UI_MapPanel map = btn.gameObject.GetComponent<UI_MapPanel>();
        mapImage.sprite = map.mapSprite;
        mapText.text = map.sceneName;
        MapExitButton();
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
        SceneManager.LoadScene(GameManager.Instance.sceneName);
    }
}
