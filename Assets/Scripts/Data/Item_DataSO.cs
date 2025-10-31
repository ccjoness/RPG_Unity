using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Material item", fileName = "Material Data - ")]
public class Item_DataSO : ScriptableObject
{
   public string itemName;
   public Sprite itemIcon;
   public ItemType itemType;
   public int maxStackSize = 1;
}
