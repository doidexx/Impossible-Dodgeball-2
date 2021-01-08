using UnityEngine;
using TMPro;

public class RequirementDisplay : MonoBehaviour
{
    [Header("Requirement Text")]
    public GameObject button = null;
    public TextMeshProUGUI requirementText = null;

    public void UpdateRequirementText(bool show, string requirement)
    {
        button.gameObject.SetActive(show);
        requirementText.text = requirement;
    }
}
