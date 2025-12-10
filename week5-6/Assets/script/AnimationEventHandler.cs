using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventHandler : MonoBehaviour
{
    // 这个函数名要和 Animation Event 里填的一样
    public void OnAnimationEnd()
    {
        Debug.Log("动画播放结束，标记第一个碎片已完成，并返回主场景");

        //  关键：告诉系统“第一个碎片已经完成”
        GameProgress.FirstFragmentDone = true;

        //  然后再跳回主场景
        SceneManager.LoadScene("Main");
    }
}
