using TMPro;
using UnityEngine;

public class UI_ActiveQuestPreview : MonoBehaviour
{
    private Player_QuestManager questManager;
    
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private UI_QuestRewardSlot[] questRewardSlots;
    [SerializeField] private UI_QuestRewardSlot goldRewardSlot;
    [SerializeField] private Item_DataSO goldItemData;
    
    public void SetupQuestPreview(QuestData questData)
    {
        questManager = Player.instance.questManager;
        QuestDataSO questSO = questData.questDataSo;
        
        questName.text = questSO.questName;
        description.text = questSO.description;
        
        progress.text = $"{questSO.questGoal}: {questManager.GetQuestProgress(questData)} / {questSO.requiredAmount}";

        foreach (var obj in questRewardSlots)
            obj.gameObject.SetActive(false);
        
        for (int i = 0; i < questSO.rewardItems.Length; i++)
        {
            questRewardSlots[i].gameObject.SetActive(true);
            questRewardSlots[i].UpdateSlot(questSO.rewardItems[i]);
        }
        
        if (questSO.goldReward > 0)
        {
            Inventory_Item gold = new Inventory_Item(goldItemData);
            gold.stackSize = questSO.goldReward;
            goldRewardSlot.gameObject.SetActive(true);
            goldRewardSlot.UpdateSlot(gold);
        }
    }
}
