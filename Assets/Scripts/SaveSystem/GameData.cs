using System.Collections.Generic;
using System;
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

    public SerializableDictionary<string, bool> unlockedCheckpoints; // checkpoint id | unlocked status
    public SerializableDictionary<string, Vector3> inScenePortals; // scene name | portal position
    
    public SerializableDictionary<string, bool> chests; // chest id | is opened
    
    public string portalDestinationSceneName;
    public bool returningFromTown;

    public string lastScenePlayed;
    public Vector3 lastPlayerPosition;

    public GameData()
    {
        inventory = new SerializableDictionary<string, int>();
        storageItems = new SerializableDictionary<string, int>();
        storageMaterials = new SerializableDictionary<string, int>();

        equippedItems = new SerializableDictionary<string, ItemType>();

        skillTreeUI = new SerializableDictionary<string, bool>();
        skillUpgrades = new SerializableDictionary<SkillType, SkillUpgradeType>();

        chests = new SerializableDictionary<string, bool>();

        unlockedCheckpoints = new SerializableDictionary<string, bool>();
        inScenePortals = new SerializableDictionary<string, Vector3>();
    }
}
