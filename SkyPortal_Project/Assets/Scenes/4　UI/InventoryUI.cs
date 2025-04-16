using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // スロットのプレハブ
    public Transform slotParent;  // スロットを配置する親オブジェクト
    public int inventorySize = 36; // インベントリのスロット数

void Start()
{
    InitializeInventory();
}

void InitializeInventory()
{
    for (int i = 0; i < inventorySize; i++)
    {
        Instantiate(slotPrefab, slotParent);
    }
}

}
