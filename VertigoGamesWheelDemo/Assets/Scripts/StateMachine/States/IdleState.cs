using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class IdleState : IStateCommand
{
    private RectTransform wheelTransform;
    [SerializeField] private Button spinButton;
    [SerializeField] private GameObject slicePrefab;
    [SerializeField] private List<SliceData> sliceDataList;
    [SerializeField] private SliceData deathData; // DeathData'yı ayrı tanımladık.
    [SerializeField] private int numberOfSlices = 8;
    [SerializeField] private float radius = 235f;
    [SerializeField] private Button exitbutton;

    public override void Enter()
    {
        Debug.Log("IdleState");
        wheelTransform = StateController.Instance.WheelTransform;
        spinButton.onClick.AddListener(SpinButtonEvent);

        foreach (Transform child in wheelTransform)
        {
            Destroy(child.gameObject);
        }

        spinButton.interactable = true;
        exitbutton.interactable = true;

        exitbutton.onClick.AddListener(ExitToSpin);

        GenerateSlices();
    }

    private void ExitToSpin()
    {
        StateController.Instance.ChangeState<WaitingPurchaseState>();
        GetComponent<RewardClaimState>().DestroyRewards();
    }


    public override void Tick()
    {
        
    }

    public override void Exit()
    {
        spinButton.interactable = false;
        exitbutton.interactable = false;
    }

    private void GenerateSlices()
    {
        float _angleStep = 360f / numberOfSlices;
        int _deathDataIndex = Random.Range(0, numberOfSlices);

        List<SliceData> _shuffledDataList = new List<SliceData>(sliceDataList);

        Shuffle(_shuffledDataList);

        for (int i = 0; i < numberOfSlices; i++)
        {
            float angle = i * _angleStep * Mathf.Deg2Rad;
            Vector2 position = new Vector2(
                Mathf.Sin(angle) * radius,
                Mathf.Cos(angle) * radius
            );

            GameObject _slice = Instantiate(slicePrefab, wheelTransform);
            RectTransform _sliceRect = _slice.GetComponent<RectTransform>();
            _sliceRect.anchoredPosition = position;
            _sliceRect.localRotation = Quaternion.Euler(0, 0, -i * _angleStep);

            SliceData _data = (i == _deathDataIndex) ? deathData : _shuffledDataList[i % _shuffledDataList.Count];
            _slice.GetComponent<SliceBehavior>().Initialize(_data);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int _randomIndex = Random.Range(0, i + 1);
            T _temp = list[i];
            list[i] = list[_randomIndex];
            list[_randomIndex] = _temp;
        }
    }


    public void SpinButtonEvent()
    {
        StateController.Instance.ChangeState<SpinningState>();
    }

}
