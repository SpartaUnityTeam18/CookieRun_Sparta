using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public Slider healthBar;

    public void UpdateScore(int currentScore, int highScore)
    {
        currentScoreText.text = "Score: " + currentScore.ToString();
        highScoreText.text = "Best: " + highScore.ToString();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.value = currentHealth / maxHealth;
    }
}