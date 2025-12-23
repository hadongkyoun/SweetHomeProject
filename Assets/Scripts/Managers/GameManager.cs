using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private MainMenuHandler MenuHandler;

    // InGame true 면 MainMenuHandler에서 Start Disabled 하기
    public bool InGame = false;

    private void Start()
    {
        MenuHandler = FindFirstObjectByType<MainMenuHandler>();
    }

    public void StartGame()
    {
        // In main menu
        if (!InGame)
        {
            MenuHandler.Loading();
            InGame = true;
        }
    }

    public void ExitGame()
    {
        // GameManager Quit 으로
        Application.Quit();
    }

}
