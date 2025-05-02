using UnityEngine;
using System.Collections;

public class VoiceManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource voiceSource;

    public AudioClip morningBGM;
    public AudioClip voice1;
    public AudioClip voice2;
    public AudioClip voice3;
    public AudioClip voice4;

    private bool voice4Played = false;

    void Start()
    {
        bgmSource.clip = morningBGM;
        bgmSource.loop = true;
        bgmSource.Play();

        StartCoroutine(PlayVoices());
    }
    public void PlayVoice4()
    {
        voiceSource.clip = voice4;
        voiceSource.Play();
    }

    IEnumerator PlayVoices()
    {
        yield return new WaitForSeconds(3f);
        voiceSource.clip = voice1;
        voiceSource.Play();
        yield return new WaitForSeconds(voice1.length + 0.5f);

        voiceSource.clip = voice2;
        voiceSource.Play();
        yield return new WaitForSeconds(voice2.length + 2f);

        voiceSource.clip = voice3;
        voiceSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!voice4Played && other.CompareTag("PlayerHand"))  // ȷ���������ֲ�������"PlayerHand" tag
        {
            voice4Played = true;
            StartCoroutine(PlayVoice4Sequence());
        }
    }

    IEnumerator PlayVoice4Sequence()
    {
        yield return new WaitForSeconds(2f); // �������ٲ�����
        voiceSource.clip = voice4;
        voiceSource.Play();

        yield return new WaitForSeconds(voice4.length + 3f); // ���������ٵ�����
        FindObjectOfType<FadeToBlackManager>().StartFade(); // ��Ļ���루�������ڻ����У�
    }

    public void PlayFinalVoice()
    {
        StartCoroutine(PlayVoice4Sequence());
    }


}
