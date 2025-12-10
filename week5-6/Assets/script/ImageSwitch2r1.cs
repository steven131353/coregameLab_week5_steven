using UnityEngine;
using UnityEngine.UI;

public class FragmentImageSwitcher : MonoBehaviour
{
    [Header("这个是不是第一个碎片？")]
    public bool isFirstFragment = false;

    [Header("要换图的 Image 组件")]
    public Image targetImage;

    [Header("没完成时的图片")]
    public Sprite normalSprite;

    [Header("完成之后要显示的图片")]
    public Sprite completedSprite;

    private void Start()
    {
        UpdateSprite();
    }

    private void OnEnable()
    {
        // 场景切回来时也刷新一次
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (targetImage == null) return;

        // 只对“第一个碎片”检查状态，其他碎片按需要也可以加逻辑
        if (isFirstFragment && GameProgress.FirstFragmentDone)
        {
            //  第一个碎片已经完成 → 换成完成后的图片
            if (completedSprite != null)
                targetImage.sprite = completedSprite;
        }
        else
        {
            // 没完成 / 其他碎片 → 显示原始图片
            if (normalSprite != null)
                targetImage.sprite = normalSprite;
        }
    }
}
