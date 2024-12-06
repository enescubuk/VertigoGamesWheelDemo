using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardClaimState : IStateCommand
{
    private List<SliceData> rewardedSliceDataList = new List<SliceData>();
    [SerializeField] private RectTransform WheelTransform;

    public override void Enter()
    {
        Debug.Log("RewardClaimState");
        ClaimReward();
    }

    private void ClaimReward()
    {
        int sliceIndex = Mathf.FloorToInt((WheelTransform.localRotation.eulerAngles.z + 360) % 360 / (360f / WheelTransform.childCount));
        Transform sliceTransform = WheelTransform.GetChild(sliceIndex);
        SliceBehavior sliceBehavior = sliceTransform.GetComponent<SliceBehavior>();

        if (sliceBehavior != null)
        {
            SliceData sliceData = sliceBehavior.GetSliceData();
            rewardedSliceDataList.Add(sliceData);
            Debug.Log("Claimed Reward: " + sliceData.SliceName);
        }
        else
        {
            Debug.LogError("SliceBehavior not found on the selected slice!");
        }
    }

    public override void Tick()
    {
    }

    public override void Exit()
    {
    }
}
