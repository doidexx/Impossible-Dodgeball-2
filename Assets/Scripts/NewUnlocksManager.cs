using UnityEngine;
using UnityEngine.UI;

public class NewUnlocksManager : MonoBehaviour
{
    public Transform content = null;
    public Image image = null;
    public GameObject noNewsText = null;
    public GameObject newUnlocksText = null;
    public SkinButton[] skinButtons = null;

    UnlockedSkinsManager skinsManager = null;
    DataHolder holder = null;
    int lastIndexChecked = 0;
    int lastIndexActivated = 0;

    private void Awake()
    {
        skinsManager = FindObjectOfType<UnlockedSkinsManager>();
        holder = FindObjectOfType<DataHolder>();
    }

    private void Start()
    {
        CheckSkins();
        noNewsText.SetActive(content.childCount == 0);
        newUnlocksText.SetActive(content.childCount > 0);
        EnableImage();
    }

    private void CheckSkins() //Runs From animation Events after the first one
    {
        for (int i = lastIndexChecked; i < skinButtons.Length; i++)
        {
            lastIndexChecked++;
            if (skinsManager.AlreadyInList(skinButtons[i].material.GetInstanceID()) == true)
                continue;
            AddToNewSkins(skinButtons[i]);
        }
    }

    private void AddToNewSkins(SkinButton button)
    {
        if (holder.highScore >= button.scoreRequirement)
        {
            var image = Instantiate(this.image, content);
            image.sprite = button.sprite;
            skinsManager.AddToList(button.material.GetInstanceID());
        }
    }

    public void EnableImage()
    {
        if (content.childCount == 0 || lastIndexActivated == content.childCount)
            return;
        content.GetChild(lastIndexActivated).gameObject.SetActive(true);
        lastIndexActivated++;
    }
}
