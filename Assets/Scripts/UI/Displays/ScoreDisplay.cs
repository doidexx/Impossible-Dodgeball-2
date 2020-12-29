using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public string initialMessage = "";

    GameManager gameManager = null;
    TextMeshProUGUI text = null;
    float textUpdateSpeed = 3;
    float displayedScore = 0;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        displayedScore = Mathf.Lerp(displayedScore, gameManager.score, textUpdateSpeed * Time.deltaTime);
        text.text = initialMessage + (int)displayedScore;
    }
}
