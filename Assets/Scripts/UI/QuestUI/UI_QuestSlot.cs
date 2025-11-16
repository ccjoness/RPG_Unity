using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private Image[] rewardQuickPreviewSlots;
    [SerializeField] private Image goldRewardIcon;
    [SerializeField] private Item_DataSO goldItem;

    public QuestDataSO questInSlot { get; private set; }
    private UI_QuestPreview questPreview;

    public void SetupQuestSlot(QuestDataSO questDataSO)
    {
        questPreview = transform.root.GetComponentInChildren<UI_Quest>().GetQuestPreview();

        questInSlot = questDataSO;
        questName.text = questDataSO.questName;

        foreach (var previewIcon in rewardQuickPreviewSlots)
        {
            previewIcon.gameObject.SetActive(false);
        }

        goldRewardIcon.gameObject.SetActive(false);

        for (int i = 0; i < questInSlot.rewardItems.Length; i++)
        {
            if (questDataSO.rewardItems[i] == null || questDataSO.rewardItems[i].itemData == null) continue;

            Image slot = rewardQuickPreviewSlots[i];
            slot.gameObject.SetActive(true);
            slot.sprite = questDataSO.rewardItems[i].itemData.itemIcon;
            slot.GetComponentInChildren<TextMeshProUGUI>().text = questDataSO.rewardItems[i].stackSize.ToString();
        }

        if (questDataSO.goldReward > 0)
        {
            goldRewardIcon.gameObject.SetActive(true);
            goldRewardIcon.GetComponentInChildren<TextMeshProUGUI>().text = questDataSO.goldReward.ToString();
        }
    }

    public void UpdateQuestPreview()
    {
        questPreview.SetupQuestPreview(questInSlot);
    }
}