using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_QuestManager : MonoBehaviour
{
    public List<QuestData> activeQuests;
    public List<QuestData> completedQuests;
    
    private Entity_DropManager dropManager;

    private void Awake()
    {
        dropManager = GetComponent<Entity_DropManager>();
    }
    
    public void TryGiveRewardFrom(RewardType npcType)
    {
        List<QuestData> getRewardQuests = new List<QuestData>();
        
        foreach (var quest in activeQuests)
        {
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
    
    public void AddProgress(string questTargetId, int amount = 1)
    {
        List<QuestData> getRewardQuests = new List<QuestData>();
        foreach (var quest in activeQuests)
        {
            if (quest.questDataSo.questTargetId != questTargetId)
                continue;
            
            quest.AddQuestProgress(amount);
            
            if (quest.questDataSo.rewardType == RewardType.None && quest.CanGetReward())
            {
                getRewardQuests.Add(quest);
            }
        }
        
        foreach (var quest in getRewardQuests)
        {
            GiveQuestReward(quest.questDataSo);
            CompleteQuest(quest);
        }
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
        if (questToCheck == null) return false;
        
        return activeQuests.Find(q => q.questDataSo == questToCheck) != null;
    }
}