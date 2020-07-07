using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float timeBeforeNextRound = 10;
    float roundTimer = 10;
    public float getRoundTimer { get { return roundTimer; } }
    [SerializeField] float timeBeforeGameOver = 3;
    float gameoverTimer = 0;

    int score = 0;
    int round = 0;
    int scoreGoal = 1;
    public int getScore { get { return score; } }
    public int getRound { get { return round; } }

    public bool isPlayerHit = false;
    private bool hasWon = false;

    BallSpawner ballSpawner;

    private void Start()
    {
        roundTimer = timeBeforeNextRound;
        ballSpawner = GetComponent<BallSpawner>();
    }

    public void Update()
    {
        if (hasWon)
            RoundWon();
        if (isPlayerHit)
            RoundLost();
    }

    private void StartNewRound()
    {
        round++;
        scoreGoal = round * UnityEngine.Random.Range(20, 45);
        ballSpawner.SetAmountToSpawn(scoreGoal);
        ballSpawner.SetCanSpawn(true);
        Player.canMove = true;
    }

    public void OnHomeScreen()
    {
        ballSpawner.DisableActiveBalls();
        Time.timeScale = 1;
        ResetGame();
        hasWon = false;
    }

    public void PlayAgain()
    {
        GetComponent<UIManager>().OpenScreen(Screen.gameplay);
        ResetGame();
        hasWon = true;
    }

    private void ResetGame()
    {
        FindObjectOfType<Player>().ResetPlayer();
        roundTimer = timeBeforeNextRound;
        score = 0;
        round = 0;
        scoreGoal = score + 1;
        isPlayerHit = false;
        Player.canMove = false;
        ballSpawner.SetCanSpawn(false);
    }

    public void IncreaseScore()
    {
        if (isPlayerHit) return;
        if (hasWon) return;
        score++;
        if (score == scoreGoal)
            hasWon = true;
    }

    private void RoundWon()
    {
        roundTimer -= Time.deltaTime;
        if (roundTimer > 5)
        {
            Player.canMove = false;
            //Start celebration animation
            //Start celebration FXs
        }
        else if (roundTimer > 0)
        {
            Player.canMove = true;
            //Stop all celebration elements half way through count down
        }
        else
        {
            hasWon = false;
            StartNewRound();
            roundTimer = timeBeforeNextRound;
        }
    }

    private void RoundLost()
    {
        gameoverTimer += Time.deltaTime;
        if (gameoverTimer < timeBeforeGameOver) return;

        FindObjectOfType<UIManager>().OpenScreen(Screen.gameOver);
        isPlayerHit = false;
        gameoverTimer = 0;
        ballSpawner.SetCanSpawn(false);
    }
}

