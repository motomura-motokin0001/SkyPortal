using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // アイテム名
    public Sprite icon;     // アイテム画像
    public ItemType itemType; // アイテムの種類

    public enum ItemType
    {
        Block,
        Tool,
        Consumable,
        material
    }
}
