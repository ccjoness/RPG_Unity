using System;
using UnityEngine;

public class SkillObject_Sword : SkillObject_Base
{
    protected Skill_SwordThrow swordManager;

    protected Transform playerTransform;
    protected bool shouldComeback;
    protected float comebackSpeed = 20;
    protected float maxAllowedDistance = 25;
    
    protected virtual void Update()
    {
        transform.right = rb.linearVelocity;
        HandleComeback();
    }

    public virtual void SetupSword(Skill_SwordThrow _swordManager, Vector2 direction)
    {
        rb.linearVelocity = direction;
        
        this.swordManager = _swordManager;

        playerTransform = swordManager.transform.root;
        playerStats = swordManager.player.stats;
        damageScaleData = swordManager.damageScaleData;
    }
    
    public void GetSwordBackToPlayer() => shouldComeback = true;
    
    protected void HandleComeback()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        
        if (distance > maxAllowedDistance)
            GetSwordBackToPlayer();
        
        if (shouldComeback == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position, comebackSpeed * Time.deltaTime);
        if (distance < .5f)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        StopSword(collision);
        DamageEnemiesInRadius(transform, 1);
    }

    protected virtual void StopSword(Collider2D collision)
    {
        rb.simulated = false;
        transform.parent = collision.transform;
    }
}
