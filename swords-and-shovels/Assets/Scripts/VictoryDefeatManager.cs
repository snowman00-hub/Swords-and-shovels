using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryDefeatManager : MonoBehaviour
{
    public static VictoryDefeatManager Instance;
    public CanvasGroup diePanel;
    public CanvasGroup victoryPanel;

    public float waitTime = 2f;
    public float fadeTime = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // 테스트 코드
        if(Input.GetKeyDown(KeyCode.J))
        {
            Defeat();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Victory();
        }
        //
    }

    public void Defeat()
    {
        FadeAsync(diePanel).Forget();
    }

    public void Victory()
    {
        FadeAsync(victoryPanel).Forget();
    }

    private async UniTask FadeAsync(CanvasGroup panel)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

        float elapsed = 0f;
        while(elapsed < waitTime)
        {
            elapsed += Time.deltaTime;
            float t= elapsed/waitTime;
            panel.alpha = Mathf.Lerp(0, 1, t);
            await UniTask.Yield();
        }

        panel.alpha = 1;
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}