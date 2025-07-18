using TMPro;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreboardText;
    int score = 0;

    public void ChangeScoreAmount(int amount)
    {
        score += amount;
        scoreboardText.text = score.ToString();
    }
}
