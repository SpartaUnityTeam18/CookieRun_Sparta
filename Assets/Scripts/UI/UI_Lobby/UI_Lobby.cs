using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{
    public Button playButton;
    public Button cookieButton;
    public Button optionButton;
    public Button exitButton;

    public GameObject cookieListPanel;
    public GameObject optionPanel;

    public Button cookieCloseButton;
    public Button optionCloseButton;

    public TextMeshProUGUI coin;

    private void Start()
    {
        cookieListPanel.SetActive(false);
        optionPanel.SetActive(false);

        playButton.onClick.AddListener(OnPlayButtonClicked);
        cookieButton.onClick.AddListener(OnCookieButtonClicked);
        optionButton.onClick.AddListener(OnOptionButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        cookieCloseButton.onClick.AddListener(OnCookieCloseButtonClicked);
        optionCloseButton.onClick.AddListener(OnOptionCloseButtonClicked);

        UpdateCoin();

        SoundManager.Instance.StopAllSound();
        SoundManager.Instance.PlayBGM("Bgm_Lobby_0");
    }

    private void Update()
    {
        UpdateCoin();
    }

    void UpdateCoin()
    {
        coin.text = GameManager.Instance.totalCoin.ToString();
    }

    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Select");
    }

    void OnCookieButtonClicked()
    {
        cookieListPanel.SetActive(!cookieListPanel.activeSelf);
    }

    void OnCookieCloseButtonClicked()
    {
        cookieListPanel.SetActive(false);
    }

    void OnOptionButtonClicked()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
    }

    void OnOptionCloseButtonClicked()
    {
        optionPanel.SetActive(false);
    }

    void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
