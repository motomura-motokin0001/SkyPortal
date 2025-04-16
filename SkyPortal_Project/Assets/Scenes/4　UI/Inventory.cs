using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int maxSlots = 36;
    public InventoryManager inventoryManager;

    public void AddItem(Item newItem)
    {
        if (items.Count <= maxSlots)
        {
            items.Add(newItem);
            inventoryManager.AddItem(newItem);
            Debug.Log(newItem.itemName + " を取得した！");
        }
        else
        {
            Debug.Log("インベントリがいっぱい！");
        }
    }
}
