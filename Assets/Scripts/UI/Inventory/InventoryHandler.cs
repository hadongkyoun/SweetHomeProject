using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

    private List<Item> items = new List<Item>();
    private InventoryUI inventoryUI;


    void Awake()
    {
        inventoryUI = FindFirstObjectByType<InventoryUI>();
    }
    void Start()
    {

    }

    void Update()
    {
        //s
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public void PlayerPickItem(Item item)
    {
        items.Add(item);
        item.transform.parent = this.transform;
        item.gameObject.SetActive(false);

        // Update UI Item information
        // And then try update ui slot in this method
        inventoryUI.UpdateItemSlotList();
    }

    //public void PlayerUseItem(int id)
    //{

    //}


}
