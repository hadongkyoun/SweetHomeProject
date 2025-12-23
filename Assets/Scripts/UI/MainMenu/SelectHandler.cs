using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectHandler : MonoBehaviour
{

    private MainMenuHandler mainMenuHandler;

    [Space(20)]
    [Header("AskStart Setting")]
    [SerializeField]
    private GameObject AskStartPanel;
    [SerializeField]
    private Button StartYesBtn;
    [SerializeField]
    private Button StartNoBtn;

    [Space(15)]
    [Header("Continue Setting")]
    [SerializeField]
    private GameObject SaveSlotPanel;

    [Space(15)]
    [Header("Option Setting")]
    [SerializeField]
    private GameObject OptionPanel;

    [Space(15)]
    [Header("AskExit Setting")]
    [SerializeField]
    private GameObject AskExitPanel;
    [SerializeField]
    private Button ExitYesBtn;
    [SerializeField]
    private Button ExitNoBtn;


    private void Start()
    {

        StartYesBtn.onClick.AddListener(GameManager.Instance.StartGame);
        StartYesBtn.onClick.AddListener(() => OpenAskStartPanel(false));
        StartNoBtn.onClick.AddListener(() => OpenAskStartPanel(false));

        ExitYesBtn.onClick.AddListener(GameManager.Instance.ExitGame);
        ExitNoBtn.onClick.AddListener(() => OpenAskExitPanel(false));

    }

    public void OpenAskStartPanel(bool isOn)
    {
        AskStartPanel.SetActive(isOn);
    }
    public void OpenSaveSlotPanel(bool isOn)
    {
        if(SaveSlotPanel == null)
            return;
        SaveSlotPanel.SetActive(isOn);
    }
    public void OpenOptionPanel(bool isOn)
    {
        if (OptionPanel == null)
            return;
        OptionPanel.SetActive(isOn);
    }
    public void OpenAskExitPanel(bool isOn)
    {
        AskExitPanel.SetActive(isOn);
    }

    

}
