using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fruit : MonoBehaviour, ISaveable
{

    private int age;
    private species type;
    private string uuid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // creates fruit from save data
    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.FruitData fruitData = new SaveData.FruitData();
        fruitData.uuid = "generate later lol";
        fruitData.age = age;
        fruitData.type = type;
        a_SaveData.m_FruitData.Add(fruitData);
    }

    // stores fruit as save data
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach(SaveData.FruitData fruitData in a_SaveData.m_FruitData)
        {
            if (fruitData.uuid == uuid)
            {
                type = fruitData.type;
                age = fruitData.age;
                break;
            }
        }
    }
}
