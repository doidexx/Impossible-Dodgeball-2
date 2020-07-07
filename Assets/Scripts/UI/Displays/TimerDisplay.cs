using ID.Core;
using TMPro;
using UnityEngine;

namespace ID.UI.Displays
{
    public class TimerDisplay : MonoBehaviour
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
            var time = _gameManager.getRoundTimer;
            if (time <= 0.1f || time >= 10)
                _text.enabled = false;
            else
                _text.enabled = true;
            _text.text = $"{time:0}";
        }
    }
}
