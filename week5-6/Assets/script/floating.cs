using UnityEngine;

public class FloatingFragment : MonoBehaviour
{
    public float floatSpeed = 0.5f;   // 上下漂浮速度
    public float floatAmplitude = 20f; // 上下偏移幅度（像素）

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
