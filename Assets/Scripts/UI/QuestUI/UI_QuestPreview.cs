using TMPro;
using UnityEngine;

public class UI_QuestPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questGoal;
    [SerializeField] private UI_QuestRewardSlot[] questRewards;
    [SerializeField] private UI_QuestRewardSlot goldRewardSlot;
    [SerializeField] private Item_DataSO goldItemData;

    [SerializeField] private GameObject[] additionalObjects;
    private UI_Quest questUI;
    private QuestDataSO previewQuest;

    public void SetupQuestPreview(QuestDataSO questDataSO)
    {
        questUI = transform.root.GetComponentInChildren<UI_Quest>();
        previewQuest = questDataSO;
        
        EnableAdditionalObjects(true);
        EnableQuestRewardObjects(false);

        questName.text = questDataSO.questName;
        questDescription.text = questDataSO.description;
        questGoal.text = $"{questDataSO.questGoal}: {questDataSO.requiredAmount}";
        
        for (int i = 0; i < questDataSO.rewardItems.Length; i++)
        {
            Inventory_Item rewardItem = new Inventory_Item(questDataSO.rewardItems[i].itemData);
            rewardItem.stackSize = questDataSO.rewardItems[i].stackSize;
            questRewards[i].gameObject.SetActive(true);
            questRewards[i].UpdateSlot(rewardItem);
        }
        if (questDataSO.goldReward > 0)
        {
            Inventory_Item gold = new Inventory_Item(goldItemData);
            gold.stackSize = questDataSO.goldReward;
            goldRewardSlot.gameObject.SetActive(true);
            goldRewardSlot.UpdateSlot(gold);
        }
    }
    
    public void AcceptQuestButton()
    {
        MakeQuestPreviewEmpty();
        
        questUI.questManager.AcceptQuest(previewQuest);
        questUI.UpdateQuestList();
    }
    
    public void MakeQuestPreviewEmpty()
    {
        questName.text = "";
        questDescription.text = "";
        questGoal.text = "";
        
        EnableAdditionalObjects(false);
        EnableQuestRewardObjects(false);
    }
    
    private void EnableAdditionalObjects(bool enable)
    {
        foreach (var obj in additionalObjects)
            obj.SetActive(enable);
    }
    
    private void EnableQuestRewardObjects(bool enable)
    {
        foreach (var reward in questRewards)
            reward.gameObject.SetActive(enable);
        goldRewardSlot.gameObject.SetActive(enable);
    }
}
