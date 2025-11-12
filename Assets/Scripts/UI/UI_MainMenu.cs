using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private void Start()
    {
        transform.root.GetComponentInChildren<UI_FadeScreen>().FadeIn();
    }
    public void PlayButton()
    {
        GameManager.instance.ContinueGame();
    }
    
    public void QuitButton() => Application.Quit();
}
