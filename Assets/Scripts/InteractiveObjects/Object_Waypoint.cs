using TMPro;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.SceneManagement;
using ColorUtility = UnityEngine.ColorUtility;


public class Object_Waypoint : MonoBehaviour
{
    public enum PortalType
    {
        PurplePortal,
        GreenPortal,
        RedPortal,
        OrangePortal,
        None
    }

    [SerializeField] private string transferToScene;
    public PortalType portalType;
    [SerializeField] private TextMeshPro waypointText;
    [Space]
    [SerializeField] private RespawnType waypointType;
    [SerializeField] private RespawnType connectedWaypoint;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private bool canBeTriggered = true;
    private SpriteRenderer sr => GetComponentInChildren<SpriteRenderer>();

    public RespawnType GetWaypointType() => waypointType;
    
    public Vector3 GetPositionAndSetTriggerFalse() 
    {
        canBeTriggered = false;
        return respawnPoint == null ? transform.position : respawnPoint.position;
    }
    
    private void Awake()
    {
        GetWaypointColor();
        SetWayPointText();
    }

    private void SetWayPointText()
    {
        if (waypointText == null)
            return;
        waypointText.text = $"To {transferToScene.Replace("_", " ")}";
    }
    private void GetWaypointColor()
    {
        string hexColor;
        switch (portalType)
        {
            case PortalType.PurplePortal: hexColor = "#EF96FF"; break;
            case PortalType.GreenPortal: hexColor = "#96FF9A"; break;
            case PortalType.RedPortal: hexColor = "#FC325B"; break;
            case PortalType.OrangePortal: hexColor = "#FF9E00"; break;
            case PortalType.None:
            default: hexColor = "#FFFFFF"; break;
        }
        ColorUtility.TryParseHtmlString(hexColor, out Color color);
        sr.color = color;
    }

    private void OnValidate()
    {
        gameObject.name = $"Object_Waypoint - {waypointType.ToString()} - {transferToScene}";

        if (waypointType == RespawnType.Enter)
            connectedWaypoint = RespawnType.Exit;

        if (waypointType == RespawnType.Exit)
            connectedWaypoint = RespawnType.Enter;
        
        GetWaypointColor();
        SetWayPointText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBeTriggered == false)
            return;

        GameManager.instance.ChangeScene(transferToScene, connectedWaypoint);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canBeTriggered = true;
    }
}