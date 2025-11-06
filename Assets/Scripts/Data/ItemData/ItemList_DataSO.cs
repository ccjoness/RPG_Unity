using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item List", fileName = "List of items - ")]
public class ItemList_DataSO : ScriptableObject
{
    public Item_DataSO[] itemList;
}
