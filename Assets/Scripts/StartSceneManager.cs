using UnityEngine;
using UnityEngine.UI;


public class StartSceneManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    public Image blackScreen;
    public Image titleImage;
    public Text introText;
    public Text continueHint;

    private bool canContinue = false;
    private bool isTitleShowing = false;

    void Start()
    {
        backgroundMusic.Play();

        // 所有元素初始透明
        SetAlpha(blackScreen, 0f);
        SetAlpha(titleImage, 0f);
        SetAlpha(introText, 0f);
        SetAlpha(continueHint, 0f);

        // 5秒后开始渐黑
        Invoke("FadeInBlackScreen", 5f);
    }

    void Update()
    {
        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Continue pressed");
            // 这里可以加你正式进入故事的代码，比如关掉UI，切换状态
        }
    }

    void FadeInBlackScreen()
    {
        StartCoroutine(FadeImage(blackScreen, 0f, 1f, 1f)); // 1秒内渐黑
        Invoke("FadeInTitle", 1f); // 黑完之后再出现标题
    }

    void FadeInTitle()
    {
        StartCoroutine(FadeImage(titleImage, 0f, 1f, 1f));
        isTitleShowing = true;
        Invoke("FadeOutTitle", 5f); // 5秒后标题渐出
    }

    void FadeOutTitle()
    {
        if (isTitleShowing)
        {
            StartCoroutine(FadeImage(titleImage, 1f, 0f, 1f));
            Invoke("FadeInIntro", 1f); // 标题渐隐后显示介绍
        }
    }

    void FadeInIntro()
    {
        StartCoroutine(FadeText(introText, 0f, 1f, 1f));
        Invoke("FadeInContinueHint", 5f);
    }

    void FadeInContinueHint()
    {
        StartCoroutine(FadeText(continueHint, 0f, 1f, 1f));
        canContinue = true;
    }

    // 工具函数：设置透明度
    void SetAlpha(Graphic uiElement, float alpha)
    {
        Color color = uiElement.color;
        color.a = alpha;
        uiElement.color = color;
    }

    // 协程：渐变图片
    System.Collections.IEnumerator FadeImage(Image img, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            SetAlpha(img, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(img, endAlpha);
    }

    // 协程：渐变文字
    System.Collections.IEnumerator FadeText(Text txt, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            SetAlpha(txt, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(txt, endAlpha);
    }
}