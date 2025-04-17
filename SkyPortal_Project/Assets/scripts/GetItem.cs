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
                if(Vector3.Distance(transform.position, item.position) < 0.5f)
                {
                    Debug.Log("Item collected: " + item.name);
                    Destroy(item.gameObject); // アイテムを破壊
                    itemsInRange.Remove(item); // リストから削除
                    // ここでアイテムを取得する処理を追加することも可能
                    // 例えば、アイテムのデータをプレイヤーのインベントリに追加するなど
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Item detected: " + other.name);
            itemsInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Item exited: " + other.name);
            itemsInRange.Remove(other.transform);
        }
    }

}