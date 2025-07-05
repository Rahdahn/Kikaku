using UnityEngine;
using UnityEngine.Events;

public class DragHandler : MonoBehaviour
{
    public UnityEvent onDragStart;
    public UnityEvent onDragEnd;

    private bool dragging = false;

    void Update()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(PlayerInputReader.Instance.PointerPosition);

        if (PlayerInputReader.Instance.ClickStarted)
        {
            Collider2D hit = Physics2D.OverlapPoint(worldPos);
            if (hit?.transform == this.transform)
            {
                dragging = true;
                onDragStart?.Invoke();
            }
        }

        if (PlayerInputReader.Instance.ClickHeld && dragging)
        {
            transform.position = worldPos;
        }

        if (PlayerInputReader.Instance.ClickReleased && dragging)
        {
            dragging = false;
            onDragEnd?.Invoke();
        }
    }
}
