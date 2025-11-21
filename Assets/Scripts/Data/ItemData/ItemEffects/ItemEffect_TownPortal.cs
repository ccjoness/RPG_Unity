using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Town Portal", fileName = "Item Effect Data - Portal to Town")]
public class ItemEffect_TownPortal : ItemEffect_DataSO
{
    public override void ExecuteEffect()
    {
        if (SceneManager.GetActiveScene().name == Object_TownPortal.instance.townSceneName)
        {
            Debug.Log("Cannot open portal in town!");
            return;
        }

        Player player = Player.instance;
        Vector3 portalPosition = player.transform.position + new Vector3(player.facingDir * 1.5f, 0);
        
        Object_TownPortal.instance.ActivatePortal(portalPosition, player.facingDir);
    }
}
