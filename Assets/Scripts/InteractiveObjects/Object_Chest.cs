using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Object_Chest : MonoBehaviour, IDamageable, ISaveable
{

    [SerializeField] private string chestId;
    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();
    private Entity_DropManager dropManager => GetComponent<Entity_DropManager>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;
    [SerializeField] private bool canDropItems = true;
    
    private void OnValidate()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(chestId))
        {
            chestId = Guid.NewGuid().ToString();
        }
#endif
    }

    public void SetStateFromSave(bool chestOpen)
    {
        if (chestOpen)
        {
            this.canDropItems = false;
            anim.SetBool("chestOpen", true);
        }
        
    }
    
    public bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (canDropItems == false)
            return false;
        
        canDropItems = false;
        dropManager?.DropItems();
        
        fx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200f, 200f);
        
        return true;
    }

    public void LoadData(GameData data)
    {
        bool open = data.chests.TryGetValue(chestId, out open);
        SetStateFromSave(open);
    }
    
    public void SaveData(ref GameData data)
    {
        if (canDropItems)
            return;

        data.chests.TryAdd(chestId, true);
    }
}
