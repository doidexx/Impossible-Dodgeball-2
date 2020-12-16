using TMPro;
using UnityEngine;

public class RoundDisplay : MonoBehaviour
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
        text.text = initialMessage + gameManager.round.ToString("0");
    }
}
