using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Entity_Mana : MonoBehaviour
{
    public event Action OnManaUpdate;
    
    private Slider manaBar;
    private Entity entity;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;

    private UI_MiniManaBar manaBarUI;
    private bool miniManaBarActive;
    [SerializeField] protected float currentMana;
    
    [Header("Mana Regeneration")]
    [SerializeField] private float regenInterval = 1;
    [SerializeField] private bool canRegenerateMana = true;

    private bool isDead;
    

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityStats = GetComponent<Entity_Stats>();
        entityHealth = GetComponent<Entity_Health>();
        manaBarUI = GetComponentInChildren<UI_MiniManaBar>();
        manaBar = manaBarUI.GetComponentInChildren<Slider>();
        isDead = entityHealth.isDead;
         
        SetupMana();
    }
    
    protected virtual void Start()
    {
    }

    private void SetupMana()
    {
        if (entityStats == null)
            return;

        currentMana = entityStats.GetMaxMana();
        OnManaUpdate += UpdateManaBar;

        UpdateManaBar();
        InvokeRepeating(nameof(RegenerateMana), 0, regenInterval);
    }

    
    private void RegenerateMana()
    {
        if (canRegenerateMana == false)
            return;

        float regenAmount = entityStats.resources.manaRegen.GetValue();
        IncreaseMana(regenAmount);
    }

    public void IncreaseMana(float manaAmount)
    {
        if (isDead)
            return;

        float newMana = currentMana + manaAmount;
        float maxMana = entityStats.GetMaxMana();

        currentMana = Mathf.Min(newMana, maxMana);
        OnManaUpdate?.Invoke();
    }

    public void ReduceMana(float damage)
    {
        currentMana -= damage;
        OnManaUpdate?.Invoke();
    }

    public float GetManaPercent() => currentMana / entityStats.GetMaxMana();

    public void SetManaToPercent(float percent)
    {
        currentMana = entityStats.GetMaxMana() * Mathf.Clamp01(percent);
        OnManaUpdate?.Invoke();
    }
    
    public float GetCurrentMana() => currentMana;

    private void UpdateManaBar()
    {
        if (manaBar == null && manaBar.transform.parent.gameObject.activeSelf == false)
            return;

        manaBar.value = currentMana / entityStats.GetMaxMana();
    }
    
    public void EnableManaBar(bool enable) => manaBar?.transform.parent.gameObject.SetActive(enable);
    
}
