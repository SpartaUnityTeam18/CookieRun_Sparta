using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 도전 과제 매니저   현재 씬이름 받아와서 작동 여부 해보기
public class AchievementManager : Singleton<AchievementManager>
{
    public class Achievement
    {
        // 도전 과제 이름
        public string name;
        // 도전 과제 달성 시 텍스트
        public string message;
        // 목표 값
        public int total;
        // 현재 값
        public int current;
        // 도전 과제 달성 여부
        public bool Completed;
        // 목표 달성 여부 (current >= total 이면 true)
        public bool isComplete => current >= total;

        public Achievement(string _name, string message, int _total)
        {
            this.name = _name;
            this.message = message;
            this.total = _total;
            this.current = 0;
            this.Completed = false;
        }

        public void AddCurrent(int amount = 1)
        {
            // 완료된 거면 리턴
            if (PlayerPrefs.GetInt(name, 0) == 1) return;

            // current 증가
            current += amount;

            // 달성 여부가 true일때
            if (isComplete)
            {
                // 도전 과제 완료 시
                Completed = true;
                UIManager.Instance.ShowAchievement(message);
                GameManager.Instance.SaveAchievement(name, isComplete);
            }
        }

        public void Reset()
        {
            // 미완료 업적일때만 초기화 진행
            if (!Completed)
            {
                current = 0;
                Debug.Log($"{name} 업적 진행도 초기화됨!");
            }
        }
    }

    // 딕서녀리 사용 (키 타입 string / 데이터 타입 Achievement)
    private Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    private void Start()
    {
        // 초기화 진행
        achievements["Dodge"] = new Achievement("Dodge", "10회 연속 장애물 회피 업적 달성 : 스테이지 2 해금 완료", 10);
        achievements["Jelly"] = new Achievement("Jelly", "젤리 100개 먹기 업적 달성 : 천사맛 쿠키 해금 완료", 100);
        achievements["Score"] = new Achievement("Score", "600점 달성 업적 달성 : 스테이지 3 해금 완료", 600);
    }

    public void UpdateAchievement(string name, int amount = 1)
    {
        // 도전 과제 진행 사항 업데이트, 지정된 키가 있을때 isTutorialScene이 false 일때
        if (achievements.ContainsKey(name) && !GameManager.Instance.isTutorialScene)
        {
            achievements[name].AddCurrent(amount);
        }
    }

    public void RestDodgeAchievement()
    {
        // 피격 시 회피 도전 과제 리셋, 지정된 키가 있을 때 isTutorialScene이 false 일때 Reset
        if (achievements.ContainsKey("Dodge") && !GameManager.Instance.isTutorialScene)
        {
            achievements["Dodge"].Reset();
        }
    }
}
