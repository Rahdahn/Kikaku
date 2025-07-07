using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMove : MonoBehaviour
{
    public Camera mainCamera;
    public float moveDistance = 10f;
    public float moveDuration = 1f;
    public List<Transform> moveWithCameraObjects; // 一緒に移動させるオブジェクト

    public UnityEvent onMoveComplete;

    private void Start()
    {
        StartCoroutine(MoveCameraRoutine());
    }

    private IEnumerator MoveCameraRoutine()
    {
        Vector3 startCamPos = mainCamera.transform.position;
        Vector3 endCamPos = startCamPos + new Vector3(moveDistance, 0, 0);

        List<Vector3> startObjPos = new List<Vector3>();
        foreach (var obj in moveWithCameraObjects)
            startObjPos.Add(obj.position);

        float elapsed = 0;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            mainCamera.transform.position = Vector3.Lerp(startCamPos, endCamPos, t);

            for (int i = 0; i < moveWithCameraObjects.Count; i++)
            {
                if (moveWithCameraObjects[i] != null)
                {
                    moveWithCameraObjects[i].position = Vector3.Lerp(startObjPos[i], startObjPos[i] + new Vector3(moveDistance, 0, 0), t);
                }
            }

            yield return null;
        }

        onMoveComplete?.Invoke();
    }
}
