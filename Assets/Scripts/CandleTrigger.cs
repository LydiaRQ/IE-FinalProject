using UnityEngine;
using System.Collections;

public class CandleTrigger : MonoBehaviour
{
    private bool hasTriggered = false;
    private bool triggered = false;
    private VoiceManager voiceManager;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(TriggerVoiceAndFade());
        }
    }

    IEnumerator TriggerVoiceAndFade()
    {
        // 延迟2秒播放第四条语音
        yield return new WaitForSeconds(2f);
        FindObjectOfType<VoiceManager>().PlayVoice4();

        // 等待语音播放完再执行淡出黑幕（语音长度 + 3秒）
        yield return new WaitForSeconds(FindObjectOfType<VoiceManager>().voice4.length + 3f);
        FindObjectOfType<FadeToBlackManager>().StartFade();
    }

    void Update()
    {
        // 如果你按下了 T 键，就强制触发蜡烛的效果（方便测试）
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!triggered)
            {
                triggered = true;
                StartCoroutine(TriggerVoiceAndFade());
            }
        }
    }

}
