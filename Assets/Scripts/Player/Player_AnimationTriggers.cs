using UnityEngine;

public class Player_AnimationTriggers : Entity_AnimationTriggers
{
    private Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponentInParent<Player>();
    }

    private void ThrowSword() => player.skillManager.swordThrow.ThrowSword();
    
    private void PlayStepSFX()
    {
        player.sfx?.PlayStepSFX();
    }
 
    private void PlayDashSFX() => player.sfx?.PlayDashSFX();
}
