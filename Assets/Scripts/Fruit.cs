using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fruit : MonoBehaviour, ISaveable
{

    private int age;
    private species type;
    private System.Guid guid;
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
        fruitData.guid = guid;
        fruitData.age = age;
        fruitData.type = type;
        a_SaveData.m_FruitData.Add(fruitData);
    }

    // stores fruit as save data
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach(SaveData.FruitData fruitData in a_SaveData.m_FruitData)
        {
            type = fruitData.type;
            age = fruitData.age;
            guid = fruitData.guid;
            break;
        }
    }

    //setters
    public void setAge(int n)
    {
        age = n;
    }

    public void setType(species newType)
    {
        type = newType;
    }

    public void instantiate(species newType, System.Guid guid, int n = 0)
    {
        type = newType;
        age = n;
        this.guid = guid;
    }

    //getters
    public int getAge()
    {
        return age;
    }

    public species getType()
    {
        return type;
    }

    public System.Guid getUuid()
    {
        return guid;
    }
}
