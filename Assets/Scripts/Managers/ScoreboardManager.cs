using TMPro;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreboardText;
    int score = 0;

    public void ChangeScoreAmount(int amount)
    {
        if (gameManager.GameOver) return;

        score += amount;
        scoreboardText.text = score.ToString();
    }
}
