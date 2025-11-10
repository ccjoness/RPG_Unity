using UnityEngine;
using System;

[DisallowMultipleComponent]
public class UniqueID : MonoBehaviour
{
    // No runtime initializer here — let Unity serialize whatever we set in the Editor.
    [SerializeField, HideInInspector] private string id;
    public string ID => id;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Don’t touch IDs during play.
        if (Application.isPlaying) return;

        // Don’t assign IDs on prefab assets, only on scene instances.
        if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this)) return;

        // Ensure the scene object has a unique, persisted ID.
        if (string.IsNullOrEmpty(id) || HasDuplicateInScene(id))
        {
            AssignNewIdAsInstanceOverride();
        }
    }

    private void AssignNewIdAsInstanceOverride()
    {
        id = Guid.NewGuid().ToString("N"); // compact 32-char form

        // Mark as modified so Unity saves it as a scene override (for prefab instances).
        var so = new UnityEditor.SerializedObject(this);
        var prop = so.FindProperty("id");
        prop.stringValue = id;
        so.ApplyModifiedPropertiesWithoutUndo();

        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(this);
        UnityEditor.EditorUtility.SetDirty(this);
    }

    private bool HasDuplicateInScene(string value)
    {
        // Find all UniqueID components, but only consider scene instances.
        var all = Resources.FindObjectsOfTypeAll<UniqueID>();
        int count = 0;
        foreach (var u in all)
        {
            // Skip prefab assets and hidden editor copies
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(u)) continue;
            if (!u.gameObject.scene.IsValid()) continue;

            if (u.id == value) count++;
            if (count > 1) return true;
        }
        return false;
    }
#endif
}