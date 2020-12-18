using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public float timeBetweenRounds = 15f;
    public float timeBeforeGameover = 3f;
    [Header("Scores")]
    public int score = 0;
    public int round = 0;
    [Header("Time Enlapsed")]
    public float roundTimer = Mathf.Infinity;
    [Header("States")]
    bool waitingForRound = true;
    [Header("Player Model Control")]
    public ModelControl[] models = null;

    float gameoverTimer = 0;
    int maxScore = 0;

    Player player = null;
    BallSpawner ballSpawner = null;
    UIManager uiManager = null;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        ballSpawner = FindObjectOfType<BallSpawner>();
        uiManager = FindObjectOfType<UIManager>();
        roundTimer = 0;
        ChangeAspects();
    }

    public void Update()
    {
        waitingForRound = roundTimer < timeBetweenRounds;
        if (waitingForRound == false && MaxScoreReached())
            NextRound();
        else if (MaxScoreReached())
            roundTimer = Mathf.Max(0, roundTimer += Time.deltaTime);

        if (player.playerState == PlayerState.Down)
            RoundLost();
    }

    private bool MaxScoreReached()
    {
        return score == maxScore;
    }

    private void NextRound()
    {
        round++;
        roundTimer = 0;
        maxScore += round * Random.Range(5, 10);
        ballSpawner.IncreaseAmountTo(maxScore);
        ballSpawner.IncreaseSpeed();
    }

    public void IncreaseScore()
    {
        if (round == 0 || player.playerState == PlayerState.Down)
            return;
        score++;
    }

    private void RoundLost()
    {
        gameoverTimer += Time.deltaTime;
        if (gameoverTimer < timeBeforeGameover)
            return;
        gameoverTimer = 0;
        FindObjectOfType<DataHolder>().SaveData();
        uiManager.LoadScene("Game Over");
    }

    private void ChangeAspects()
    {
        var holder = FindObjectOfType<DataHolder>();
        var allSkins = Resources.LoadAll("Skin Materials", typeof(Material));
        var allBalls = Resources.LoadAll("Ball Materials", typeof(Material));

        foreach (ModelControl model in models)
        {
            if ((int)model.modelType != holder.selectedModelId)
                continue;
            model.gameObject.SetActive(true);
            player.model = model.gameObject;
            player.GetComponent<Animator>().avatar = model.avatar;
            break;
        }
        foreach (Material skin in allSkins)
        {
            if (skin.GetInstanceID() != holder.selectedMaterialId)
                continue;
            player.GetComponentInChildren<SkinnedMeshRenderer>().material = skin;
            break;
        }
        foreach (Material skin in allBalls)
        {
            if (skin.GetInstanceID() != holder.selectedBallId)
                continue;
            ballSpawner.prefabMaterial = skin;
            break;
        }
    }
}