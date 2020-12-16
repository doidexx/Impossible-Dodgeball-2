using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public float timeForDoubleClick = 1;
    public Sprite sprite = null;
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
            SelectSkin();
        else if (holder.selectedBallId == material.GetInstanceID())
            SelectBall();
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
        holder.SaveData();
    }

    public void ApplyBallSkin()
    {
        holder.selectedBallId = material.GetInstanceID();
        holder.SaveData();
    }
}

public enum ModelType
{
    Male,
    Female,
    Chibi,
    Child
}