using UnityEngine;
using System;

public class Object_CheckPoint : MonoBehaviour, ISaveable
{
    [SerializeField] private string checkpointId;
    [SerializeField] private Transform respawnPoint;
    
    public bool isActive { get; private set; }
    private Animator anim;
    private AudioSource fireAudioSource;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        fireAudioSource = GetComponent<AudioSource>();
    }
    
    public Vector3 GetPosition() => respawnPoint == null ? transform.position : respawnPoint.position;

    public string GetCheckpointId() => checkpointId;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(checkpointId))
        {
            checkpointId = Guid.NewGuid().ToString();
        }
#endif
    }

    public void ActivateCheckPoint(bool activate)
    {
        isActive = activate;
        anim.SetBool("isActive", activate);
        if (isActive && fireAudioSource.isPlaying == false)
            fireAudioSource.Play();
        
        if (isActive == false)
            fireAudioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ActivateCheckPoint(true);
    }

    public void LoadData(GameData data)
    {
        bool active = data.unlockedCheckpoints.TryGetValue(checkpointId, out active);
        ActivateCheckPoint(active);
    }

    public void SaveData(ref GameData data)
    {
        if (isActive == false)
            return;

        data.unlockedCheckpoints.TryAdd(checkpointId, true);
    }
}