using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // ✅ 用于切换场景

public class FragmentHitbox : MonoBehaviour, IPointerClickHandler
{
    public string sceneToLoad = "DialogueScene"; // 默认跳转场景名

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked fragment: " + transform.parent.name);
        // ✅ 切换到对话页面
        SceneManager.LoadScene(sceneToLoad);
    }
}
