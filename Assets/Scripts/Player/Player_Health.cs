using UnityEngine;

public class Player_Health : Entity_Health
{
    private Player player;
    private UI_MiniHealthBar healthBarUI;
    
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        healthBarUI = GetComponentInChildren<UI_MiniHealthBar>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            Die();
    }

    protected override void Die()
    {
        base.Die();
        player.ui.OpenDeathScreenUI();
    }
}
