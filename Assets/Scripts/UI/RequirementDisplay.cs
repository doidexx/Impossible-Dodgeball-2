using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RequirementDisplay : MonoBehaviour
{
    [Header("Requirement Text")]
    public Button button = null;
    public TextMeshProUGUI requirementText = null;

    public void UpdateRequirementText(bool show, bool buyable, string requirement)
    {
        button.gameObject.SetActive(show);
        button.enabled = buyable;
        requirementText.text = requirement;
    }
}
