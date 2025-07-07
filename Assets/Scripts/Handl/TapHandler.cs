using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TapHandler : MonoBehaviour
{
    [Tooltip("タップ対象のオブジェクト")]
    public GameObject[] tapTargets;

    [Tooltip("各タップ時に呼びたいイベント")]
    public UnityEvent onEachTap;

    [Tooltip("全てタップが完了したときのイベント")]
    public UnityEvent onAllTapped;

    private HashSet<GameObject> tappedObjects = new HashSet<GameObject>();
    private bool isFinished = false;

    void Update()
    {
        if (isFinished || tapTargets.Length == 0) return;

        if (PlayerInputReader.Instance.ClickStarted)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(PlayerInputReader.Instance.PointerPosition);
            Collider2D hit = Physics2D.OverlapPoint(pos);

            if (hit != null)
            {
                foreach (GameObject target in tapTargets)
                {
                    if (hit.gameObject == target && !tappedObjects.Contains(target))
                    {
                        tappedObjects.Add(target);
                        Debug.Log($"Tapped: {target.name}");
                        onEachTap?.Invoke();
                        break;
                    }
                }

                if (tappedObjects.Count == tapTargets.Length)
                {
                    isFinished = true;
                    Debug.Log("全ての対象がタップ完了");
                    onAllTapped?.Invoke();
                }
            }
        }
    }
}
