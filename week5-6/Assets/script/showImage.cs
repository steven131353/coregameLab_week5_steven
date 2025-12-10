using UnityEngine;

public class ShowImageAfterFirstFragment : MonoBehaviour
{
    [Header("第一个碎片完成后要显示的图片（或父物体）")]
    public GameObject targetImage;

    private void Start()
    {
        if (targetImage == null)
        {
            Debug.LogWarning("ShowImageAfterFirstFragment：targetImage 没有拖拽！");
            return;
        }

        // 如果第一个碎片已经完成，就把图片显示出来
        if (GameProgress.FirstFragmentDone)
        {
            targetImage.SetActive(true);
            Debug.Log("第一个碎片已完成，显示图片：" + targetImage.name);
        }
        else
        {
            // 如果还没完成，就确保它是隐藏状态
            targetImage.SetActive(false);
            Debug.Log("第一个碎片未完成，保持图片隐藏：" + targetImage.name);
        }
    }
}
