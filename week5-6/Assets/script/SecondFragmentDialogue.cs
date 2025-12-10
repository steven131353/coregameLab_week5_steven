using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SecondFragmentDialogue : MonoBehaviour
{
    [Header("对话文字 UI")]
    public TextMeshProUGUI dialogueText;

    [Header("每一句对话内容")]
    [TextArea(2, 5)]
    public string[] lines;        // 在 Inspector 里填每一句话

    [Header("打字机速度（秒/字）")]
    public float typeSpeed = 0.03f;

    [Header("下一句按钮（可选，只是为了在代码里开关用）")]
    public GameObject nextButton;

    [Header("第二个碎片相关图片")]
    public GameObject carFragmentImage;   // 破碎的车
    public GameObject carFullImage;       // 完整的车（对话结束时显示）

    [Header("对话结束后是否回到主场景")]
    public bool goBackToMainWhenFinish = true;
    public string mainSceneName = "Main";

    private int index = 0;        // 当前是第几句
    private bool isTyping = false; // 是否正在打字中

    private void Start()
    {
        // 一开始显示碎片，隐藏完整车
        if (carFragmentImage != null) carFragmentImage.SetActive(true);
        if (carFullImage != null) carFullImage.SetActive(false);

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
            Debug.LogWarning("SecondFragmentDialogue：没有填任何对话内容（lines 为空）");
            return;
        }

        if (index < 0 || index >= lines.Length)
        {
            Debug.LogWarning("SecondFragmentDialogue：index 越界：" + index);
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
    /// 下一句按钮点击事件 —— 把这个函数绑到 Button 的 OnClick 上
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
    /// 对话结束时的逻辑：
    /// 1. 碎片车 → 完整车
    /// 2. 记录第二个碎片已完成
    /// 3. 选择是否回到主场景
    /// </summary>
    private void EndDialogue()
    {
        // ① 碎片 → 完整车
        if (carFragmentImage != null) carFragmentImage.SetActive(false);
        if (carFullImage != null) carFullImage.SetActive(true);

        // ② 记录第二个碎片已完成（用于回到 Main 后替换那块碎片）
        GameProgress.SecondFragmentDone = true;

        Debug.Log("第二个碎片对话结束，标记 SecondFragmentDone = true");

        // ③ 是否回到主场景
        if (goBackToMainWhenFinish && !string.IsNullOrEmpty(mainSceneName))
        {
            SceneManager.LoadScene(mainSceneName);
        }
        else
        {
            // 如果你不想立刻回主场景，可以在这里再弹一个“返回”按钮之类的
            Debug.Log("停留在当前场景，显示完整车辆");
        }
    }
}
