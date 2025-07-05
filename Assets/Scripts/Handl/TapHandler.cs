using UnityEngine;
using UnityEngine.Events;

public class TapHandler : MonoBehaviour
{
    public UnityEvent onTap;

    void Update()
    {
        if (PlayerInputReader.Instance.ClickStarted)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(PlayerInputReader.Instance.PointerPosition);
            if (Physics2D.OverlapPoint(pos)?.transform == this.transform)
            {
                onTap?.Invoke();
            }
        }
    }
}
