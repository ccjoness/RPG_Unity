using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    protected SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = .2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;
    private Coroutine playStatusEffectCoroutine;

    [Header("On Doing Damage VFX")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private Color critHitVfxColor = Color.white;
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private GameObject critHitVfx;
    
    [Header("Element Colors")]
    [SerializeField] private Color burnVfx = new(1f, 0.5f, 0.07f);
    [SerializeField] private Color chillVfx = Color.cyan;
    [SerializeField] private Color shockVfx = Color.yellow;
    private Color originalHitVfxColor;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
        originalHitVfxColor = hitVfxColor;
    }

    public void PlayOnStatusVfx(float duration, ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                StartCoroutine(PlayStatusVfxCo(duration, burnVfx));
                break;
            case ElementType.Ice:
                StartCoroutine(PlayStatusVfxCo(duration, chillVfx));
                break;
            case ElementType.Lightning:
                StartCoroutine(PlayStatusVfxCo(duration, shockVfx));
                break;
        }
    }

    public void StopAllVfx()
    {
        StopAllCoroutines();
        sr.color = Color.white;
        sr.material = originalMaterial;

    }
    
    private IEnumerator PlayStatusVfxCo(float duration, Color effectColor)
    {
        float tickInterval = .15f;
        float timeHasPassed = 0;

        Color lightColor = effectColor * 1.2f;
        Color darkColor = effectColor * .9f;
        
        bool toggle = false;

        while (timeHasPassed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;
            yield return new WaitForSeconds(tickInterval);
            timeHasPassed += tickInterval;
        }
        
        sr.color = Color.white;
    }
    
    public void CreateOnHitVfx(Transform target, bool isCrit, ElementType element)
    {
        GameObject hitPrefab = isCrit ? critHitVfx : hitVfx;
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = GetElementColor(element, isCrit);            
        
        if (entity.facingDir == -1 && isCrit )
            vfx.transform.Rotate(0, 180, 0);
    }

    public Color GetElementColor(ElementType element, bool isCrit)
    {
        switch (element)
        {
            case ElementType.Fire:
                return burnVfx;
            case ElementType.Ice:
                return chillVfx;
            case ElementType.Lightning:
                return shockVfx;
            case ElementType.None when isCrit:
                return critHitVfxColor;
            default:
                return originalHitVfxColor;
        }
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine);

        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo()
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }
}
