using UnityEngine;

public class Coin : Pickup
{
    ScoreboardManager scoreboardManager;
    [Tooltip("The amount of points a coin gives on pickup")]
    [SerializeField] int pointAmount = 100;

    public void Init(ScoreboardManager scoreboardManager)
    {
        this.scoreboardManager = scoreboardManager;
    }
    protected override void OnPickup()
    {
        scoreboardManager.ChangeScoreAmount(pointAmount);
    }
}
