using UnityEngine;
using UnityEngine.Events;

public class SwipeHandler : MonoBehaviour
{
    public enum SwipeDirection { Up, Down, Left, Right }
    public SwipeDirection requiredDirection;
    public float minSwipeDistance = 100f;
    public UnityEvent onSwipeDetected;

    private Vector2 startPos;
    private bool tracking = false;

    void Update()
    {
        if (PlayerInputReader.Instance.ClickStarted)
        {
            startPos = PlayerInputReader.Instance.PointerPosition;
            tracking = true;
        }

        if (PlayerInputReader.Instance.ClickReleased && tracking)
        {
            tracking = false;
            Vector2 endPos = PlayerInputReader.Instance.PointerPosition;
            Vector2 swipe = endPos - startPos;

            if (swipe.magnitude > minSwipeDistance)
            {
                SwipeDirection dir = DetectDirection(swipe);
                if (dir == requiredDirection)
                    onSwipeDetected?.Invoke();
            }
        }
    }

    SwipeDirection DetectDirection(Vector2 swipe)
    {
        return Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y)
            ? (swipe.x > 0 ? SwipeDirection.Right : SwipeDirection.Left)
            : (swipe.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
    }
}
