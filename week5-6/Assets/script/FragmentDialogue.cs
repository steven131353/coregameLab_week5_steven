using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FragmentDialogue : MonoBehaviour
{
    // 这个场景是第几个碎片的对话
    public enum FragmentId
    {
        First,
        Second,
        Third
    }

    [Header("这是第几个碎片的对话场景？")]
    public FragmentId fragmentId = FragmentId.First;

    [Header("对话文字 UI")]
    public TextMeshProUGUI dialogueText;

    [Header("每一句对话内容")]
    [TextArea(2, 5)]
    public string[] lines;        // 在 Inspector 里填每一句话

    [Header("打字机速度（秒/字）")]
    public float typeSpeed = 0.03f;

    [Header("下一句按钮（可选，只是为了在代码里开关用）")]
    public GameObject nextButton;

    [Header("碎片相关图片")]
    public GameObject fragmentImage;   // 破碎的图
    public GameObject fullImage;       // 完整状态图（对话结束时显示）

    [Header("对话结束后是否回到主场景")]
    public bool goBackToMainWhenFinish = true;
    public string mainSceneName = "Main";

    private int index = 0;         // 当前是第几句
    private bool isTyping = false; // 是否正在打字中

    private void Start()
    {
        // 一开始显示碎片，隐藏完整图
        if (fragmentImage != null) fragmentImage.SetActive(true);
        if (fullImage != null) fullImage.SetActive(false);

        index = 0;
        ShowCurrentLine();
    }

    /// <summary>
    /// 显示当前 index 对应的那一句话
    /// </summary>
    private void ShowCurrentLine()
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("FragmentDialogue：没有填任何对话内容（lines 为空）");
            return;
        }

        if (index < 0 || index >= lines.Length)
        {
            Debug.LogWarning("FragmentDialogue：index 越界：" + index);
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeLine(lines[index]));
    }

    /// <summary>
    /// 打字机效果逐字显示
    /// </summary>
    private IEnumerator TypeLine(string line)
    {
        isTyping = true;

        if (dialogueText != null)
            dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    /// <summary>
    /// “下一句”按钮点击事件 —— 绑到 Button 的 OnClick 上
    /// </summary>
    public void OnClickNext()
    {
        // 如果还在打字中，先直接显示完整一句
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = lines[index];
            isTyping = false;
            return;
        }

        // 跳到下一句
        index++;

        if (index < lines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            // 没有更多句子了，结束对话
            EndDialogue();
        }
    }

    /// <summary>
    /// 对话结束时：
    /// 1. 碎片图 → 完整图
    /// 2. 标记对应碎片已完成
    /// 3. 选择是否回到主场景
    /// </summary>
    private void EndDialogue()
    {
        // ① 碎片 → 完整图
        if (fragmentImage != null) fragmentImage.SetActive(false);
        if (fullImage != null) fullImage.SetActive(true);

        // ② 记录对应碎片已经完成
        switch (fragmentId)
        {
            case FragmentId.First:
                GameProgress.FirstFragmentDone = true;
                break;
            case FragmentId.Second:
                GameProgress.SecondFragmentDone = true;
                break;
            case FragmentId.Third:
                GameProgress.ThirdFragmentDone = true;
                break;
        }

        Debug.Log("碎片 " + fragmentId + " 对话结束，标记完成。");

        // ③ 是否回到主场景
        if (goBackToMainWhenFinish && !string.IsNullOrEmpty(mainSceneName))
        {
            SceneManager.LoadScene(mainSceneName);
        }
        else
        {
            // 不回主场景就停在当前场景，看完整图
            Debug.Log("停留在当前场景，显示完整图片。");
        }
    }
}
