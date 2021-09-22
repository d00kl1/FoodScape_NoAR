using System.Collections.Generic;
using UnityEngine;

public enum species
{
    lemon,
}

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct FruitData
    {
        public string uuid;
        public int age;
        public species type;
    }
    
    public int m_Score;
    public List<FruitData> m_FruitData = new List<FruitData>();
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}