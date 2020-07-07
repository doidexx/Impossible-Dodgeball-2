using UnityEngine;

namespace ID.Core
{
    public class BallSpawner : MonoBehaviour
    {
        [Header("Spawn Position")]
        [SerializeField] private float spawnPointX = 0;
        [SerializeField] private float spawnPointY = 0;
        [SerializeField] private float spawnPointDepth = 60;

        [Header("Cooldown Settings")]

        [SerializeField] [Range(0, 1)]
        private float timeBetweenBalls = 0.3f;
        private float _timeSinceLastBall = Mathf.Infinity;

        [Header("Amounts")]
        [SerializeField]
        private int amountToSpawn = 30;
        
        private int _amountSpawned = 0;

        [Header("Pool settings")]
        [SerializeField]
        private Transform pool = null;
        [SerializeField] private int amountOfBalls = 30;
        [SerializeField] private Ball ballPrefab = null;

        private int _currentBallIndex = 0;
        private bool _canSpawn = false;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            PopulatePool();
        }

        private void PopulatePool()
        {
            if (pool == null || ballPrefab == null) return;
            for (var i = 0; i < amountOfBalls; i++)
            {
                var newBall = Instantiate(ballPrefab, pool, true);
            }
        }

        private void Update()
        {
            _timeSinceLastBall += Time.deltaTime;
            if (_timeSinceLastBall > timeBetweenBalls)
                SpawnBall();
        }

        private void SpawnBall()
        {
            if (HaveAllSpawned()) return;
            if (!_canSpawn || !pool) return;
            if (_gameManager.isPlayerHit) return;

            _currentBallIndex++;
            if (_currentBallIndex == pool.childCount)
                _currentBallIndex = 0;

            GameObject currentBall = pool.GetChild(_currentBallIndex).gameObject;
            if (!currentBall.activeInHierarchy)
            {
                currentBall.transform.position = GetNewSpawnPosition();
                currentBall.SetActive(true);
            }
            _timeSinceLastBall = 0;
            _amountSpawned++;
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
            return _amountSpawned == amountToSpawn;
        }

        private Vector3 GetNewSpawnPosition()
        {
            var x = UnityEngine.Random.Range(-spawnPointX, spawnPointX);
            var y = UnityEngine.Random.Range(5f, spawnPointY);
            return new Vector3(x, y, spawnPointDepth);
        }

        public void SetCanSpawn(bool value)
        {
            _canSpawn = value;
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
}
