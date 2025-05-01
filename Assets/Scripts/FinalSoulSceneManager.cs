using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class FinalSoulSceneManager : MonoBehaviour
{
    public GameObject soulCore;
    public AudioSource voiceSource;
    public AudioClip finalVoice;

    public TextMeshProUGUI touchHintText;
    public TextMeshProUGUI finalMessageText;
    public Image blackPanel;
    public Image titleImage;

    private bool soulTouched = false;
    private bool hintShown = false;

    void Start()
    {
        // 初始状态全部隐藏
        soulCore.SetActive(false);
        touchHintText.gameObject.SetActive(false);
        finalMessageText.gameObject.SetActive(false);
        blackPanel.gameObject.SetActive(false);
        titleImage.gameObject.SetActive(false);

        StartCoroutine(SceneFlow());
    }

    IEnumerator SceneFlow()
    {
        yield return new WaitForSeconds(10f); // 等待 10 秒后显示光团并播放语音

        soulCore.SetActive(true);
        voiceSource.clip = finalVoice;
        voiceSource.Play();

        yield return new WaitForSeconds(finalVoice.length + 5f);

        if (!soulTouched)
        {
            touchHintText.gameObject.SetActive(true);
            hintShown = true;
        }
    }

    void Update()
    {
        if (!soulTouched && IsSoulTouched())
        {
            soulTouched = true;
            if (hintShown)
                touchHintText.gameObject.SetActive(false);

            StartCoroutine(HandleSoulTouch());
        }
    }

    bool IsSoulTouched()
    {
        // PC 模拟：按空格判定是否靠近光团
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider[] hits = Physics.OverlapSphere(soulCore.transform.position, 0.5f);
            foreach (Collider hit in hits)
            {
                if (hit.gameObject == soulCore)
                    return true;
            }
        }
        return false;
    }

    IEnumerator HandleSoulTouch()
    {
        finalMessageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        blackPanel.gameObject.SetActive(true);
        titleImage.gameObject.SetActive(true);
    }

}
