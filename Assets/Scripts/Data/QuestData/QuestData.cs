using System;


[Serializable]
public class QuestData 
{
    public QuestDataSO questDataSo;
    public int currentAmount;
    public bool canGetReward;
    
    public void AddQuestProgress(int amount = 1)
    {
        currentAmount += amount;
        canGetReward = CanGetReward();
    }

    public bool CanGetReward() => currentAmount >= questDataSo.requiredAmount;

    public QuestData(QuestDataSO questSo)
    {
        this.questDataSo = questSo;
    }
}