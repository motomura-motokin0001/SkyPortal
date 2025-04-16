using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemIcon; // アイテムのアイコン
    private Item storedItem; // 現在スロットに入っているアイテム
    private Transform originalParent; // 元のスロットの親オブジェクト
    private static InventorySlot draggedSlot; // 現在ドラッグ中のスロット
    private static GameObject draggedIcon; // マウスに追従するアイコン

    /// <summary>
    /// アイテムをスロットにセットする
    /// </summary>
    public void SetItem(Item item)
    {
        storedItem = item;
        if (item != null)
        {
            itemIcon.sprite = item.icon;
            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }
    }

    /// <summary>
    /// アイテムをスロットから削除する
    /// </summary>
    public void ClearItem()
    {
        storedItem = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }

    /// <summary>
    /// スロットが空かどうか
    /// </summary>
    public bool IsEmpty()
    {
        return storedItem == null;
    }

    /// <summary>
    /// クリック時の処理
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ctrl + 右クリックで削除
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (storedItem != null)
                {
                    Debug.Log(storedItem.itemName + " を削除しました！");
                    ClearItem();
                }
                return;
            }
        }

        // 通常の左クリック処理
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (storedItem != null)
            {
                Debug.Log(storedItem.itemName + " を使用しました！");
            }
        }
    }

    /// <summary>
    /// ドラッグ開始時の処理
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (storedItem == null) return;

        draggedSlot = this;
        originalParent = transform.parent;

        draggedIcon = new GameObject("DraggedIcon");
        draggedIcon.transform.SetParent(transform.root);
        draggedIcon.transform.SetAsLastSibling();

        Image iconImage = draggedIcon.AddComponent<Image>();
        iconImage.sprite = itemIcon.sprite;
        iconImage.raycastTarget = false;

        RectTransform iconRect = draggedIcon.GetComponent<RectTransform>();
        iconRect.sizeDelta = new Vector2(80, 80);
    }

    /// <summary>
    /// ドラッグ中の処理
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (storedItem == null || draggedIcon == null) return;
        draggedIcon.transform.position = eventData.position;
    }

    /// <summary>
    /// ドラッグ終了時の処理
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggedIcon);
        draggedIcon = null;
    }

    /// <summary>
    /// ドロップ時の処理（スロットを入れ替える）
    /// </summary>
public void OnDrop(PointerEventData eventData)
{
    if (draggedSlot == null)
    {
        Debug.LogWarning("draggedSlot が null です！");
        return;
    }

    if (draggedSlot == this)
    {
        Debug.Log("同じスロットにドロップされたため、処理をスキップします。");
        return;
    }

    if (draggedSlot.storedItem == null)
    {
        Debug.LogWarning("draggedSlot の storedItem が null です！");
        return;
    }

    Debug.Log("アイテムを入れ替えます: " + draggedSlot.storedItem.itemName + " ↔ " + (storedItem != null ? storedItem.itemName : "空"));

    // アイテムをスワップ
    Item tempItem = storedItem;
    SetItem(draggedSlot.storedItem);
    draggedSlot.SetItem(tempItem);

    Debug.Log("アイテムの入れ替え完了！");
}
    /// <summary>
    /// スロット内のアイテムを取得する
    /// </summary>
    public Item GetItem()
    {
        return storedItem;
    }
}
