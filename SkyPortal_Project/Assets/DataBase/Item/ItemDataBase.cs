using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( menuName = "ScriptableObjects/アイテムデータベース", fileName = "ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<ItemParameters> itemList = new List<ItemParameters>(); // アイテムリスト
}
