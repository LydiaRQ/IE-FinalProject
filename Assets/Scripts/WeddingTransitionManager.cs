using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class WeddingTransitionManager : MonoBehaviour
{
    public AudioSource weddingMusic;
    public Image blackPanel;
    public TextMeshProUGUI subtitleText;
    public float fadeDuration = 2f;
    public string nextSceneName;

    private bool canProceed = false;

    void Start()
    {
        // 开场音乐
        weddingMusic.Play();

        // 黑幕与字幕初始透明
        SetAlpha(blackPanel, 0f);
        SetAlpha(subtitleText, 0f);

        // 10秒后进入黑幕
        Invoke("StartFade", 10f);
    }

    void Update()
    {
        if (canProceed && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void StartFade()
    {
        StartCoroutine(FadeInBlack());
    }

    IEnumerator FadeInBlack()
    {
        float timer = 0f;
        Color panelColor = blackPanel.color;

        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            panelColor.a = alpha;
            blackPanel.color = panelColor;
            timer += Time.deltaTime;
            yield return null;
        }

        panelColor.a = 1f;
        blackPanel.color = panelColor;

        // 显示字幕
       
        SetAlpha(subtitleText, 1f);

        // 准备好进入下一幕
        canProceed = true;
    }

    void SetAlpha(Graphic ui, float alpha)
    {
        Color c = ui.color;
        c.a = alpha;
        ui.color = c;
    }
}
