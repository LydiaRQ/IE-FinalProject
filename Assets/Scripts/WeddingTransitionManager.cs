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
        // ��������
        weddingMusic.Play();

        // ��Ļ����Ļ��ʼ͸��
        SetAlpha(blackPanel, 0f);
        SetAlpha(subtitleText, 0f);

        // 10�������Ļ
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

        // ��ʾ��Ļ
       
        SetAlpha(subtitleText, 1f);

        // ׼���ý�����һĻ
        canProceed = true;
    }

    void SetAlpha(Graphic ui, float alpha)
    {
        Color c = ui.color;
        c.a = alpha;
        ui.color = c;
    }
}
