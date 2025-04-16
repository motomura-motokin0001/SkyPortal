using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject slotPrefab; // スロットのプレハブ
    public Transform slotParent;  // スロットの親オブジェクト (Grid Layout Group付き)
    public int inventorySize = 36; // インベントリのスロット数
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            inventorySlots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item);
                return true;
            }
        }
        Debug.Log("インベントリがいっぱいです！");
        return false;
    }

public void RemoveItem(Item item)
{
    foreach (var slot in inventorySlots)
    {
        if (!slot.IsEmpty() && slot.GetItem() == item)
        {
            slot.ClearItem();
            Debug.Log(item.itemName + " をインベントリから削除しました！");
            return;
        }
    }
    Debug.LogWarning("インベントリ内に " + item.itemName + " が見つかりませんでした。");
}
    /// <summary>
    /// インベントリをソートする
    /// </summary>
    public void SortInventory()
    {
        Debug.Log("インベントリをソートします...");

        // アイテムが入っているスロットのみ取得
        List<Item> itemsInInventory = new List<Item>();
        foreach (var slot in inventorySlots)
        {
            if (!slot.IsEmpty())
            {
                itemsInInventory.Add(slot.GetItem());
            }
        }

        // アイテムを名前順でソート
        itemsInInventory.Sort((a, b) => a.itemName.CompareTo(b.itemName));

        // 全スロットをクリア
        foreach (var slot in inventorySlots)
        {
            slot.ClearItem();
        }

        // ソートしたアイテムを先頭から配置
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            inventorySlots[i].SetItem(itemsInInventory[i]);
        }

        Debug.Log("ソート完了！");
    }
}
