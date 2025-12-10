using UnityEngine;

public class ThirdFragmentImageSwitch : MonoBehaviour
{
    [Header("第三个碎片：破碎版图片")]
    public GameObject brokenImage;

    [Header("第三个碎片：完整版图片")]
    public GameObject fullImage;

    private void Start()
    {
        if (brokenImage == null || fullImage == null)
        {
            Debug.LogWarning("ThirdFragmentImageSwitch：brokenImage 或 fullImage 没有拖！");
            return;
        }

        // 如果第三个碎片已经完成，对主场景显示完整图片
        if (GameProgress.ThirdFragmentDone)
        {
            brokenImage.SetActive(false);
            fullImage.SetActive(true);
            Debug.Log("第三个碎片已完成，显示完整版本图片");
        }
        else
        {
            // 未完成 → 显示破碎图片
            brokenImage.SetActive(true);
            fullImage.SetActive(false);
            Debug.Log("第三个碎片未完成，显示破碎版本图片");
        }
    }
}
