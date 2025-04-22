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
        public virtual void Use()
    {
        Debug.Log("使った：" + itemName);
        // ここでアイテム効果を実行
    }
}
