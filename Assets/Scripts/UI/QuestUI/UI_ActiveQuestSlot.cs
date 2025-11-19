using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActiveQuestSlot : MonoBehaviour
{
    private QuestData questInSlot;
    private UI_ActiveQuestPreview questPreview;
    
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private Image[] questRewardPreview;
    [SerializeField] private Image goldRewardIcon;
    [SerializeField] private Item_DataSO goldItem;
    
    public void SetupActiveQuestSlot(QuestData questToSetup)
    {
        questPreview = transform.root.GetComponentInChildren<UI_ActiveQuestPreview>();
        questInSlot = questToSetup;
        
        questName.text = questToSetup.questDataSo.questName;
        
        Inventory_Item[] reward = questToSetup.questDataSo.rewardItems;
        
        foreach (var previewIcon in questRewardPreview)
        {
            previewIcon.gameObject.SetActive(false);
        }
        
        goldRewardIcon.gameObject.SetActive(false);
        
        for (int i = 0; i < reward.Length; i++)
        {
            if (reward[i] == null || reward[i].itemData == null) continue;
            Image preview = questRewardPreview[i];
            
            preview.gameObject.SetActive(true);
            preview.sprite = reward[i].itemData.itemIcon;
            preview.GetComponentInChildren<TextMeshProUGUI>().text = reward[i].stackSize.ToString();
        }
        
        if (questToSetup.questDataSo.goldReward > 0)
        {
            goldRewardIcon.gameObject.SetActive(true);
            goldRewardIcon.GetComponentInChildren<TextMeshProUGUI>().text = questToSetup.questDataSo.goldReward.ToString();
        }
    }
    
    public void SetupPreview()
    {
        questPreview.SetupQuestPreview(questInSlot);
    }
}
