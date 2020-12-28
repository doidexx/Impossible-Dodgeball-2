using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("Settings")]
    public float timeForDoubleClick = 1;
    public Sprite sprite = null;
    public Material material = null;
    [Header("Images")]
    public Image checkMark = null;
    public Image _lock = null;
    public ModelType modelType;
    [Header("Lock Settings")]
    public bool unlocked = false;
    public bool purchased = false;
    public int scoreRequirement = 0;
    [TextArea(5, 10)]
    public string requirementText = "";

    float timeSinceLastClick = Mathf.Infinity;
    UIManager uIManager = null;
    DataHolder holder;

    private void Awake()
    {
        GetComponent<Image>().sprite = sprite;
        uIManager = FindObjectOfType<UIManager>();
        holder = FindObjectOfType<DataHolder>();
        unlocked = CheckOwnerShip();
        CheckLastActiveSkin();
        requirementText += scoreRequirement;
        if (unlocked == false) //Make it so it reads from dataHolder
            _lock.gameObject.SetActive(true);
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

    private bool CheckOwnerShip()
    {
        var scoreReached = holder.highScore >= scoreRequirement;
        return scoreReached;
    }

    private void Update()
    {
        timeSinceLastClick += Time.deltaTime;
    }

    public void SelectSkin()
    {
        uIManager.ChangePreviewSkinTo(modelType, material);
        if (unlocked == false)
        {
            uIManager.UpdateRequirementText(!unlocked, requirementText);
            return;
        }
        if (timeSinceLastClick < timeForDoubleClick)
            ApplySkin();
        timeSinceLastClick = 0;
    }

    public void SelectBall()
    {
        uIManager.ChangePreviewBallTo(material);
        if (unlocked == false)
            return;
        if (timeSinceLastClick < timeForDoubleClick)
            ApplyBallSkin();
        timeSinceLastClick = 0;
    }

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

    public void RemoveSelection()
    {
        checkMark.gameObject.SetActive(false);
    }

    public void Mark()
    {
        checkMark.gameObject.SetActive(true);
    }
}

public enum ModelType
{
    Male,
    Female,
    Chibi,
    Child
}