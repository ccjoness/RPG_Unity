using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    public static GameManager instance;
    private Vector3 lastPlayerPosition;

    private string lastScenePlayed;
    private bool dataLoaded;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // public void SetLastPlayerPosition(Vector3 position) => lastPlayerPosition = position;
    
    public void ContinueGame() => ChangeScene(lastScenePlayed, RespawnType.NonSpecific);

    public void RestartScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        ChangeScene(sceneName, RespawnType.NonSpecific);
    }

    public void ChangeScene(string sceneName, RespawnType respawnType)
    {
        SaveManager.instance.SaveGame();
        Time.timeScale = 1;
        StartCoroutine(ChangeSceneCo(sceneName, respawnType));
    }

    private IEnumerator ChangeSceneCo(string sceneName, RespawnType respawnType)
    {
        UI_FadeScreen fadeScreen = FindFadeScreenUI();

        fadeScreen.FadeOut();
        yield return fadeScreen.fadeEffectCo;

        SceneManager.LoadScene(sceneName);

        dataLoaded = false;
        yield return null;
        
        while (dataLoaded == false)
            yield return null;
        
        fadeScreen = FindFadeScreenUI();
        fadeScreen.FadeIn();
        
        Player player = Player.instance;
        
        if (player == null)
            yield break;
        
        Vector3 position = GetNewPlayerPosition(respawnType);
        
        if (position != Vector3.zero)
            player.TeleportPlayer(position);
    }
    
    private UI_FadeScreen FindFadeScreenUI() => UI.instance != null ? UI.instance.fadeScreenUI : FindFirstObjectByType<UI_FadeScreen>(FindObjectsInactive.Include);
    

    private Vector3 GetNewPlayerPosition(RespawnType type)
    {
        if (type == RespawnType.Portal)
        {
            Object_TownPortal portal = Object_TownPortal.instance;
            
            Vector3 position = portal.GetPosition();
            
            portal.SetTrigger(false);
            portal.DisableIfNeeded();

            return position;
        }
        
        if (type == RespawnType.NonSpecific)
        {
            var data = SaveManager.instance.GetGameData();
            var checkpoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
            var unlockedCheckpoints = checkpoints
                .Where(cp => data.unlockedCheckpoints.TryGetValue(cp.GetCheckpointId(), out bool unlocked) && unlocked)
                .Select(cp => cp.GetPosition())
                .ToList();

            var enterWaypoints = FindObjectsByType<Object_Waypoint>(FindObjectsSortMode.None)
                .Where(wp => wp.GetWaypointType() == RespawnType.Enter)
                .Select(wp => wp.GetPositionAndSetTriggerFalse())
                .ToList();

            var selectedPositions = unlockedCheckpoints.Concat(enterWaypoints).ToList();
            
            if (selectedPositions.Count == 0)
                return Vector3.zero;
            return selectedPositions
                .OrderBy(position => Vector3.Distance(position, lastPlayerPosition))
                .First();
        }

        return GetWaypointPosition(type);
    }

    private Vector3 GetWaypointPosition(RespawnType type)
    {
        var waypoints = FindObjectsByType<Object_Waypoint>(FindObjectsSortMode.None);

        foreach (var point in waypoints)
        {
            if (point.GetWaypointType() == type)
                return point.GetPositionAndSetTriggerFalse();
        }

        return Vector3.zero;
    }

    public void LoadData(GameData data)
    {
        lastScenePlayed = data.lastScenePlayed;
        lastPlayerPosition = data.lastPlayerPosition;
        
        if (string.IsNullOrEmpty(lastScenePlayed))
            lastScenePlayed = "Level_0";
        
        dataLoaded = true;
    }

    public void SaveData(ref GameData data)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MainMenu")
            return;

        data.lastPlayerPosition = Player.instance.transform.position;
        data.lastScenePlayed = currentScene;
        dataLoaded = false;
    }
}
