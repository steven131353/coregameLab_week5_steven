using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Animator targetAnimator; // ✅ 指向要触发动画的 Image
    public string triggerName = "Play"; // ✅ 动画触发参数名

    private string[] dialogue = {
        "???:…Where is this place?",
        "You finally came.",
        "Let me tell you the truth."
    };

    private int index;
    private bool isActive;
    private bool readyToTrigger; // ✅ 新增标志：等待触发动画

    void Start()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = "";
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (readyToTrigger)
            {
                // ✅ 播放动画
                if (targetAnimator != null)
                {
                    targetAnimator.SetTrigger(triggerName);
                    Debug.Log("Triggered animation: " + triggerName);
                }
                readyToTrigger = false;
                return;
            }

            NextSentence();
        }
    }

    void NextSentence()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            StopAllCoroutines();
            StartCoroutine(TypeSentence());
        }
        else
        {
            Debug.Log("All dialogue finished.");
            // ✅ 对话全部结束后，等待下一次点击来触发动画
            readyToTrigger = true;
        }
    }
}
