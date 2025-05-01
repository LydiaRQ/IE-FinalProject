using UnityEngine;

public class SpiritMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 1f;

    public GameObject warningText;

    // �ƶ���Χ����
    public float minX = -20f;
    public float maxX = 15f;
    public float minY = -8f;
    public float maxY = 15f;
    public float minZ = 0f;    // ���������̫Զ������Z��������ǰ����
    public float maxZ = 2f;    // ������΢���

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        if (warningText != null)
        {
            warningText.SetActive(false);
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ����Ч����ֻ���ƶ�ʱ������Ư����
        float floatOffset = 0f;
        if (horizontal != 0 || vertical != 0)
        {
            floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        }

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        Vector3 targetPosition = transform.position + move * moveSpeed * Time.deltaTime;

        // Ӧ�ø���
        targetPosition.y += floatOffset;

        // �ж�Ŀ��λ���Ƿ�������Χ��
        bool withinX = targetPosition.x >= minX && targetPosition.x <= maxX;
        bool withinY = targetPosition.y >= minY && targetPosition.y <= maxY;
        bool withinZ = targetPosition.z >= minZ && targetPosition.z <= maxZ;

        if (withinX && withinY && withinZ)
        {
            transform.position = targetPosition;

            if (warningText != null)
                warningText.SetActive(false);
        }
        else
        {
            // ���Ŀ��λ�ó�����Χ��ͣ��ԭ�أ�����ʾ��ʾ
            if (warningText != null)
                warningText.SetActive(true);
        }
    }
}
