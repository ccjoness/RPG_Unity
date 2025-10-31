using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public Item_DataSO itemData;
    public int stackSize = 1;
    
    
    public ItemModifier[] modifiers {get; private set;}
    
    public Inventory_Item(Item_DataSO itemData)
    {
      this.itemData = itemData;
      modifiers = EquipmentData()?.modifiers;
    }

    public void AddModifiers(Entity_Stats playerStats)
    {
        foreach (var mod in modifiers)
        {
            Stat statToModify = playerStats.GetStatByType(mod.statType);
            statToModify.AddModifier(mod.value, itemData.itemName);
        }
    }

    public void RemoveModifiers(Entity_Stats playerStats)
    {
        foreach (var mod in modifiers)
        {
            Stat statToModify = playerStats.GetStatByType(mod.statType);
            statToModify.RemoveModifier(itemData.itemName);
        }
    }

    private Equipment_DataSO EquipmentData()
    {
        if (itemData is Equipment_DataSO equipment)
            return equipment;
        return null;
    }
    
    public bool CanAddStack() => stackSize < itemData.maxStackSize;
    
    public void AddStack() => stackSize++;
    
    public void RemoveStack() => stackSize--;
}
