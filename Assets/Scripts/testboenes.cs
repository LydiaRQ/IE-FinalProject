using UnityEngine;

public class SkeletonDebugger : MonoBehaviour
{
    public bool logBoneInfo = true;

    void Start()
    {
        if (logBoneInfo)
        {
            Debug.Log("==== 角色骨骼结构信息 ====");
            LogBoneStructure(transform, 0);
        }

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            if (animator.avatar != null && animator.avatar.isHuman)
            {
                Debug.Log("==== Humanoid骨骼映射信息 ====");
                CheckHumanoidRig(animator);
            }
            else
            {
                Debug.Log("Avatar不是Humanoid类型，无法检查骨骼映射");
            }
        }
        else
        {
            Debug.LogError("未找到Animator组件");
        }
    }

    void LogBoneStructure(Transform bone, int depth)
    {
        string indent = new string('-', depth * 2);
        Debug.Log(indent + bone.name + " (位置: " + bone.localPosition + ", 旋转: " + bone.localRotation.eulerAngles + ")");

        foreach (Transform child in bone)
        {
            LogBoneStructure(child, depth + 1);
        }
    }

    void CheckHumanoidRig(Animator animator)
    {
        // 检查主要骨骼映射
        CheckBone(animator, HumanBodyBones.Hips, "臀部");
        CheckBone(animator, HumanBodyBones.Spine, "脊柱");
        CheckBone(animator, HumanBodyBones.Chest, "胸部");
        CheckBone(animator, HumanBodyBones.Head, "头部");

        // 检查四肢
        CheckBone(animator, HumanBodyBones.LeftUpperArm, "左上臂");
        CheckBone(animator, HumanBodyBones.LeftLowerArm, "左前臂");
        CheckBone(animator, HumanBodyBones.LeftHand, "左手");

        CheckBone(animator, HumanBodyBones.RightUpperArm, "右上臂");
        CheckBone(animator, HumanBodyBones.RightLowerArm, "右前臂");
        CheckBone(animator, HumanBodyBones.RightHand, "右手");

        CheckBone(animator, HumanBodyBones.LeftUpperLeg, "左大腿");
        CheckBone(animator, HumanBodyBones.LeftLowerLeg, "左小腿");
        CheckBone(animator, HumanBodyBones.LeftFoot, "左脚");

        CheckBone(animator, HumanBodyBones.RightUpperLeg, "右大腿");
        CheckBone(animator, HumanBodyBones.RightLowerLeg, "右小腿");
        CheckBone(animator, HumanBodyBones.RightFoot, "右脚");
    }

    void CheckBone(Animator animator, HumanBodyBones bone, string boneName)
    {
        Transform boneTransform = animator.GetBoneTransform(bone);
        if (boneTransform != null)
        {
            Debug.Log(boneName + "骨骼映射成功: " + boneTransform.name);
        }
        else
        {
            Debug.LogWarning(boneName + "骨骼映射失败，这可能影响动画播放");
        }
    }
}