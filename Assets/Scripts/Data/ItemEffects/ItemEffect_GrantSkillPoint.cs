using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Grant Skill Point", fileName = "Item Effect Data - Grant Skill Point")]
public class ItemEffect_GrantSkillPoint : ItemEffect_DataSO
{
   [SerializeField] private int pointsToAdd;

   private void OnValidate()
   {
      string point = pointsToAdd > 1 ? "Points" : "Point";
      effectDescription = $"Grants {pointsToAdd} Skill {point}";
   }

   public override void ExecuteEffect()
   {
      UI ui = FindFirstObjectByType<UI>();
      ui.skillTreeUI.AddSkillPoints(pointsToAdd);
   }
}
