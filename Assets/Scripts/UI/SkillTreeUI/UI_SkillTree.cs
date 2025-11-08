using System;
using TMPro;
using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField] private int skillPoints;
    [SerializeField] private TextMeshProUGUI skillPointsText;
    [SerializeField] private UI_TreeConnectHandler[] parentNodes;
    private UI_TreeNode[] allTreeNodes;
    public Player_SkillManager skillManager { get ; private set; }

    public void UnlockDefaultSkills()
    {
        allTreeNodes = GetComponentsInChildren<UI_TreeNode>(true);
        skillManager = FindAnyObjectByType<Player_SkillManager>();
        
        foreach (var node in allTreeNodes)
            node.UnlockDefaultSkill();
    }

    private void Start()
    {
        UpdateAllConnections();
        UpdateSkillPointsText();
    }
    
    [ContextMenu("Reset Skill Tree")]
    public void RefundAllSkills()
    {
        UI_TreeNode[] skillNodes = GetComponentsInChildren<UI_TreeNode>();

        foreach (var node in skillNodes)
            node.Refund();
    }
    
    private void UpdateSkillPointsText() => skillPointsText.text = $"{skillPoints}";
    public bool EnoughSkillPoints(int cost) => skillPoints >= cost;
    
    public void RemoveSkillPoints(int cost) {
        skillPoints -= cost;
        UpdateSkillPointsText();
    }
    
    public void AddSkillPoints(int points)
    {
        skillPoints += points;
        UpdateSkillPointsText();
    }



    [ContextMenu("Update All Connections")]
    public void UpdateAllConnections()
    {
        foreach (var node in parentNodes)
        {
            node.UpdateAllConnections();
        }
    }
}
