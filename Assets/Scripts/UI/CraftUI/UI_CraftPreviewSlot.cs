using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftPreviewSlot : MonoBehaviour
{
    [SerializeField] private Image previewIcon;
    [SerializeField] private TextMeshProUGUI previewNameValue;

    public void SetupPreviewSlot(Item_DataSO itemData, int availableAmount, int requiredAmount)
    {
        previewIcon.sprite = itemData.itemIcon;
        previewNameValue.text = $"{itemData.itemName} - {availableAmount}/{requiredAmount}";
    }
}
