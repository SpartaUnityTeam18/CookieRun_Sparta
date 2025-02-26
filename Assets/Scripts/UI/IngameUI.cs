using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using JetBrains.Annotations;


public class InGameUI : BaseUI
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public Slider healthBar;
    public float decreaseRate = 2f;

    public void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        scoreUpdate();
        highscoreUpdate();
        healthBarUpdate();
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
        highScoreText.text = PlayerPrefs.GetInt("Map_1_highScore", 0).ToString();
    }

    public void healthBarUpdate()
    {
        float rate = (UIManager.Instance.cookie._hp / UIManager.Instance.cookie._maxHp); //value 값 나온거를 slider에 대입
        healthBar.value = rate;
    }
}







//public class SlideButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    public static event Action SlideButtonDown;
//    public static event Action SlideButtonUp;

//    private Button _slideButton;

//    private void Awake()
//    {
//        _slideButton = GetComponent<Button>();
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        SlideButtonDown?.Invoke();
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        SlideButtonUp?.Invoke();
//    }
//}



//public class JumpButton : MonoBehaviour
//{
//    public static event Action OnClickJumpButton;

//    public void JumpClick()
//    {
//        OnClickJumpButton?.Invoke();
//    }
//}

//public class TotalScore : MonoBehaviour
//{
//    private Text _totalScoreText;
//    private IEnumerator _raiseScore;

//    private void Awake()
//    {
//        _totalScoreText = GetComponent<Text>();
//    }


//    private void Start()
//    {
//        _raiseScore = CountingScore(Cookie.ScoreUI, 0);
//        StartCoroutine(_raiseScore);
//    }

//    IEnumerator CountingScore(float maxScore, float initialScore)
//    {
//        float duration = 0.9f;
//        float offset = maxScore / duration;

//        while (initialScore < maxScore)
//        { 

//            initialScore += offset * Time.deltaTime;
//            _totalScoreText.text = $"{initialScore: #,0}";
//            yield return null;
//        }

//        initialScore = maxScore;
//        _totalScoreText.text = $"{initialScore: #,0}";
//    }
//}


