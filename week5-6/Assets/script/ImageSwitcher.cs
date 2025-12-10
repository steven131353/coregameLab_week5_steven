using UnityEngine;

public class SecondFragmentImageSwitch : MonoBehaviour
{
    [Header("第二个碎片：破碎版图片")]
    public GameObject brokenImage;

    [Header("第二个碎片：完整版图片")]
    public GameObject fullImage;

    private void Start()
    {
        if (brokenImage == null || fullImage == null)
        {
            Debug.LogWarning("SecondFragmentImageSwitch：brokenImage 或 fullImage 没有拖！");
            return;
        }

        // ✅ 如果第二个碎片已经完成，显示完整车，隐藏破碎图
        if (GameProgress.SecondFragmentDone)
        {
            brokenImage.SetActive(false);
            fullImage.SetActive(true);
            Debug.Log("第二个碎片已完成，显示完整车辆图片");
        }
        else
        {
            // ✅ 还没完成：显示破碎图，隐藏完整图
            brokenImage.SetActive(true);
            fullImage.SetActive(false);
            Debug.Log("第二个碎片未完成，显示破碎碎片图片");
        }
    }
}
