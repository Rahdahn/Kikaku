using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeGameManager : MonoBehaviour
{
    [System.Serializable]
    public class Step
    {
        public string stepName;
        public GameObject targetObject;
        public UnityEvent onStepStart;   // ステップ開始時のイベント（例：UI表示）
        public UnityEvent onStepComplete; // ステップ完了後に実行されるイベント
    }

    public List<Step> steps = new List<Step>();
    private int currentStepIndex = -1;

    void Start()
    {
        AdvanceStep(); // 最初のステップから開始
    }

    public void AdvanceStep()
    {
        currentStepIndex++;

        if (currentStepIndex >= steps.Count)
        {
            Debug.Log("全ステップ完了！");
            return;
        }

        Step current = steps[currentStepIndex];
        Debug.Log($"ステップ開始: {current.stepName}");

        current.targetObject.SetActive(true); // 対象オブジェクトをアクティブに
        current.onStepStart?.Invoke();
    }

    public void CompleteCurrentStep()
    {
        Step current = steps[currentStepIndex];
        Debug.Log($"ステップ完了: {current.stepName}");

        current.onStepComplete?.Invoke();
        current.targetObject.SetActive(false); // 完了したオブジェクトは非アクティブに

        AdvanceStep(); // 次のステップへ
    }
}
