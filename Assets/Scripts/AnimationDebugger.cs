using UnityEngine;

public class AnimationDebugger : MonoBehaviour
{
    private Animator animator;
    public string animationStateName = "CINEMA_4D___";

    void Start()
    {
        // 获取Animator组件并验证
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("错误: 未找到Animator组件!");
            return;
        }

        // 打印Animator基本信息
        Debug.Log("-------动画调试信息-------");
        Debug.Log("Animator组件已找到");
        Debug.Log("控制器: " + (animator.runtimeAnimatorController != null ? animator.runtimeAnimatorController.name : "未设置"));
        Debug.Log("Avatar: " + (animator.avatar != null ? animator.avatar.name : "未设置"));
        Debug.Log("Apply Root Motion: " + animator.applyRootMotion);

        // 检查动画片段
        if (animator.runtimeAnimatorController != null)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            Debug.Log("动画片段数量: " + clips.Length);

            for (int i = 0; i < clips.Length; i++)
            {
                Debug.Log("片段 " + i + ": " + clips[i].name + " (长度: " + clips[i].length + "秒)");
            }
        }

        // 尝试播放动画
        PlayAnimation();
    }

    void Update()
    {
        // 每秒打印一次状态信息
        if (Time.frameCount % 60 == 0 && animator != null)
        {
            // 检查当前状态
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("当前状态: " + (stateInfo.IsName(animationStateName) ? animationStateName : "未知"));
            Debug.Log("归一化时间: " + stateInfo.normalizedTime);
            Debug.Log("动画速度: " + animator.speed);
            Debug.Log("是否处于过渡: " + animator.IsInTransition(0));
        }

        // 按空格键重播动画
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("按下空格键 - 尝试播放动画");
            PlayAnimation();
        }
    }

    void PlayAnimation()
    {
        if (animator != null)
        {
            Debug.Log("尝试播放动画: " + animationStateName);
            animator.Play(animationStateName, 0, 0f);

            // 延迟检查动画是否开始播放
            Invoke("CheckAnimation", 0.1f);
        }
    }

    void CheckAnimation()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("动画状态检查: " + animationStateName + " 是否激活: " + stateInfo.IsName(animationStateName));
        }
    }
}