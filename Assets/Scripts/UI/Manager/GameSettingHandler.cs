using Unity.Cinemachine;
using UnityEngine;


// This is control Game setting option's value.
public class GameSettingHandler : MonoBehaviour
{
    //[Header("Have user changed value scripts")]
    private PlayerController playerController;
    //allGameSettings = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameSetting>().ToList();


    private void Awake()
    {
        
    }


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
