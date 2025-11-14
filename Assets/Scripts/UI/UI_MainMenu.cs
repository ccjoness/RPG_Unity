using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private void Start()
    {
        transform.root.GetComponentInChildren<UI_Options>(true).LoadUpVolume();
        transform.root.GetComponentInChildren<UI_FadeScreen>().FadeIn();
        AudioManager.instance.StartBGM("playlist_mainMenu");
    }
    
    public void PlayButton()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.ContinueGame();
    }
    
    public void QuitButton() => Application.Quit();
}
