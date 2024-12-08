using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningState : IStateCommand
{
    private RectTransform wheelTransform;
    [SerializeField] private float spinDuration = 2f;
    [SerializeField] private float maxSpinSpeed = 1000f;
    [SerializeField] private int numberOfSlices = 8;
    private bool isSpinning = false;

    public override void Enter()
    {
        Debug.Log("SpinningState");
        wheelTransform = StateController.Instance.WheelTransform;
        StartCoroutine(SpinCoroutine());
    }

    public override void Tick() 
    {

    }

    public override void Exit() 
    {

    }

    private IEnumerator SpinCoroutine()
    {
        isSpinning = true;
        float _elapsedTime = 0f;
        float _currentAngle = 0f;
        while (_elapsedTime < spinDuration)
        {
            float _speed = Mathf.Lerp(maxSpinSpeed, 0, _elapsedTime / spinDuration);
            _currentAngle += _speed * Time.deltaTime;
            wheelTransform.localRotation = Quaternion.Euler(0, 0, -_currentAngle);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        isSpinning = false;
        DetermineSlice();
    }

    private void DetermineSlice()
    {
        float _finalAngle = (wheelTransform.localRotation.eulerAngles.z + 360) % 360;
        float _offset = (360f / numberOfSlices) / 2f;
        int _sliceIndex = Mathf.FloorToInt(((_finalAngle + _offset) % 360) / (360f / numberOfSlices));

        Transform _sliceTransform = wheelTransform.GetChild(_sliceIndex);
        SliceBehavior _sliceBehavior = _sliceTransform.GetComponent<SliceBehavior>();
        SliceData _sliceData = _sliceBehavior.GetSliceData();

        if (_sliceData.Rarity == Rarity.Death)
        {
            Debug.Log("Death Slice! Game Over!");
            StateController.Instance.ChangeState<BombExplodedState>();
        }
        else
        {
            StateController.Instance.ChangeState<RewardClaimState>();
        }
    }
}
