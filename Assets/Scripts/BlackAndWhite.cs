using UnityEngine;
using UnityEngine.Rendering.PostProcessing; // 内置渲染管线的后处理命名空间

public class BlackAndWhite : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    void Start()
    {
        // 确保找到PostProcessVolume组件
        if (postProcessVolume == null)
        {
            Debug.LogError("请指定一个PostProcessVolume组件!");
            return;
        }

        // 尝试获取ColorGrading组件
        postProcessVolume.profile.TryGetSettings(out colorGrading);

        if (colorGrading != null)
        {
            // 启用效果并设置饱和度为-100（黑白）
            colorGrading.enabled.Override(true);
            colorGrading.saturation.Override(-100f);

            Debug.Log("已将饱和度设置为-100");
        }
        else
        {
            Debug.LogError("找不到ColorGrading组件！");
        }
    }

    // 可以添加一个切换方法
    public void ToggleGrayscale(bool enable)
    {
        if (colorGrading != null)
        {
            colorGrading.saturation.Override(enable ? -100f : 0f);
        }
    }
}