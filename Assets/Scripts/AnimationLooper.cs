using UnityEngine;

public class AnimationLoop : MonoBehaviour
{
    public Animator animator;              // Animator 组件
    public string animationName = "CINEMA_4D___"; // 动画名称
    public int loopCount = 3;             // 设置循环次数

    private int currentLoop = 0;
    private bool isPlaying = false;      // 用于判断是否正在播放动画

    void Start()
    {
        PlayAnimationWithLoops();   // 启动播放
    }

    void PlayAnimationWithLoops()
    {
        currentLoop = 0;               // 重置循环次数
        isPlaying = true;              // 设置为播放状态
        animator.Play(animationName);  // 播放动画
    }

    void Update()
    {
        if (isPlaying && currentLoop < loopCount)
        {
            // 获取当前动画状态
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // 检查动画是否播放完一轮
            if (stateInfo.normalizedTime >= 1.0f)
            {
                currentLoop++;   // 完成一轮后增加循环次数

                // 如果还没有达到循环次数，重新播放动画
                if (currentLoop < loopCount)
                {
                    animator.Play(animationName);  // 重新播放动画
                }
            }
        }
        else
        {
            // 达到循环次数后停止播放
            if (isPlaying)
            {
                Debug.Log("Animation loop finished.");
                isPlaying = false;  // 设置为停止状态
            }
        }
    }
}
