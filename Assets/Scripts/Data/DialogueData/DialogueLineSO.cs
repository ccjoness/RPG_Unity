using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Dialogue Data/New Line Data", fileName = "Line - ")]
public class DialogueLineSO : ScriptableObject
{
    [Header("Dialogue Info")]
    public string dialogueGroup;
    public DialogueSpeakerSO speaker;
    
    [Header("Text Options")]
    [TextArea] public string[] textLine;
    
    [Header("Dialogue Action")]
    public DialogueActionType actionType;
    
    
    
    public string GetRandomLine() => textLine[Random.Range(0, textLine.Length)];
}
