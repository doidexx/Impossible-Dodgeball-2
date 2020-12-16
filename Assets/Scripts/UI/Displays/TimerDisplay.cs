using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public GameObject lavel = null;

    GameManager gameManager = null;
    TextMeshProUGUI text = null;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (gameManager.roundTimer < 4.9 || gameManager.roundTimer == 0)
        {
            lavel.SetActive(false);
            text.text = "";
        }
        else
        {
            float timeInt = gameManager.timeBetweenRounds - gameManager.roundTimer;
            text.text = timeInt.ToString("0.0");
            lavel.SetActive(true);
        }
    }
}
