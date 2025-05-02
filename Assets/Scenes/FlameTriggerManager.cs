using UnityEngine;
using System.Collections;

public class FlameTriggerManager : MonoBehaviour
{
    public GameObject[] flames; // �ֱ����� Flame1, Flame2, Flame3
    public VoiceManager voiceManager; // ԭ�����������Ľű�
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

    // ��VoiceManager�ڵ����ڶ������������
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
                        voiceManager.PlayFinalVoice(); // ���������ԭ���Ĳ������һ�������ķ���
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
