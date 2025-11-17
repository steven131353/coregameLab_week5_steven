using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventHandler : MonoBehaviour
{
    //  这个函数名必须和 Animation Event 中填写的一样
    public void OnAnimationEnd()
    {
        Debug.Log(" 动画播放结束，返回主场景");
        SceneManager.LoadScene("Main");
    }
}
