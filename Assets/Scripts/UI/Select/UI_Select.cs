using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Select : MonoBehaviour
{
    public Image cookieImage;
    public Image mapImage;

    public TextMeshProUGUI cookieText;
    public TextMeshProUGUI mapText;

    public Button cookieButton;
    public Button mapButton;
    public Button startButton;
    public Button lobbyButton;

    public GameObject cookieSelection;
    public GameObject cookiePanels;
    public GameObject mapSelection;
    public GameObject mapPanels;

    public Button cookieExitButton;
    public Button mapExitButton;

    List<UI_CookiePanel> cookiePanelsList = new();
    List<UI_MapPanel> mapPanelsList = new();

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
        lobbyButton.onClick.AddListener(() => SceneManager.LoadScene("Lobby"));

        //쿠키, 맵 목록 찾기
        FindCookiePanels();
        FindMapPanels();

        //천사맛, 맵 2, 3 잠금 확인
        cookiePanelsList[1].SetLock(GameManager.Instance.LoadAchievement("Jelly"));
        mapPanelsList[1].SetLock(GameManager.Instance.LoadAchievement("Dodge"));
        mapPanelsList[2].SetLock(GameManager.Instance.LoadAchievement("Coin"));
    }

    void FindCookiePanels()//쿠키 목록 찾아서 버튼 할당
    {
        for (int i = 0; i < cookiePanels.transform.childCount; i++) 
        {
            cookiePanelsList.Add(cookiePanels.transform.GetChild(i).GetComponent<UI_CookiePanel>());
            Button button = cookiePanelsList[i].cookieButton;
            button.onClick.AddListener(() => OnClickCookiePanelClicked(button));
        }
    }

    void FindMapPanels()//맵 목록 찾아서 버튼 할당
    {
        for (int i = 0; i < mapPanels.transform.childCount; i++)
        {
            mapPanelsList.Add(mapPanels.transform.GetChild(i).GetComponent<UI_MapPanel>());
            Button button = mapPanelsList[i].mapButton;
            button.onClick.AddListener(() => OnClickMapPanelClicked(button));
        }
    }

    void OnClickCookiePanelClicked(Button btn)//쿠키 선택 버튼 클릭하면 현재 선택된 쿠키 변경
    {
        Cookie cookie = btn.transform.parent.GetComponent<UI_CookiePanel>().cookiePrefab.GetComponent<Cookie>();
        cookieImage.sprite = cookie.cookieSprite;
        cookieText.text = cookie.cookieName;
        CookieExitButton();
    }

    void OnClickMapPanelClicked(Button btn)//맵 선택 버튼 클릭하면 현재 선택된 맵 변경
    {
        UI_MapPanel map = btn.transform.parent.GetComponent<UI_MapPanel>();
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
