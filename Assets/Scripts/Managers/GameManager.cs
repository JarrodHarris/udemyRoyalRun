using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    // [SerializeField] GameObject gameOverText;    //UDEMY IMPLEMENTATION
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text scoreText;
    [Tooltip("Amount of time the game will start with")]
    [SerializeField] float startTime = 30f;
    [SerializeField] ParticleSystem[] fogParticleSystems;

    float timeLeft;
    bool gameOver = false;
    bool fogVisivility = true;

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

    public void ChangeFogVisibility()
    {
        foreach (ParticleSystem fog in fogParticleSystems)
        {
            var emissionModule = fog.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = !fogVisivility;
        }
        fogVisivility = !fogVisivility;
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

        // gameOverText.SetActive(true);    //UDEMY IMPLEMENTATION
        gameOverText.text = "Game Over \n" + scoreText.text;

        scoreText.text = "";
        timeText.text = "";

        Time.timeScale = .1f;   //changes the overall time of the game. Usually running at 1f
    }
}
