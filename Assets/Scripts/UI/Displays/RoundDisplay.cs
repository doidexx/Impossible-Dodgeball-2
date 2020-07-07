using ID.Core;
using TMPro;
using UnityEngine;

namespace ID.UI.Displays
{
    public class RoundDisplay : MonoBehaviour
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
            _text.text = "Round: " + _gameManager.getRound;
        }
    }
}
