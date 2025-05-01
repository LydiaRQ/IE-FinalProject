using UnityEngine;

public class SoulFloat : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 0.1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0f);
    }
}
