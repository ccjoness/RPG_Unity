using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Gold", fileName = "Item Effect Data - Gold")]
public class ItemEffect_Gold : ItemEffect_DataSO
{
    [SerializeField] private int goldAmount = 1;
    public override void ExecuteEffect()
    {
        Player.instance.inventory.gold += goldAmount;
    }
}
