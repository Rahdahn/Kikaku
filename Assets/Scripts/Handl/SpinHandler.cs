using UnityEngine;
using UnityEngine.Events;

public class SpinHandler : MonoBehaviour
{
    public Transform center;
    public float requiredTotalAngle = 360f;
    public UnityEvent onSpinComplete;

    private float totalRotation = 0f;
    private Vector2 prevPos;
    private bool spinning = false;

    void Update()
    {
        Vector2 current = Camera.main.ScreenToWorldPoint(PlayerInputReader.Instance.PointerPosition);

        if (PlayerInputReader.Instance.ClickStarted)
        {
            spinning = true;
            prevPos = current;
            totalRotation = 0f;
        }

        if (PlayerInputReader.Instance.ClickHeld && spinning)
        {
            float angle = Vector2.SignedAngle(prevPos - (Vector2)center.position, current - (Vector2)center.position);
            totalRotation += Mathf.Abs(angle);
            prevPos = current;

            if (totalRotation >= requiredTotalAngle)
            {
                spinning = false;
                onSpinComplete?.Invoke();
            }
        }

        if (PlayerInputReader.Instance.ClickReleased)
        {
            spinning = false;
        }
    }
}
