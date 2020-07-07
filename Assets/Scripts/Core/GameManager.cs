using UnityEngine;

namespace ID.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float timeBeforeNextRound = 10;
        private float _roundTimer = 10;
        public float getRoundTimer => _roundTimer;
        [SerializeField] private float timeBeforeGameOver = 3;
        private float _gameoverTimer = 0;

        private int _score = 0;
        private int _round = 0;
        private int _scoreGoal = 1;
        public int getScore => _score;
        public int getRound => _round;

        public bool isPlayerHit = false;
        private bool _hasWon = false;

        private BallSpawner _ballSpawner;

        private void Start()
        {
            _roundTimer = timeBeforeNextRound;
            _ballSpawner = GetComponent<BallSpawner>();
        }

        public void Update()
        {
            if (_hasWon)
                RoundWon();
            if (isPlayerHit)
                RoundLost();
        }

        private void StartNewRound()
        {
            _round++;
            _scoreGoal = _round * UnityEngine.Random.Range(20, 45);
            _ballSpawner.SetAmountToSpawn(_scoreGoal);
            _ballSpawner.SetCanSpawn(true);
            Player.canMove = true;
        }

        public void OnHomeScreen()
        {
            _ballSpawner.DisableActiveBalls();
            Time.timeScale = 1;
            ResetGame();
            _hasWon = false;
        }

        public void PlayAgain()
        {
            GetComponent<UIManager>().OpenScreen(Screen.Gameplay);
            ResetGame();
            _hasWon = true;
        }

        private void ResetGame()
        {
            FindObjectOfType<Player>().ResetPlayer();
            _roundTimer = timeBeforeNextRound;
            _score = 0;
            _round = 0;
            _scoreGoal = _score + 1;
            isPlayerHit = false;
            Player.canMove = false;
            _ballSpawner.SetCanSpawn(false);
        }

        public void IncreaseScore()
        {
            if (isPlayerHit) return;
            if (_hasWon) return;
            _score++;
            if (_score == _scoreGoal)
                _hasWon = true;
        }

        private void RoundWon()
        {
            _roundTimer -= Time.deltaTime;
            if (_roundTimer > 5)
            {
                Player.canMove = false;
                //Start celebration animation
                //Start celebration FXs
            }
            else if (_roundTimer > 0)
            {
                Player.canMove = true;
                //Stop all celebration elements half way through count down
            }
            else
            {
                _hasWon = false;
                StartNewRound();
                _roundTimer = timeBeforeNextRound;
            }
        }

        private void RoundLost()
        {
            _gameoverTimer += Time.deltaTime;
            if (_gameoverTimer < timeBeforeGameOver) return;

            FindObjectOfType<UIManager>().OpenScreen(Screen.GameOver);
            isPlayerHit = false;
            _gameoverTimer = 0;
            _ballSpawner.SetCanSpawn(false);
        }
    }
}

