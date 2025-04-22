using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    void Awake() { instance = this; }

    public List<Item> items = new List<Item>();
    public int space = 9;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("インベントリがいっぱいです");
            return false;
        }
        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
