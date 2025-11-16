using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private Player player;
    private Inventory_Player inventory;
    private UI_SkillSlot[] skillSlots;

    [Header("Health Bar")]
    [SerializeField] private RectTransform healthRect;
    [SerializeField] private Image healthImageUI;
    [SerializeField] private TextMeshProUGUI healthText;
    
    [Header("Mana Bar")]
    [SerializeField] private RectTransform manaRect;
    [SerializeField] private Image manaImageUI;
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Quick Item Slots")] 
    [SerializeField] private float yOffsetQuickItemParent = 150;
    [SerializeField] private Transform quickItemOptionsParent;
    private UI_QuickItemSlotSelectOption[] quickItemOptions;
    private UI_QuickItemSlot[] quickItemSlots;

    private void Start()
    {
        quickItemSlots = GetComponentsInChildren<UI_QuickItemSlot>();

        player = FindFirstObjectByType<Player>();
        player.health.OnHealthUpdate += UpdateHealthBar;
        player.mana.OnManaUpdate += UpdateManaBar;

        inventory = player.inventory;
        inventory.OnInventoryChange += UpdateQuickSlotsUI;
        inventory.OnQuickSlotUsed += PlayQuickSlotFeedback;
    }

    public void PlayQuickSlotFeedback(int slotNumber) => quickItemSlots[slotNumber].SimulateButtonFeedback();

    public void UpdateQuickSlotsUI()
    {
        Inventory_Item[] quickItems = inventory.quickItems;

        for (int i = 0; i < quickItems.Length; i++)
            quickItemSlots[i].UpdateQuickSlotUI(quickItems[i]);
    }

    public void OpenQuickItemOptions(UI_QuickItemSlot quickItemSlot, RectTransform targetRect)
    {
        if (quickItemOptions == null)
            quickItemOptions = quickItemOptionsParent.GetComponentsInChildren<UI_QuickItemSlotSelectOption>(true);
        
        List<Inventory_Item> consumables = inventory.itemList.FindAll(item => item.itemData.itemType == ItemType.Consumable);

        for (int i = 0; i < quickItemOptions.Length; i++)
        {
            if (i < consumables.Count)
            {
                quickItemOptions[i].gameObject.SetActive(true);
                quickItemOptions[i].SetupOption(quickItemSlot, consumables[i]);
            }
            else
                quickItemOptions[i].gameObject.SetActive(false);
        }

        quickItemOptionsParent.position = targetRect.position + Vector3.up * yOffsetQuickItemParent;
    }

    public void HideQuickItemOptions() => quickItemOptionsParent.position = new Vector3(2191, -286);

    public UI_SkillSlot GetSkillSlot(SkillType skillType)
    {
        if (skillSlots == null)
            skillSlots = GetComponentsInChildren<UI_SkillSlot>(true);

        foreach (var slot in skillSlots)
        {
            if (slot.skillType == skillType)
            {
                slot.gameObject.SetActive(true);
                return slot;
            }
        }

        return null;
    }

    private void UpdateHealthBar()
    {
        float currentHealth = Mathf.RoundToInt(player.health.GetCurrentHealth());
        float maxHealth = player.stats.GetMaxHealth();


        healthText.text = $"{currentHealth}/{maxHealth}";
        healthImageUI.fillAmount = player.health.GetHealthPercent();
    }
    
    private void UpdateManaBar()
    {
        float currentMana = Mathf.RoundToInt(player.mana.GetCurrentMana());
        float maxMana = player.stats.GetMaxMana();


        manaText.text = $"{currentMana}/{maxMana}";
        manaImageUI.fillAmount = player.mana.GetManaPercent();
    }
}