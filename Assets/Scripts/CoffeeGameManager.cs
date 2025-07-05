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
        public UnityEvent onStepStart;   // �X�e�b�v�J�n���̃C�x���g�i��FUI�\���j
        public UnityEvent onStepComplete; // �X�e�b�v������Ɏ��s�����C�x���g
    }

    public List<Step> steps = new List<Step>();
    private int currentStepIndex = -1;

    void Start()
    {
        AdvanceStep(); // �ŏ��̃X�e�b�v����J�n
    }

    public void AdvanceStep()
    {
        currentStepIndex++;

        if (currentStepIndex >= steps.Count)
        {
            Debug.Log("�S�X�e�b�v�����I");
            return;
        }

        Step current = steps[currentStepIndex];
        Debug.Log($"�X�e�b�v�J�n: {current.stepName}");

        current.targetObject.SetActive(true); // �ΏۃI�u�W�F�N�g���A�N�e�B�u��
        current.onStepStart?.Invoke();
    }

    public void CompleteCurrentStep()
    {
        Step current = steps[currentStepIndex];
        Debug.Log($"�X�e�b�v����: {current.stepName}");

        current.onStepComplete?.Invoke();
        current.targetObject.SetActive(false); // ���������I�u�W�F�N�g�͔�A�N�e�B�u��

        AdvanceStep(); // ���̃X�e�b�v��
    }
}
