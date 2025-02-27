using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUp : MonoBehaviour
{
    public Sprite cookieSprite;
    public string cookieName;
    public int cookieId;
    int level;

    public Image cookieImage;

    public TextMeshProUGUI cookieNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI requiredCoinText;

    public Button LevelUpButton;

    List<int> hp = new List<int>() { 162, 150 };

    private void Start()
    {
        level = PlayerPrefs.GetInt($"Cookie_{cookieId}_level", 1);
        cookieNameText.text = cookieName;
        cookieImage.sprite = cookieSprite;

        UpdateLevel();
        UpdateCoin();
        UpdateHPText();

        LevelUpButton.onClick.AddListener(LevelUp);
    }

    void LevelUp()
    {
        if (level >= 15) return;
        if (GameManager.Instance.UseCoin(GetRequiredCoin()) == false) return;
        PlayerPrefs.SetInt($"Cookie_{cookieId}_level", ++level);
        levelText.text = level.ToString();
        UpdateLevel();
        UpdateCoin();
        UpdateHPText();
    }

    int GetRequiredCoin()
    {
        return (level + 1) * 100; 
    }

    void UpdateLevel()
    {
        if (level >= 15) levelText.text = "Max";
        else levelText.text = level.ToString();
    }

    void UpdateCoin()
    {
        requiredCoinText.text = GetRequiredCoin().ToString();
    }

    void UpdateHPText()
    {
        hpText.text = (hp[cookieId] + (level - 1) * 5).ToString();
    }
}
