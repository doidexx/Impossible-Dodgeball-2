using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadManager
{
    public static void Save(DataHolder obj)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/save.sav", FileMode.Create);

        SaveData data = new SaveData(obj);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved");
    }

    public static SaveData Load(DataHolder obj)
    {
        if (File.Exists(Application.persistentDataPath + "/save.sav"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/save.sav", FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Loaded");
            return data;
        }
        else
        {
            Debug.Log("File Does NOT Exist");
            return null;
        }
    }
}

[Serializable]
public class SaveData
{
    public int lastScore;
    public int highScore;
    public int highRound;

    public int selectedModelId;
    public int selectedMaterialId;

    public int selectedBallId;

    public float musicVolume;
    public float SFXVolume;

    #region SkinsList
    #endregion

    public SaveData(DataHolder holder)
    {
        lastScore = holder.lastScore;
        highScore = holder.highScore;
        highRound = holder.highRound;

        selectedMaterialId = holder.selectedMaterialId;
        selectedModelId = holder.selectedModelId;

        selectedBallId = holder.selectedBallId;

        musicVolume = holder.musicVolume;
        SFXVolume = holder.SFXVolume;
    }
}
