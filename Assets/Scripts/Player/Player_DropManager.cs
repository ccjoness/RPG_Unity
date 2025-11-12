using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player_DropManager : Entity_DropManager
{
    [Header("Player Drop Details")] 
    [Range(0, 100)] 
    [SerializeField] private float changeToLooseItems = 90f;
    private Inventory_Player inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory_Player>();
    }

    public override void DropItems()
    {
        List<Inventory_Item> inventoryCopy = new List<Inventory_Item>(inventory.itemList);
        List<Inventory_EquipmentSlot> equipmentCopy = new List<Inventory_EquipmentSlot>(inventory.equipList);

        foreach (var item in inventoryCopy)
        {
            if (Random.Range(0, 100) < changeToLooseItems)
            {
                CreateItemDrop(item.itemData);
                inventory.RemoveFullStack(item);
            }
        }

        foreach (var equip in equipmentCopy)
        {
            if (Random.Range(0, 100) < changeToLooseItems && equip.HasItem())
            {
                var item = equip.GetEquipedItem();

                CreateItemDrop(item.itemData);
                inventory.UnequipItem(item);
                inventory.RemoveFullStack(item);
            }
        }
    }
}
