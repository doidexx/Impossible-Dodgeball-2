using ID.Core;
using TMPro;
using UnityEngine;

namespace ID.UI.Displays
{
    public class ScoreDisplay : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private GameManager _gameManager;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            _text.text = "Score: " + _gameManager.getScore;
        }
    }
}
