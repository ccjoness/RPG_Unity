using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillToolTip;
    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    
    public UI_SkillTree skillTree;
    private bool skillTreeEnabled;

    private void Awake()
    {
        itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
        skillToolTip = GetComponentInChildren<UI_SkillToolTip>();
        statToolTip = GetComponentInChildren<UI_StatToolTip>();
        
        skillTree =  GetComponentInChildren<UI_SkillTree>(true);
    }

    public void ToggleSkillTreeUI()
    {
        skillTreeEnabled = !skillTreeEnabled;
        Time.timeScale = skillTreeEnabled ? 0f : 1f;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillToolTip.ShowToolTip(false, null);
    }
}
