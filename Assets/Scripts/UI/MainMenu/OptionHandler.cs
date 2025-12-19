using UnityEngine;
using UnityEngine.UI;

public class OptionHandler : MonoBehaviour
{
    [Space(15)]
    [Header("Graphics Setting")]
    [SerializeField]
    private GameObject GraphicsPanel;
    [SerializeField]
    private Button GraphicsBtn;

    [Space(15)]
    [Header("Sound Setting")]
    [SerializeField]
    private GameObject SoundPanel;
    [SerializeField]
    private Button SoundBtn;


    [Space(15)]
    [Header("GamePlay Setting")]
    [SerializeField]
    private GameObject GameplayPanel;
    [SerializeField]
    private Button GameplayBtn;

    [Space(15)]
    [Header("Return Setting")]
    [SerializeField]
    private Button ReturnBtn;


    private void Awake()
    {
        GraphicsBtn.onClick.AddListener(() => OpenGraphicPanel(true));
        SoundBtn.onClick.AddListener(() => OpenSoundPanel(true));
        GameplayBtn.onClick.AddListener(() => OpenGamePlayPanel(true));
        ReturnBtn.onClick.AddListener(Return);
    }

    public void OpenGraphicPanel(bool isOn)
    {
        GraphicsPanel.SetActive(isOn);
    }
    public void OpenSoundPanel(bool isOn)
    {
        SoundPanel.SetActive(isOn);
    }
    public void OpenGamePlayPanel(bool isOn)
    {
        GameplayPanel.SetActive(isOn);
    }
    private void Return()
    {
        this.gameObject.SetActive(false);
    }
}
