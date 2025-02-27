using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.InputSystem;


public class InGameUI : BaseUI
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public Slider healthBar;
    public float decreaseRate = 2f;

    public InputAction inputAction;
    public Button slideButton;

    public Button jumpButton;


    public void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        scoreUpdate();
        highscoreUpdate();
        healthBarUpdate();
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SimulateButtonPress(jumpButton);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SimulateButtonPress(slideButton);
        }
        void SimulateButtonPress(Button button)
        {
            // ��ư Ŭ�� �ð� ȿ��
            button.onClick.Invoke();  // ���� ��ư Ŭ�� �̺�Ʈ ����
            StartCoroutine(PressEffect(button));
        }

        System.Collections.IEnumerator PressEffect(Button button)
        {
            var originalColor = button.colors;
            ColorBlock cb = button.colors;
            cb.normalColor = cb.pressedColor;
            button.colors = cb;

            yield return new WaitForSeconds(0.1f); // ���� ���� ���� �ð�

            cb.normalColor = originalColor.normalColor;
            button.colors = cb;
        }
    
    }

    protected override UIState GetUIState()
    {
        return UIState.InGame;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    public void scoreUpdate()
    {
        currentScoreText.text = GameManager.Instance.totalScore.ToString();
    }

    public void highscoreUpdate()
    {
        highScoreText.text = PlayerPrefs.GetInt($"Map_{GameManager.Instance.sceneName.Split('_')[1]}_HighScore", 0).ToString();
    }

    public void healthBarUpdate()
    {
        float maxHealth = UIManager.Instance.cookie._maxHp;
        float rate = (UIManager.Instance.cookie._hp / UIManager.Instance.cookie._maxHp); //value �� ���°Ÿ� slider�� ����
        healthBar.value = rate;
        gameObject.GetComponent<Image>().fillAmount = 0.5f;

        // ���� maxHealth�� 0�̸� ������ ������ ���Ƿ� 1�� ����
        if (maxHealth <= 0)
        {
            maxHealth = 1;
        }

        // Slider ���� ü�� ������ ����
        healthBar.value = rate;
    }

}










