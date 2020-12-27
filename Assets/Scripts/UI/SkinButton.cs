using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public float timeForDoubleClick = 1;
    public Sprite sprite = null;
    public Image checkMark = null;
    public Material material = null;
    public ModelType modelType;

    float timeSinceLastClick = Mathf.Infinity;
    UIManager uIManager = null;
    DataHolder holder;

    private void Awake()
    {
        GetComponent<Image>().sprite = sprite;
        uIManager = FindObjectOfType<UIManager>();
        CheckLastActiveSkin();
    }

    private void CheckLastActiveSkin()
    {
        holder = FindObjectOfType<DataHolder>();
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
        uIManager.ChangePreviewSkinTo(modelType, material);
        if (timeSinceLastClick < timeForDoubleClick)
            ApplySkin();
        timeSinceLastClick = 0;
    }

    public void SelectBall()
    {
        uIManager.ChangePreviewBallTo(material);
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