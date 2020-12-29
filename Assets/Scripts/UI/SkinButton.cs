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
    public bool purchasable = false;
    public int scoreRequirement = 0;
    [TextArea(2, 2)]
    public string scoreRequirementText = "";
    [TextArea(2, 2)]
    public string buyableText = "";

    float timeSinceLastClick = Mathf.Infinity;
    UIManager uIManager = null;
    DataHolder holder;

    private void Awake()
    {
        GetComponent<Image>().sprite = sprite;
        uIManager = FindObjectOfType<UIManager>();
        holder = FindObjectOfType<DataHolder>();
        CheckLastActiveSkin();

        CheckUnlockable(gameObject);
        _lock.gameObject.SetActive(!unlocked);
        scoreRequirementText += scoreRequirement;
    }

    public void CheckUnlockable(GameObject obj)
    {
        unlocked = holder.IsInList(material.GetInstanceID());
        var scoreReached = holder.highScore >= scoreRequirement;
        if (unlocked || !scoreReached || purchasable)
            return;
        holder.AddToOwnedSkins(material.GetInstanceID());
        unlocked = true;
        if (obj != gameObject)
            obj.GetComponent<CheckNewUnlock>().newlyUnlocks.Add(sprite);
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

    private void Update()
    {
        timeSinceLastClick += Time.deltaTime;
    }

    public void SelectSkin()
    {
        UpdateRequirementText();
        uIManager.ChangePreviewSkinTo(modelType, material);
        if (unlocked == false)
            return;
        if (timeSinceLastClick < timeForDoubleClick)
            ApplySkin();
        timeSinceLastClick = 0;
    }

    public void SelectBall()
    {
        UpdateRequirementText();
        uIManager.ChangePreviewBallTo(material);
        if (unlocked == false)
            return;
        if (timeSinceLastClick < timeForDoubleClick)
            ApplyBallSkin();
        timeSinceLastClick = 0;
    }

    private void UpdateRequirementText()
    {
        var text = (purchasable) ? buyableText : scoreRequirementText;
        uIManager.UpdateRequirementText(!unlocked, purchasable, text);
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