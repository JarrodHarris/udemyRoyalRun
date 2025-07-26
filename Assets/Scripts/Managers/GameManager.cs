using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 30f;

    float timeLeft;
    bool gameOver = false;

    public bool GameOver
    {
        get { return gameOver; }
        // set { gameOver = value; }    //NOT NEEDED
    }

    public void IncreaseTime(float amountOfTime)
    {
        if (gameOver) return;

        timeLeft += amountOfTime;
    }

    private void Start()
    {
        timeLeft = startTime;
    }

    private void Update()
    {
        DecreaseTime();
    }

    private void DecreaseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");    //F1 - to display 1 decimal point

        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    private void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;   //to disable the player input script
        gameOverText.SetActive(true);
        Time.timeScale = .1f;   //changes the overall time of the game. Usually running at 1f
    }
}
