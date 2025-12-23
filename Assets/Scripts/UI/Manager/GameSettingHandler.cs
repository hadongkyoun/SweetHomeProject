using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// This is control Game setting option's value.
public class GameSettingHandler : MonoBehaviour
{
    private PlayerController playerController;

    //allGameSettings = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameSetting>().ToList();


    //public void UpdateGraphicSettingValue()
    //{

    //}
    public void UpdateGameplaySettingValue()
    {
        if (playerController == null)
        {
            if (GameObject.FindWithTag("Player").TryGetComponent<PlayerController>(out PlayerController player))
            {
                playerController = player;
            }
        }

        if (playerController != null)
        {
            playerController.UpdateGameSettingInGame();
        }
    }
    //public void UpdateSoundSettingValue()
    //{

    //}
}
