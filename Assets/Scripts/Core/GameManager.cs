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
    [Header("Audio Clips")]
    public AudioClip[] bounceSounds = null;
    [Range(0,1)]
    public float sneakerVolumeLimiter = 0.7f;
    [Header("Effects")]
    public ParticleSystem confeti = null;

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
        LoadData();
    }

    public void Update()
    {
        waitingForRound = roundTimer < timeBetweenRounds;
        if (waitingForRound == false && MaxScoreReached())
            NextRound();
        else if (MaxScoreReached())
            roundTimer = Mathf.Max(0, roundTimer += Time.deltaTime);

        if (round > 0 && roundTimer > 0.5 && roundTimer < 3 && confeti.isPlaying == false)
            confeti.Play();

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
        ballSpawner.IncreaseSpeed(round);
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

    #region Load Last Data
    private void LoadData()
    {
        var holder = FindObjectOfType<DataHolder>();
        ChangeToModel(holder);
        ChangeSkinTo(holder);
        ChangeBallSkinTo(holder);
        FixVolume(holder.SFXVolume);
    }

    private void ChangeBallSkinTo(DataHolder holder)
    {
        var allBalls = Resources.LoadAll("Ball Materials", typeof(Material));
        foreach (Material skin in allBalls)
        {
            if (skin.GetInstanceID() != holder.selectedBallId)
                continue;
            ballSpawner.prefabMaterial = skin;
            break;
        }
    }

    private void ChangeSkinTo(DataHolder holder)
    {
        var allSkins = Resources.LoadAll("Skin Materials", typeof(Material));
        foreach (Material skin in allSkins)
        {
            if (skin.GetInstanceID() != holder.selectedMaterialId)
                continue;
            player.GetComponentInChildren<SkinnedMeshRenderer>().material = skin;
            break;
        }
    }

    private void ChangeToModel(DataHolder holder)
    {
        foreach (ModelControl model in models)
        {
            if ((int)model.modelType != holder.selectedModelId)
                continue;
            model.gameObject.SetActive(true);
            player.model = model.gameObject;
            player.GetComponent<Animator>().avatar = model.avatar;
            break;
        }
    }

    public void FixVolume(float value)
    {
        player.GetComponent<AudioSource>().volume = value * sneakerVolumeLimiter;
        ballSpawner.UpdateBallVolume(value);
    }
    #endregion
}