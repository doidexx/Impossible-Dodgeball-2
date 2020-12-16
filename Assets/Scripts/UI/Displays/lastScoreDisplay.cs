using UnityEngine;
using TMPro;

public class lastScoreDisplay : MonoBehaviour
{
    public string initialMessage = "";

    private void OnEnable()
    {
        var score = FindObjectOfType<DataHolder>().lastScore;
        GetComponent<TextMeshProUGUI>().text = initialMessage + score.ToString();
    }
}
