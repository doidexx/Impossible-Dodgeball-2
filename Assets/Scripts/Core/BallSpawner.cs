using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Spawn Position")]
    [SerializeField] float spawnPointX = 0;
    [SerializeField] float spawnPointY = 0;
    [SerializeField] float spawnPointDepth = 60;

    [Header("Cooldown Settings")]

    [SerializeField] [Range(0, 1)] float timeBetweenBalls = 0.3f;
    float timeSinceLastBall = Mathf.Infinity;

    [Header("Amounts")]
    [SerializeField] int amountToSpawn = 30;
    int amountSpawned = 0;

    [Header("Pool settings")]
    [SerializeField] Transform pool = null;
    [SerializeField] int amountOfBalls = 30;
    [SerializeField] Ball ballPrefab = null;

    int currentBallIndex = 0;
    bool canSpawn = false;

    private void Start()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        if (pool == null || ballPrefab == null) return;
        for (int i = 0; i < amountOfBalls; i++)
        {
            var newBall = Instantiate(ballPrefab);
            newBall.transform.SetParent(pool);
        }
    }

    private void Update()
    {
        timeSinceLastBall += Time.deltaTime;
        if (timeSinceLastBall > timeBetweenBalls)
            SpawnBall();
    }

    private void SpawnBall()
    {
        if (HaveAllSpawned()) return;
        if (!canSpawn || !pool) return;
        if (FindObjectOfType<GameManager>().isPlayerHit) return;

        currentBallIndex++;
        if (currentBallIndex == pool.childCount)
            currentBallIndex = 0;

        GameObject currentBall = pool.GetChild(currentBallIndex).gameObject;
        if (!currentBall.activeInHierarchy)
        {
            currentBall.transform.position = GetNewSpawnPosition();
            currentBall.SetActive(true);
        }
        timeSinceLastBall = 0;
        amountSpawned++;
    }

    public void DisableActiveBalls()
    {
        foreach (Transform ball in pool)
        {
            if (ball.gameObject.activeInHierarchy == false) continue;
            ball.gameObject.SetActive(false);
        }
    }

    private bool HaveAllSpawned()
    {
        return amountSpawned == amountToSpawn;
    }

    private Vector3 GetNewSpawnPosition()
    {
        float x = UnityEngine.Random.Range(-spawnPointX, spawnPointX);
        float y = UnityEngine.Random.Range(5f, spawnPointY);
        return new Vector3(x, y, spawnPointDepth);
    }

    public void SetCanSpawn(bool value)
    {
        canSpawn = value;
    }

    public void SetAmountToSpawn(int amount)
    {
        amountToSpawn = amount;
    }

    public void ChangeBallSkinTo(Material material)
    {
        foreach(Transform ball in pool)
        {
            ball.GetComponent<Renderer>().material = material;
        }
    }
}
