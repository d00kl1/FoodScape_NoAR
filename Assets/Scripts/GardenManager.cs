using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public enum Actions 
{
    Plant,
    Harvest,    
}


//garden manager
public class GardenManager : MonoBehaviour, ISaveable
{

    public GameObject[] fruit_array;
    public GameObject gemstonePrefab;
    public GameObject item_0Prefab;
    public GameObject item_1Prefab;
    public GameObject item_2Prefab;
    public GameObject item_3Prefab;
    public GameObject item_4Prefab;
    public GameObject item_5Prefab;
    public AudioController audioController;    
    private System.Random rand = new System.Random();

    private int itemType;

    private Actions action;    

    List<GameObject> plants = new List<GameObject>();

    void Awake()
    {

        fruit_array = new GameObject[] {
        item_0Prefab,
        item_1Prefab,
         item_2Prefab,
         item_3Prefab,
        item_4Prefab,
        item_5Prefab
        };
        //used to set something up before game has started
        for (int i = 0; i < 10; i++){
            float x = (float)(rand.NextDouble() * 2 - 1);
            float z = (float)(rand.NextDouble() * 2 - 1);
            GameObject item = Instantiate(gemstonePrefab, new Vector3(x, 1.01f, z), Quaternion.identity);
            item.transform.SetParent(transform);
            item.transform.LookAt(Camera.main.transform);
            item.transform.DOScale(0, .25f).SetEase(Ease.OutBounce).From();
        }
        Debug.Log("awake");
    }

    void Start()
    {

    }

    void onApplicationQuit()
    {
        PopulateSaveData(new SaveData());
        Debug.Log("test");
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach (GameObject fruit in plants)
        {
           // fruit.LoadFromSaveData(a_SaveData);
        }
    }

    public void PopulateSaveData (SaveData a_SaveData)
    {
        // put game manager stuff like score here, ie
        //a_SaveData.(variable) = (garden manager variable)

        foreach(GameObject obj in plants)
        {
            Fruit fruit = obj.GetComponent(typeof (Fruit)) as Fruit;
            fruit.PopulateSaveData(a_SaveData);
        }
    }

    public static void SaveJsonData(IEnumerable<ISaveable> a_Saveables)
    {
        SaveData sd = new SaveData();
        foreach (var saveable in a_Saveables)
        {
            saveable.PopulateSaveData(sd);
        }

        if (FileManager.WriteToFile("SaveData01.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }

    public static void LoadJsonData(IEnumerable<ISaveable> a_Saveables)
    {
        if (FileManager.LoadFromFile("SaveData01.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            foreach (var saveable in a_Saveables)
            {
                saveable.LoadFromSaveData(sd);
            }

            Debug.Log("Load complete");
        }
    }



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool blockedByCanvasUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();            

            if (!blockedByCanvasUI)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.tag == "ground")
                    {
                        Debug.Log("Touched ground");
                        AddItem(hit.point);
                    }
                    else if ((hit.collider.tag == "item") && (action == Actions.Harvest))
                    {
                        Debug.Log("Deleting object");
                        audioController.PlayRandomClip(audioController.reverseNoteClips);

                        hit.collider.gameObject.transform.DOScale(0, .1f).SetEase(Ease.InBack).OnComplete(() => Destroy(hit.collider.gameObject));                        }
                   
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    void AddItem(Vector3 position)
    {
        var itemOffset = new Vector3(0.0f, 0.01f, 0.0f);
        
        GameObject item = null;  
        item = Instantiate(fruit_array[this.itemType], position + itemOffset, Quaternion.identity);
        item.transform.SetParent(transform);
        item.transform.LookAt(Camera.main.transform);
        
        Fruit temp = gameObject.AddComponent(typeof(Fruit)) as Fruit;
        temp.setType(species.lemon);
        item.transform.DOScale(0, .25f).SetEase(Ease.OutBounce).From();
        plants.Add(item);
        Debug.Log(plants.Count);
    }

    public void SetItemType(int itemType)
    {
        switch (itemType)
        {
            case 0:        
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                action = Actions.Plant;
                break;
            case 6:
                action = Actions.Harvest;
                break;
        }

        this.itemType = itemType;
    }
}
