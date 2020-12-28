using System;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [Header("Scores")]
    public int lastScore = 0;
    public int highScore = 0;
    public int highRound = 0;
    [Header("Skins Selection")]
    public int selectedModelId = 0;
    public int selectedMaterialId = 0;
    public int selectedBallId = 0;
    [Header("Settings")]
    public float musicVolume = 0;
    public float SFXVolume = 0;
    [Header("All Skins")]
    public int[] ownedSkinsID = null;

    private void Awake()
    {
        DataHolder[] holders = FindObjectsOfType<DataHolder>();
        if (holders.Length > 1)
            Destroy(holders[1].gameObject);
        LoadData();
        DontDestroyOnLoad(this);
    }

    public void SaveData()
    {
        var manager = FindObjectOfType<GameManager>();
        if (manager != null)
        {
            lastScore = manager.score;
            if (manager.score > highScore)
                highScore = manager.score;
        }
        var audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
        {
            musicVolume = audioController.GetComponent<AudioSource>().volume;
            SFXVolume = audioController.SFX_Volume;
        }
        SaveLoadManager.Save(this);
    }

    public void LoadData()
    {
        var data = SaveLoadManager.Load(this);
        if (data == null)
            return;

        lastScore = data.lastScore;
        highScore = data.highScore;
        highRound = data.highRound;

        selectedMaterialId = data.selectedMaterialId;
        selectedModelId = data.selectedModelId;

        selectedBallId = data.selectedBallId;

        musicVolume = data.musicVolume;
        SFXVolume = data.SFXVolume;
    }

    public void AddToOwnedSkins(int materialID) 
    {
        int[] list = new int[ownedSkinsID.Length + 1];
        for (int i = 0; i < ownedSkinsID.Length; i++)
        {
            list[i] = ownedSkinsID[i];
        }
        list[list.Length - 1] = materialID;
        ownedSkinsID = list;
    }

    public bool IsInList(int materialID)
    {
        foreach (int id in ownedSkinsID)
        {
            if (id == materialID)
                return true;
        }
        return false;
    }
}
