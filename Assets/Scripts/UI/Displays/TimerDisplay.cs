using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
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
        var time = gameManager.getRoundTimer;
        if (time == 0 || time == 10)
            text.enabled = false;
        else
            text.enabled = true;
        text.text = string.Format("{0:0}", time);
    }
}
