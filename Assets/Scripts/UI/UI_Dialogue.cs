using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialogue : MonoBehaviour
{
    private UI ui;
    
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueChoices;
    [Space]
    [SerializeField] private float typeSpeed = .1f;
    private string fullTextToShow;
    private Coroutine typeTextCo;
    
    private DialogueLineSO currentLine;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
    }

    public void PlayDialogueLine(DialogueLineSO line)
    {
        currentLine = line;
        
        speakerPortrait.sprite = line.speaker.speakerPortrait;
        speakerName.text = line.speaker.speakerName;
        
        fullTextToShow = line.GetRandomLine();
        typeTextCo = StartCoroutine(TypeTextCo(fullTextToShow));
    }

    private void HandleNextAction()
    {
        switch (currentLine.actionType)
        {
            case DialogueActionType.OpenShop:
                ui.SwitchToInGameUI();
                ui.OpenMerchantUI(true);
                break;
        }
    }
    
    public void DialogueInteraction()
    {
        if (typeTextCo != null && dialogueText.text.Length > 5)
        {
            CompleteTyping();
            return;
        }
        
    }
    
    private void CompleteTyping()
    { 
        if (typeTextCo != null)
        {
            StopCoroutine(typeTextCo);
            dialogueText.text = fullTextToShow;
        }
    }
    
    private IEnumerator TypeTextCo(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
