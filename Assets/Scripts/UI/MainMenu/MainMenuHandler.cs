using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [Header("Menu List")]
    [SerializeField]
    private Button StartBtn;
    [SerializeField]
    private Button ContinueBtn;
    [SerializeField]
    private Button OptionBtn;
    [SerializeField]
    private Button ExitBtn;

    [Space(15)]
    [Header("Loading Screen")]
    [SerializeField]
    private CanvasGroup loadingScreen;
    [SerializeField]
    private Slider loadingSlider;



    private void Awake()
    {
        StartBtn.onClick.AddListener(ClickStart);
        ContinueBtn.onClick.AddListener(ClickContinue);
        OptionBtn.onClick.AddListener(ClickOption);
        ExitBtn.onClick.AddListener(ClickExit);

        loadingScreen.alpha = 0f;
        loadingScreen.interactable = false;
        loadingScreen.blocksRaycasts = false;
    }


    private void ClickStart()
    {
        // 코루틴으로 비동기 로드 시작
        StartCoroutine(LoadSceneAsyncRoutine("TestScene"));
    }

    private IEnumerator LoadSceneAsyncRoutine(string sceneName)
    {
        
            loadingScreen.alpha = 1f;
            loadingScreen.interactable = true;
            loadingScreen.blocksRaycasts = true;
        

        loadingSlider.value = 0f;
        //if (loadingPercentText != null) loadingPercentText.text = "0%";

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        // 자동 전환을 잠시 막아 로딩 완료 화면/애니메이션을 보여줄 수 있음
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            // op.progress는 0..0.9 까지 채워지고, 0.9는 '로딩 완료 대기' 상태
            float progress = Mathf.Clamp01(op.progress / 0.9f);

            loadingSlider.value = progress;
            Debug.Log($"Loading Progress: {progress * 100f}%");
            //if (loadingPercentText != null) loadingPercentText.text = $"{Mathf.RoundToInt(progress * 100f)}%";

            // 로드가 끝나고 activation 대기 상태
            if (op.progress >= 0.9f)
            {
                loadingSlider.value = 1f;
                //if (loadingPercentText != null) loadingPercentText.text = "100%";

                // 원하면 페이드아웃/애니메이션/유저 확인 등을 여기서 처리
                yield return new WaitForSeconds(0.4f);

                // 씬 활성화
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }



    private void ClickContinue()
    {
        Debug.Log("Continue Clicked");
    }

    private void ClickOption()
    {
        Debug.Log("Option Clicked");
    }

    private void ClickExit()
    {
        Debug.Log("Exit Clicked");
        Application.Quit();
    }
}
