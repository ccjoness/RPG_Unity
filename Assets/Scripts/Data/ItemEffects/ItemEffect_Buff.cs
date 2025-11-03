using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Buff Effect", fileName = "Item Effect Data - Buff")]
public class ItemEffect_Buff : ItemEffect_DataSO
{
   [SerializeField] private BuffEffect_Data[] buffsToApply;
   [SerializeField] private float duration;
   [SerializeField] private string source = Guid.NewGuid().ToString();
   
   Player_Stats playerStats;

   public override bool CanBeUsed()
   {
      if (playerStats == null)
         playerStats = FindFirstObjectByType<Player_Stats>();
      
      if (playerStats.CanApplyBuffOf(source))
         return true;
      else
      {
         Debug.Log("Same buff effect cannot be used twice.");
         return false;
      }
      
      
   }

   public override void ExecuteEffect()
   {
      playerStats.ApplyBuff(buffsToApply, duration, source);
   }
}
