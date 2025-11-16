using System;
using UnityEngine;
using System.Linq;

public class Entity_SFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("SFX Names")] [SerializeField] private string attackHit;
    [SerializeField] private string attackMiss;
    [SerializeField] private string walkSFX;
    [SerializeField] private string dashSFX;
    [SerializeField] private string jumpSFX;
    [SerializeField] private string landSFX;
    [Space] [SerializeField] private float soundDistance;

    [SerializeField] private bool showGizmo = false;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void PlayJumpSFX()
    {
        // Debug.Log("Playing jump sfx");
        AudioManager.instance.PlaySFX(jumpSFX, audioSource, soundDistance);
    }

    public void PlayLandSFX()
    {
        // Debug.Log("Playing land sfx");
        AudioManager.instance.PlaySFX(landSFX, audioSource, soundDistance);
    }

    public void PlayDashSFX()
    {
        // Debug.Log("Playing dash sfx");
        AudioManager.instance.PlaySFX(dashSFX, audioSource, soundDistance);
    }

    public void PlayStepSFX()
    {
        // Debug.Log("Playing step sfx");
        AudioManager.instance.PlaySFX(walkSFX, audioSource, soundDistance);
    }

    public void PlayAttackHit()
    {
        // Debug.Log("Playing hit sfx");
        AudioManager.instance.PlaySFX(attackHit, audioSource, soundDistance);
    }

    public void PlayAttackMiss()
    {
        AudioManager.instance.PlaySFX(attackMiss, audioSource, soundDistance);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, soundDistance);
        }
    }
}