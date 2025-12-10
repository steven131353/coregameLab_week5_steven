using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class FragmentHitbox : MonoBehaviour, IPointerClickHandler
{
    public enum FragmentId
    {
        First,
        Second,
        Third
    }

    [Header("这个 hitbox 对应的是第几个碎片？")]
    public FragmentId fragmentId = FragmentId.First;

    [Header("点击后要跳转的场景名")]
    public string sceneToLoad = "DialogueScene1";

    [Header("错误提示文字（同一个 Text 可以给三个碎片共用）")]
    public TextMeshProUGUI warningText;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 当前进度下，应该点击哪一个碎片？
        FragmentId required = GetCurrentRequiredFragment();

        Debug.Log($"点击了碎片: {fragmentId}，当前正确的应该是: {required}");

        // 如果三块都已经完成了，就随便让他点（或直接 return，看你想不想再进对话）
        if (GameProgress.FirstFragmentDone &&
            GameProgress.SecondFragmentDone &&
            GameProgress.ThirdFragmentDone)
        {
            // 这里如果不想后面还能再进对话，可以直接 return;
            // return;

            SceneManager.LoadScene(sceneToLoad);
            return;
        }

        // 如果点的不是当前应该点的那一块 → 提示错误
        if (fragmentId != required)
        {
            ShowWrongClickMessage(required);
            return;
        }

        // ✅ 点对了 → 正常进入对应对话场景
        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>
    /// 根据当前 GameProgress，判断此时应该点哪一个碎片
    /// </summary>
    private FragmentId GetCurrentRequiredFragment()
    {
        if (!GameProgress.FirstFragmentDone)
            return FragmentId.First;

        if (!GameProgress.SecondFragmentDone)
            return FragmentId.Second;

        if (!GameProgress.ThirdFragmentDone)
            return FragmentId.Third;

        // 如果都完成了，就随便给一个（其实上面已经在 OnPointerClick 里判断完了）
        return FragmentId.Third;
    }

    /// <summary>
    /// 显示“点错碎片”的提示文字
    /// </summary>
    private void ShowWrongClickMessage(FragmentId required)
    {
        if (warningText == null)
        {
            Debug.Log("点错碎片，但没有设置 warningText UI");
            return;
        }

        warningText.gameObject.SetActive(true);

        switch (required)
        {
            case FragmentId.First:
                warningText.text = "WRONG";
                break;
            case FragmentId.Second:
                warningText.text = "WRONG。";
                break;
            case FragmentId.Third:
                warningText.text = "WRONG";
                break;
        }
    }
}
