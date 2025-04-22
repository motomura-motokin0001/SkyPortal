using System.Collections.Generic;
using UnityEngine;


public class GetItem : MonoBehaviour
{
    public float attractSpeed = 3f;
    private List<Transform> itemsInRange = new List<Transform>();

    void Update()
    {
        foreach (Transform item in new List<Transform>(itemsInRange))
        {
            if (item != null)
            {
                Vector3 direction = (transform.position - item.position).normalized;
                item.position += direction * attractSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, item.position) < 0.5f)
                {
                    Destroy(item.gameObject); // アイテムを破壊
                    itemsInRange.Remove(item); // リストから削除
                    Debug.Log("アイテムを取得！: " + item.name);
                    
                    ItemPickup pickup = item.GetComponent<ItemPickup>();
                    if (pickup != null)
                    {
                        Inventory.instance.Add(pickup.item);
                    }
                    else
                    {
                        Debug.LogWarning("ItemPickup がアタッチされていません: " + item.name);
                    }
                    
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("範囲内に入った: " + other.name);
            itemsInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("範囲内からでた: " + other.name);
            itemsInRange.Remove(other.transform);
        }
    }
}