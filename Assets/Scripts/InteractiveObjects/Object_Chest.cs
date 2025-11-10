using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Object_Chest : MonoBehaviour, IDamageable
{

    private UniqueID idComponent;
    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();
    private Entity_DropManager dropManager => GetComponent<Entity_DropManager>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;
    [SerializeField] private bool canDropItems = true;


    private void Awake()
    {
        idComponent = GetComponent<UniqueID>();
    }
    
    public string GetID() => idComponent.ID;

    public bool ChestOpen() => !canDropItems;

    public void SetStateFromSave(bool chestOpen)
    {
        if (chestOpen)
        {
            this.canDropItems = !chestOpen;
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
}
