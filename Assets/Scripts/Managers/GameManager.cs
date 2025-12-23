using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private MainMenuHandler MenuHandler;
    private GameSettingHandler gameSettingHandler;

    // InGame true 면 MainMenuHandler에서 Start Disabled 하기
    public bool InGame = false;

    public override void Awake()
    {
        base.Awake();
        MenuHandler = FindFirstObjectByType<MainMenuHandler>();
        gameSettingHandler = GetComponent<GameSettingHandler>();
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

    public void UpdateGameSetting(string settingType)
    {
        if (!InGame)            
            return;
        
        if(gameSettingHandler != null)
        {
            gameSettingHandler.UpdateGameplaySettingValue();
        }


    }
}
