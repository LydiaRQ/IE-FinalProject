using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FadeToBlackManager : MonoBehaviour
{
    public Image fadePanel; // ȫ����Ļ�� Image����͸���ȣ�
    public TextMeshProUGUI subtitleText; // �м���ʾ����Ļ
    public float fadeDuration = 2f;

    void Start()
    {
        // ȷ��һ��ʼ��͸����
        Color panelColor = fadePanel.color;
        panelColor.a = 0f;
        fadePanel.color = panelColor;

        // ������ֶ����ú�����Ļ������Ͳ��������
        subtitleText.alpha = 0f; // ��Ļ�Ȳ���ʾ
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

        // �����Ļ��ɺ���Ļ����
        subtitleText.alpha = 1f;
        FindObjectOfType<TransitionManager>().EnableTransition();
    }
}
