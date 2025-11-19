using System.Collections.Generic;
using UnityEngine;

public class Player_QuestManager : MonoBehaviour, ISaveable
{
    public List<QuestData> activeQuests;
    public List<QuestData> completedQuests;

    private Entity_DropManager dropManager;
    private Inventory_Player inventory;

    [Header("Quest Database")]
    [SerializeField] private QuestDatabaseSO questDatabase;

    private void Awake()
    {
        dropManager = GetComponent<Entity_DropManager>();
        inventory = GetComponent<Inventory_Player>();
    }
    
    public void TryGetRewardFrom(RewardType npcType)
    {
        List<QuestData> getRewardQuests = new List<QuestData>();

        foreach (var quest in activeQuests)
        {
            // DELIVER ITEMS IF CAN
            if (quest.questDataSo.questType == QuestType.Delivery)
            {
                var requiredItem = quest.questDataSo.itemToDeliver;
                var requiredAmount = quest.questDataSo.requiredAmount;
                
                if (inventory.HasItemAmount(requiredItem, requiredAmount))
                {
                    inventory.RemoveItemAmount(requiredItem, requiredAmount);
                    quest.AddQuestProgress(requiredAmount);
                }
            }
            
            if (quest.CanGetReward() && quest.questDataSo.rewardType == npcType)
                getRewardQuests.Add(quest);
        }
        
        foreach (var quest in getRewardQuests)
        {
            GiveQuestReward(quest.questDataSo);
            CompleteQuest(quest);
        }
    }

    private void GiveQuestReward(QuestDataSO questDataSo)
    {
        if (questDataSo.goldReward > 0)
        {
            Player.instance.inventory.gold += questDataSo.goldReward;
        }

        foreach (var item in questDataSo.rewardItems)
        {
            if (item == null || item.itemData == null) continue;

            for (int i = 0; i < item.stackSize; i++)
            {
                dropManager.CreateItemDrop(item.itemData);
            }
        }
    }

    public bool HasCompletedQuest()
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            QuestData quest = activeQuests[i];

            if (quest.questDataSo.questType == QuestType.Delivery)
            {
                var requiredItem = quest.questDataSo.itemToDeliver;
                var requiredAmount = quest.questDataSo.requiredAmount;

                if (inventory.HasItemAmount(requiredItem, requiredAmount))
                    return true;
            }

            if (quest.CanGetReward())
                return true;
        }

        return false;
    }

    public void AddProgress(string questTargetId, int amount = 1)
    {
        List<QuestData> getRewardQuests = new List<QuestData>();

        foreach (var quest in activeQuests)
        {
            if (quest.questDataSo.questTargetId != questTargetId)
                continue;

            if(quest.CanGetReward() == false)
                quest.AddQuestProgress(amount);
            
            if (quest.questDataSo.rewardType == RewardType.None && quest.CanGetReward()) 
                getRewardQuests.Add(quest);
        }
        
        foreach (var quest in getRewardQuests)
        {
            GiveQuestReward(quest.questDataSo);
            CompleteQuest(quest);
        }
    }

    public int GetQuestProgress(QuestData questToCheck)
    {
        QuestData quest = activeQuests.Find(q => q == questToCheck);
        return quest != null ? quest.currentAmount : 0;
    }
    
    public void AcceptQuest(QuestDataSO questDataSo)
    {
        activeQuests.Add(new QuestData(questDataSo));
    }

    public void CompleteQuest(QuestData questData)
    {
        completedQuests.Add(questData);
        activeQuests.Remove(questData);
    }

    public bool QuestIsActive(QuestDataSO questToCheck)
    {
        if(questToCheck == null)
            return false;

        return activeQuests.Find(q => q.questDataSo == questToCheck) != null;
    }

    public void LoadData(GameData data)
    {
        activeQuests.Clear();

        foreach (var entry in data.activeQuests)
        {
            string questSaveId = entry.Key;
            int progress = entry.Value;

            QuestDataSO questDataSO = questDatabase.GetQuestById(questSaveId);

            if (questDataSO == null)
            {
                Debug.Log(questSaveId + " was not found in the database!");
                continue;
            }
            
            QuestData questToLoad = new QuestData(questDataSO);
            questToLoad.currentAmount = progress;

            activeQuests.Add(questToLoad);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.activeQuests.Clear();
        
        foreach (var quest in activeQuests)
        {
            data.activeQuests.Add(quest.questDataSo.questSaveId, quest.currentAmount);
        }

        foreach (var quest in completedQuests)
        {
            data.completedQuests.Add(quest.questDataSo.questSaveId, true);
        }
    }
}