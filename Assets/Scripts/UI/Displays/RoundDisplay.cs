using UnityEngine;
using TMPro;

public class RoundDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    GameManager gameManager;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        text.text = "Round: " + gameManager.getRound;
    }
}
