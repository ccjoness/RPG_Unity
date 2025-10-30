using System;
using UnityEngine;

public class SkillObject_DomainExpansion : SkillObject_Base
{
    private Skill_DomainExpansion domainManager;
    
    private float expandSpeed;
    private float duration;
    
    private float slowdownPercent;
    
    private Vector3 targetScale;
    private bool isShrinking;

    public void SetupDomain(Skill_DomainExpansion _domainManager)
    {
        this.domainManager = _domainManager;
        
        duration = domainManager.GetDomainDuration();
        slowdownPercent = domainManager.GetSlowPercentage();
        expandSpeed =  domainManager.expandSpeed;
        float maxSize = domainManager.maxDomainSize;

        targetScale = Vector3.one * maxSize;
        Invoke(nameof(ShrinkDomain), duration);
    }

    private void Update()
    {
        HandleScaling();
    }

    private void HandleScaling()
    {
        float sizeDifference = Mathf.Abs(transform.localScale.x - targetScale.x);
        bool shouldChangeScale = sizeDifference > .1f;
        
        if (shouldChangeScale)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, expandSpeed * Time.deltaTime);
        
        if (isShrinking && sizeDifference < .1f)
            TerminateDomain();
    }

    private void TerminateDomain()
    {
        domainManager.ClearTargets();
        Destroy(gameObject);
    }

    private void ShrinkDomain()
    {
        targetScale = Vector3.zero;
        isShrinking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null)
            return;
        domainManager.AddTarget(enemy);
        enemy.SlowDownEntity(duration, slowdownPercent, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null)
            return;
        
        enemy.StopSlowdown();
    }
}