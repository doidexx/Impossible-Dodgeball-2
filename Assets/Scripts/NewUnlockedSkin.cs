using UnityEngine;

public class NewUnlockedSkin : MonoBehaviour
{
    NewUnlocksManager unlocksManager = null;

    private void Start()
    {
        unlocksManager = FindObjectOfType<NewUnlocksManager>();
    }

    public void GetNextSkin()//Animation Event
    {
        unlocksManager.EnableImage();
    }
}
