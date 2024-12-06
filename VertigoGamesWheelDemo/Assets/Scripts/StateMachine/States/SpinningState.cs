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
        float elapsedTime = 0f;
        float currentAngle = 0f;

        while (elapsedTime < spinDuration)
        {
            float speed = Mathf.Lerp(maxSpinSpeed, 0, elapsedTime / spinDuration);
            currentAngle += speed * Time.deltaTime;
            wheelTransform.localRotation = Quaternion.Euler(0, 0, -currentAngle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isSpinning = false;
        DetermineSlice();
    }

    private void DetermineSlice()
    {
        float finalAngle = (wheelTransform.localRotation.eulerAngles.z + 360) % 360;
        float offset = (360f / numberOfSlices) / 2f;
        int sliceIndex = Mathf.FloorToInt(((finalAngle + offset) % 360) / (360f / numberOfSlices));

        Transform sliceTransform = wheelTransform.GetChild(sliceIndex);
        SliceBehavior sliceBehavior = sliceTransform.GetComponent<SliceBehavior>();
        SliceData sliceData = sliceBehavior.GetSliceData();

        if (sliceData.Rarity == Rarity.Death)
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
