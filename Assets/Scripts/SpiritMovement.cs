using UnityEngine;

public class SpiritMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 1f;

    public GameObject warningText;

    // 移动范围限制
    public float minX = -20f;
    public float maxX = 15f;
    public float minY = -8f;
    public float maxY = 15f;
    public float minZ = 0f;    // 不能离雕像太远（假设Z负方向是前方）
    public float maxZ = 2f;    // 允许稍微向后

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

        // 浮动效果（只有移动时才上下漂浮）
        float floatOffset = 0f;
        if (horizontal != 0 || vertical != 0)
        {
            floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        }

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        Vector3 targetPosition = transform.position + move * moveSpeed * Time.deltaTime;

        // 应用浮动
        targetPosition.y += floatOffset;

        // 判断目标位置是否在允许范围内
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
            // 如果目标位置超出范围，停在原地，仅显示提示
            if (warningText != null)
                warningText.SetActive(true);
        }
    }
}
