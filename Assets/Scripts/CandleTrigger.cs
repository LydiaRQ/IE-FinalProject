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
        // �ӳ�2�벥�ŵ���������
        yield return new WaitForSeconds(2f);
        FindObjectOfType<VoiceManager>().PlayVoice4();

        // �ȴ�������������ִ�е�����Ļ���������� + 3�룩
        yield return new WaitForSeconds(FindObjectOfType<VoiceManager>().voice4.length + 3f);
        FindObjectOfType<FadeToBlackManager>().StartFade();
    }

    void Update()
    {
        // ����㰴���� T ������ǿ�ƴ��������Ч����������ԣ�
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
