using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("Settings")]
    public float timeForDoubleClick = 0.4f;
    public Sprite sprite = null;
    public Material material = null;
    [Header("Images")]
    public Image checkMark = null;
    public Image _lock = null;
    public ModelType modelType;
    [Header("Lock Settings")]
    public int scoreRequirement = 0;
    [TextArea(2, 2)]
    public string scoreRequirementText = "";

    float timeSinceLastClick = Mathf.Infinity;
    UIManager uIManager = null;
    DataHolder holder = null;

    private void Awake()
    {
        GetComponent<Image>().sprite = sprite;
        uIManager = FindObjectOfType<UIManager>();
        holder = FindObjectOfType<DataHolder>();
    }

    private void Start()
    {
        CheckLastActiveSkin();
        _lock.gameObject.SetActive(!IsUnlocked());
        scoreRequirementText += scoreRequirement;
    }

    public bool IsUnlocked()
    {
        return holder.highScore >= scoreRequirement;
    }

    private void CheckLastActiveSkin()
    {
        if (holder.selectedMaterialId == material.GetInstanceID())
        {
            Mark();
            uIManager.MarkButton(this);
        }
        else if (holder.selectedBallId == material.GetInstanceID())
        {
            Mark();
            uIManager.MarkBallButton(this);
        }
    }

    private void Update() => timeSinceLastClick += Time.deltaTime;

    public void SelectSkin()
    {
        UpdateRequirementText();
        uIManager.ChangePreviewSkinTo(modelType, material);
        if (IsUnlocked() == false)
            return;
        if (timeSinceLastClick < timeForDoubleClick)
            ApplySkin();
        timeSinceLastClick = 0;
    }

    public void SelectBall()
    {
        UpdateRequirementText();
        uIManager.ChangePreviewBallTo(material);
        if (IsUnlocked() == false)
            return;
        if (timeSinceLastClick < timeForDoubleClick)
            ApplyBallSkin();
        timeSinceLastClick = 0;
    }

    private void UpdateRequirementText() => FindObjectOfType<RequirementDisplay>().UpdateRequirementText(!IsUnlocked(), scoreRequirementText);

    public void ApplySkin()
    {
        holder.selectedMaterialId = material.GetInstanceID();
        holder.selectedModelId = (int)modelType;
        checkMark.gameObject.SetActive(true);
        uIManager.MarkButton(this);
    }

    public void ApplyBallSkin()
    {
        holder.selectedBallId = material.GetInstanceID();
        checkMark.gameObject.SetActive(true);
        uIManager.MarkBallButton(this);
    }

    public void RemoveSelection() => checkMark.gameObject.SetActive(false);

    public void Mark() => checkMark.gameObject.SetActive(true);
}

public enum ModelType
{
    Male,
    Female,
    Chibi,
    Child
}