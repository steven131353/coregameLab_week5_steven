using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FragmentGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outline;
    private Vector3 baseScale;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
        baseScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = baseScale * 1.05f;
        if (outline != null) outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = baseScale;
        if (outline != null) outline.enabled = false;
    }
}
