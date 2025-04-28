using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{

    void Update()
    {
        // 简单的前进移动
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

}
