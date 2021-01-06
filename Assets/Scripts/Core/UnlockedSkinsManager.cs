using System.Collections.Generic;
using UnityEngine;

public class UnlockedSkinsManager : MonoBehaviour
{
    public DataHolder holder = null;
    public Transform content = null;
    public Transform secondContent = null;
    public List<int> unlockedSkinsIDs = null;

    SkinButton[] buttons = null;

    private void Awake()
    {
        var unlockedManagers = FindObjectsOfType<UnlockedSkinsManager>();
        if (unlockedManagers.Length > 1)
            Destroy(unlockedManagers[1]);
        DontDestroyOnLoad(this);
        buttons = new SkinButton[content.childCount + secondContent.childCount];
        AddToButtons();
    }

    private void Start()
    {
        foreach (var button in buttons)
        {
            if (holder.highScore < button.scoreRequirement)
                continue;
            AddToList(button.material.GetInstanceID());
        }
    }

    private void AddToButtons()
    {
        var lastIndex = 0;
        for (int i = 0; i < content.childCount; i++)
        {
            buttons[lastIndex] = content.GetChild(i).GetComponent<SkinButton>();
            lastIndex++;
        }
        for (int i = 0; i < secondContent.childCount; i++)
        {
            buttons[lastIndex] = secondContent.GetChild(i).GetComponent<SkinButton>();
            lastIndex++;
        }
    }

    public void AddToList(int id)
    {
        if (AlreadyInList(id) == true)
            return;
        unlockedSkinsIDs.Add(id);
    }

    public bool AlreadyInList(int id)
    {
        foreach (var skinID in unlockedSkinsIDs)
        {
            if (skinID == id)
                return true;
        }
        return false;
    }
}
