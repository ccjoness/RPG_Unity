using UnityEngine;

public class UI_Storage : MonoBehaviour
{
    private Inventory_Player inventory;
    private Inventory_Storage storage;
    [SerializeField] private UI_ItemSlotParent inventoryParent;
    [SerializeField] private UI_ItemSlotParent storageParent;
    [SerializeField] private UI_ItemSlotParent materialStashParent;
    
    public void SetupStorage(Inventory_Player _inventory, Inventory_Storage _storage)
    {
        this.inventory = _inventory;
        this.storage = _storage;
        storage.OnInventoryChange += UpdateUI;
        UpdateUI();
        
        UI_StorageSlot[] storageSlots = GetComponentsInChildren<UI_StorageSlot>();
        
        foreach (var slot in storageSlots)
            slot.SetStorage(storage);
    }

    private void UpdateUI()
    {
        inventoryParent.UpdateSlots(inventory.itemList);
        storageParent.UpdateSlots(storage.itemList);
        materialStashParent.UpdateSlots(storage.materialStash);
    }
}