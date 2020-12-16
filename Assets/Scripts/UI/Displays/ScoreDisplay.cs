using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public string initialMessage = "";

    GameManager gameManager = null;
    TextMeshProUGUI text = null;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = initialMessage + gameManager.score.ToString("0");
    }
}
