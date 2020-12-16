using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public string initialMessage = "";

    private void OnEnable()
    {
        var score = FindObjectOfType<DataHolder>().highScore;
        GetComponent<TextMeshProUGUI>().text = initialMessage + score.ToString();
    }
}
