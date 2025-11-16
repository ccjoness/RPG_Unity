using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio/Audio Database")]
public class AudioDatabaseSO : ScriptableObject
{
    public List<AudioClipData> player;
    public List<AudioClipData> enemySkeleton;
    public List<AudioClipData> uiAudio;
    
    [Header("Music List")]
    public List<AudioClipData> mainMenuMusic;
    public List<AudioClipData> levelMusic;
    
    private Dictionary<string, AudioClipData> clipCollection;

    private void OnEnable()
    {
        clipCollection = new Dictionary<string, AudioClipData>();
        
        AddToCollection(player);
        AddToCollection(enemySkeleton);
        AddToCollection(uiAudio);
        AddToCollection(mainMenuMusic);
        AddToCollection(levelMusic);
    }
    
    public AudioClipData Get(string groupName) => clipCollection.TryGetValue(groupName, out var data) ? data : null;

    private void AddToCollection(List<AudioClipData> listToAdd)
    {
        foreach (var data in listToAdd)
        {
            if (data != null)
            {
                clipCollection.TryAdd(data.audioName, data);
            }
        }
    }
}

[System.Serializable]
public class AudioClipData
{
    public string audioName;
    public List<AudioClip> clips = new List<AudioClip>();
    [Range(0f, 1f)] public float maxVolume = 1f;
    
    public AudioClip GetRandomClip() => clips is not { Count: > 0 } ? null : clips[Random.Range(0, clips.Count)];

}