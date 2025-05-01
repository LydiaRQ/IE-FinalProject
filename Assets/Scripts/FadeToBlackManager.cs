using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FadeToBlackManager : MonoBehaviour
{
    public Image fadePanel; // 全屏黑幕的 Image（带透明度）
    public TextMeshProUGUI subtitleText; // 中间显示的字幕
    public float fadeDuration = 2f;

    void Start()
    {
        // 确保一开始是透明的
        Color panelColor = fadePanel.color;
        panelColor.a = 0f;
        fadePanel.color = panelColor;

        // 如果你手动设置好了字幕，这里就不清空它了
        subtitleText.alpha = 0f; // 字幕先不显示
    }

    public void StartFade()
    {
        StartCoroutine(FadeInBlack());
    }

    IEnumerator FadeInBlack()
    {
        float timer = 0f;
        Color panelColor = fadePanel.color;

        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            panelColor.a = alpha;
            fadePanel.color = panelColor;
            timer += Time.deltaTime;
            yield return null;
        }

        panelColor.a = 1f;
        fadePanel.color = panelColor;

        // 淡入黑幕完成后，字幕淡入
        subtitleText.alpha = 1f;
        FindObjectOfType<TransitionManager>().EnableTransition();
    }
}
