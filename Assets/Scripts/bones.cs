using UnityEngine;
using System.Collections.Generic;

public class BoneMovementDetector : MonoBehaviour
{
    // 要监视的骨骼名称
    public string[] bonesToWatch = {
        "Bip01_R_Hand",
        "Bip01_L_Hand",
        "Bip01_Spine",
        "Bip01_Head"
    };

    // 存储骨骼Transform引用
    private Dictionary<string, Transform> boneTransforms = new Dictionary<string, Transform>();

    // 存储上一帧的位置和旋转
    private Dictionary<string, Vector3> lastPositions = new Dictionary<string, Vector3>();
    private Dictionary<string, Quaternion> lastRotations = new Dictionary<string, Quaternion>();

    // 记录每个骨骼的移动总量
    private Dictionary<string, float> totalMovement = new Dictionary<string, float>();
    private Dictionary<string, float> totalRotation = new Dictionary<string, float>();

    // 更新间隔（帧）- 每帧都检查
    private int updateInterval = 1;

    // 时间计数器
    private float timer = 0;

    void Start()
    {
        // 查找所有要监视的骨骼
        FindBonesRecursive(transform);

        // 初始化位置和旋转记录
        foreach (var bone in boneTransforms)
        {
            lastPositions[bone.Key] = bone.Value.position;
            lastRotations[bone.Key] = bone.Value.rotation;
            totalMovement[bone.Key] = 0f;
            totalRotation[bone.Key] = 0f;
        }

        Debug.Log("开始监视 " + boneTransforms.Count + " 个骨骼的移动");
    }

    void FindBonesRecursive(Transform current)
    {
        foreach (var boneName in bonesToWatch)
        {
            if (current.name.Contains(boneName))
            {
                boneTransforms[current.name] = current;
                Debug.Log("找到骨骼: " + current.name);
            }
        }

        foreach (Transform child in current)
        {
            FindBonesRecursive(child);
        }
    }

    void Update()
    {
        // 更新时间计数器
        timer += Time.deltaTime;

        // 每隔指定帧数检查一次
        if (Time.frameCount % updateInterval != 0)
            return;

        // 检查每个骨骼的移动
        foreach (var bone in boneTransforms)
        {
            // 计算位置变化
            float positionDelta = Vector3.Distance(bone.Value.position, lastPositions[bone.Key]);
            totalMovement[bone.Key] += positionDelta;

            // 计算旋转变化
            float rotationDelta = Quaternion.Angle(bone.Value.rotation, lastRotations[bone.Key]);
            totalRotation[bone.Key] += rotationDelta;

            // 如果有任何变化，记录日志 (减小阈值以捕获微小变化)
            if (positionDelta > 0.0001f || rotationDelta > 0.01f)
            {
                Debug.Log(timer.ToString("F3") + "秒: " + bone.Key + " 移动: 位置变化=" + positionDelta.ToString("F5") +
                          ", 旋转变化=" + rotationDelta.ToString("F5"));
            }

            // 更新上一帧的值
            lastPositions[bone.Key] = bone.Value.position;
            lastRotations[bone.Key] = bone.Value.rotation;
        }

        // 每30帧(约0.5秒)输出一次总体统计
        if (Time.frameCount % 30 == 0)
        {
            Debug.Log("=== " + timer.ToString("F3") + "秒: 骨骼移动总量统计 ===");
            foreach (var bone in totalMovement)
            {
                Debug.Log(bone.Key + ": 总位移=" + bone.Value.ToString("F5") +
                          ", 总旋转=" + totalRotation[bone.Key].ToString("F5"));
            }
        }
    }
}