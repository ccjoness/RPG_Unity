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
}
