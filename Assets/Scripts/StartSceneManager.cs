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

        // ����Ԫ�س�ʼ͸��
        SetAlpha(blackScreen, 0f);
        SetAlpha(titleImage, 0f);
        SetAlpha(introText, 0f);
        SetAlpha(continueHint, 0f);

        // 5���ʼ����
        Invoke("FadeInBlackScreen", 5f);
    }

    void Update()
    {
        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Continue pressed");
            // ������Լ�����ʽ������µĴ��룬����ص�UI���л�״̬
        }
    }

    void FadeInBlackScreen()
    {
        StartCoroutine(FadeImage(blackScreen, 0f, 1f, 1f)); // 1���ڽ���
        Invoke("FadeInTitle", 1f); // ����֮���ٳ��ֱ���
    }

    void FadeInTitle()
    {
        StartCoroutine(FadeImage(titleImage, 0f, 1f, 1f));
        isTitleShowing = true;
        Invoke("FadeOutTitle", 5f); // 5�����⽥��
    }

    void FadeOutTitle()
    {
        if (isTitleShowing)
        {
            StartCoroutine(FadeImage(titleImage, 1f, 0f, 1f));
            Invoke("FadeInIntro", 1f); // ���⽥������ʾ����
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

    // ���ߺ���������͸����
    void SetAlpha(Graphic uiElement, float alpha)
    {
        Color color = uiElement.color;
        color.a = alpha;
        uiElement.color = color;
    }

    // Э�̣�����ͼƬ
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

    // Э�̣���������
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