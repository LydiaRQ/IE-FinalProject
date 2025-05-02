using UnityEngine;
using System.Collections;

public class FlameTriggerManager : MonoBehaviour
{
    public GameObject[] flames; // 分别拖入 Flame1, Flame2, Flame3
    public VoiceManager voiceManager; // 原来播放语音的脚本
    public Collider playerTriggerCollider;

    private bool flamesActivated = false;
    private int extinguishedCount = 0;

    void Start()
    {
        foreach (GameObject flame in flames)
        {
            flame.SetActive(false);
        }
    }

    // 由VoiceManager在倒数第二句语音后调用
    public void ActivateFlames()
    {
        flamesActivated = true;
        foreach (GameObject flame in flames)
        {
            flame.SetActive(true);
        }
    }

    void Update()
    {
        if (!flamesActivated) return;

        for (int i = 0; i < flames.Length; i++)
        {
            if (flames[i].activeSelf)
            {
                if (IsPlayerTouching(flames[i]))
                {
                    flames[i].SetActive(false);
                    extinguishedCount++;

                    if (extinguishedCount == flames.Length)
                    {
                        flamesActivated = false;
                        voiceManager.PlayFinalVoice(); // 这里调用你原来的播放最后一段语音的方法
                    }
                }
            }
        }
    }

    bool IsPlayerTouching(GameObject flame)
    {
        Collider flameCollider = flame.GetComponent<Collider>();
        return playerTriggerCollider.bounds.Intersects(flameCollider.bounds);
    }
}
