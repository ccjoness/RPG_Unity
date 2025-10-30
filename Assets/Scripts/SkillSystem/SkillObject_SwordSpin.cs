using UnityEngine;

public class SkillObject_SwordSpin : SkillObject_Sword
{
   private int maxDistance;
   private float attacksPerSecond;
   private float attackTimer;


   public override void SetupSword(Skill_SwordThrow _swordManager, Vector2 direction)
   {
      base.SetupSword(_swordManager, direction);
      
      anim?.SetTrigger("spin");
      
      maxDistance = _swordManager.maxDistance;
      attacksPerSecond = _swordManager.attacksPerSecond;
      
      Invoke(nameof(GetSwordBackToPlayer), _swordManager.maxSpinDuration);
   }

   protected override void Update()
   {
      HandleAttack();
      HandleStopping();
      HandleComeback();
   }

   private void HandleStopping()
   {
      float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
      if (distanceToPlayer > maxDistance && rb.simulated)
         rb.simulated = false;
   }
   
   private void HandleAttack()
   {
      attackTimer -= Time.deltaTime;

      if (attackTimer < 0)
      {
         DamageEnemiesInRadius(transform, 1);
         attackTimer = 1 /  attacksPerSecond;
      }
   }

   protected override void OnTriggerEnter2D(Collider2D collision)
   {
      rb.simulated = false;
   }
}
