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
            // 버튼 클릭 시각 효과
            button.onClick.Invoke();  // 실제 버튼 클릭 이벤트 실행
            StartCoroutine(PressEffect(button));
        }

        System.Collections.IEnumerator PressEffect(Button button)
        {
            var originalColor = button.colors;
            ColorBlock cb = button.colors;
            cb.normalColor = cb.pressedColor;
            button.colors = cb;

            yield return new WaitForSeconds(0.1f); // 눌린 상태 유지 시간

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
        float rate = (UIManager.Instance.cookie._hp / UIManager.Instance.cookie._maxHp); //value 값 나온거를 slider에 대입
        healthBar.value = rate;
        gameObject.GetComponent<Image>().fillAmount = 0.5f;

        // 만약 maxHealth가 0이면 나누기 오류가 나므로 1로 설정
        if (maxHealth <= 0)
        {
            maxHealth = 1;
        }

        // Slider 값에 체력 비율을 설정
        healthBar.value = rate;
    }

}










