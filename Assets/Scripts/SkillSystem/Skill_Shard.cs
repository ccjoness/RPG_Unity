using System.Collections;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;
    private Entity_Health playerHealth;
    
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonationTime = 2;
    
    [Header("Moving Shard Upgrade")]
    [SerializeField] private float shardSpeed = 7;

    [Header("Multicast Shard Upgrade")] 
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private int currentCharges;
    [SerializeField] private bool isRecharging;
    
    [Header("Teleport Shard Upgrade")] 
    [SerializeField] private float shardExistDuration = 10;
    
    [Header("Health Rewind Shard Upgrade")] 
    [SerializeField] private float savedHealthPercent;


    protected override void Awake()
    {
        base.Awake();
        currentCharges = maxCharges;
        playerHealth = GetComponentInParent<Entity_Health>();
    }
    
    public void CreateShard()
    {
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentShard = shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(this);
        if (Unlocked(SkillUpgradeType.Shard_Teleport) || Unlocked(SkillUpgradeType.Shard_TeleportHpRewind))
            currentShard.OnExplode += ForceCooldown;
    }

    public void CreateRawShard(Transform target = null, bool shardsCanMove = false)
    {
        bool canMove = shardsCanMove != false || (Unlocked(SkillUpgradeType.Shard_MoveToEnemy) ||  Unlocked(SkillUpgradeType.Shard_Multicast));
        
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        shard.GetComponent<SkillObject_Shard>().SetupShard(this, detonationTime, canMove, shardSpeed, target);
    }

    public void CreateDomainShard(Transform target)
    {
        
    }

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
            return;

        if (Unlocked(SkillUpgradeType.Shard))
            HandleShardRegular();

        if (Unlocked(SkillUpgradeType.Shard_MoveToEnemy))
            HandleShardMovement();

        if (Unlocked(SkillUpgradeType.Shard_Multicast))
            HandleShardMulticast();
        
        if (Unlocked(SkillUpgradeType.Shard_Teleport))
            HandleShardTeleport();

        if (Unlocked(SkillUpgradeType.Shard_TeleportHpRewind))
            HandleShardHealthRewind();
    }

    private void HandleShardHealthRewind()
    {
        if (currentShard == null)
        {
            CreateShard();
            savedHealthPercent = playerHealth.GetHealthPercent();
        }
        else
        {
            SwapPlayerAndShard();
            playerHealth.SetHealthToPercent(savedHealthPercent);
            SetSkillOnCooldown();
        }
    }
    
    private void HandleShardTeleport()
    {
        if (currentShard == null)
        {
            CreateShard();
        }
        else
        {
            SwapPlayerAndShard();
            SetSkillOnCooldown();
        }
    }

    public void SwapPlayerAndShard()
    {
        Vector3 shardPosition = currentShard.transform.position;
        Vector3 playerPosition = player.transform.position;
        
        currentShard.transform.position = playerPosition;
        currentShard.Explode();
        
        player.TeleportPlayer(shardPosition);
    }
    
    private void HandleShardMulticast()
    {
        if (currentCharges <= 0)
            return;
        
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        currentCharges--;

        if (isRecharging == false)
            StartCoroutine(ShardRechargeCo());
    }

    private IEnumerator ShardRechargeCo()
    {
        isRecharging = true;

        while (currentCharges < maxCharges)
        {
            
            yield return new WaitForSeconds(cooldown);
            currentCharges++;
        }
        isRecharging = false;
    }

    private void HandleShardMovement()
    {
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        
        SetSkillOnCooldown();
    }

    private void HandleShardRegular()
    {
        CreateShard();
        SetSkillOnCooldown();
    }

    public float GetDetonationTime()
    {
        if (Unlocked(SkillUpgradeType.Shard_Teleport) || Unlocked(SkillUpgradeType.Shard_TeleportHpRewind))
            return shardExistDuration;
        return detonationTime;
    }

    private void ForceCooldown()
    {
        if (OnCooldown() == false)
        {
            SetSkillOnCooldown();
            currentShard.OnExplode -= ForceCooldown;
        }
    }
}