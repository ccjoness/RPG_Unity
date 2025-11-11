using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Buff Effect", fileName = "Item Effect Data - Buff")]
public class ItemEffect_Buff : ItemEffect_DataSO
{
   [SerializeField] private BuffEffect_Data[] buffsToApply;
   [SerializeField] private float duration;
   [SerializeField] private string source = Guid.NewGuid().ToString();
   

   public override bool CanBeUsed(Player player)
   {

      
      if (player.stats.CanApplyBuffOf(source))
      {
         this.player = player;
         return true;
      }
      else
      {
         Debug.Log("Same buff effect cannot be used twice.");
         return false;
      }
      
      
   }

   public override void ExecuteEffect()
   {
      player.stats.ApplyBuff(buffsToApply, duration, source);
      player = null;
   }
}
