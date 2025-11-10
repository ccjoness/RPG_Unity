using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
   public int gold;
   public List<Inventory_Item> itemList;
   public SerializableDictionary<string, int> inventory; // saveID | stackSize
   public SerializableDictionary<string, int> storageItems; // saveID | stackSize
   public SerializableDictionary<string, int> storageMaterials; // saveID | stackSize
   
   public SerializableDictionary<string, ItemType> equippedItems; // saveID | slotType

   public int skillPoints;
   public SerializableDictionary<string, bool> skillTreeUI;
   public SerializableDictionary<SkillType, SkillUpgradeType> skillUpgrades;
   
   public GameData()
   {
      inventory = new SerializableDictionary<string, int>();
      storageItems = new SerializableDictionary<string, int>();
      storageMaterials = new SerializableDictionary<string, int>();
      
      equippedItems = new SerializableDictionary<string, ItemType>();
      
      skillTreeUI = new SerializableDictionary<string, bool>();
      skillUpgrades = new SerializableDictionary<SkillType, SkillUpgradeType>();
   }
}
