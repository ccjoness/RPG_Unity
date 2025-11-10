using System;
using UnityEngine;

public class Object_CheckPoint : MonoBehaviour, ISaveable
{
    private Object_CheckPoint[] allCheckPoints;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        allCheckPoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
    }

    public void ActivateCheckPoint(bool active)
    {
        anim.SetBool("isActive", active);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var point in allCheckPoints)
            point.ActivateCheckPoint(false);

        SaveManager.instance.GetGameData().savedCheckPoint = transform.position;
        ActivateCheckPoint(true);

    }

    public void LoadData(GameData data)
    {
        bool active = data.savedCheckPoint == transform.position;
        ActivateCheckPoint(active);
        if(active)
            Player.instance.TeleportPlayer(transform.position);
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
