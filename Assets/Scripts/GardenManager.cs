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
public class GardenManager : MonoBehaviour
{
    public GameObject item_0Prefab;
    public GameObject item_1Prefab;
    public GameObject item_2Prefab;
    public GameObject item_3Prefab;
    public GameObject item_4Prefab;
    public GameObject item_5Prefab;
    public GameObject gemstonePrefab;

    public AudioController audioController;    

    private int itemType;

    private Actions action;    

    Dictionary<string, GameObject> plants = new Dictionary<string, GameObject>();

    void Awake()
    {
        GameObject item = Instantiate(item_5Prefab, new Vector3(100.0f, 50.01f, 0.0f), Quaternion.identity);
        Debug.Log("awake");
        item.transform.SetParent(transform);
        item.transform.LookAt(Camera.main.transform);

        item.transform.DOScale(0, .25f).SetEase(Ease.OutBounce).From();
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

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
    }

    void AddItem(Vector3 position)
    {
        var itemOffset = new Vector3(0.0f, 0.01f, 0.0f);
        
        GameObject item = null;        

        switch (itemType) {
            case 0:
                item = Instantiate(item_0Prefab, position + itemOffset, Quaternion.identity);
                break;
            case 1:
                item = Instantiate(item_1Prefab, position + itemOffset, Quaternion.identity);
                break;
            case 2:
                item = Instantiate(item_2Prefab, position + itemOffset, Quaternion.identity);
                break;
            case 3:
                item = Instantiate(item_3Prefab, position + itemOffset, Quaternion.identity);
                break;
            case 4:
                item = Instantiate(item_4Prefab, position + itemOffset, Quaternion.identity);
                break;
            case 5:
                item = Instantiate(item_5Prefab, position + itemOffset, Quaternion.identity);
                break;
        }
        
        
        
        audioController.PlayRandomClip(audioController.forwardNoteClips);
        item.transform.SetParent(transform);
        item.transform.LookAt(Camera.main.transform);        

        item.transform.DOScale(0, .25f).SetEase(Ease.OutBounce).From();        
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
