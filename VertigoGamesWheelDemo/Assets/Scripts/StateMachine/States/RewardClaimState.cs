using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardClaimState : IStateCommand
{
    private List<SliceData> rewardedSliceDataList = new List<SliceData>();
    private RectTransform wheelTransform;
    [SerializeField] private Transform rewardPanel;
    [SerializeField] private GameObject rewardPrefab;
    public override void Enter()
    {
        Debug.Log("RewardClaimState");
        rewardPanel.gameObject.SetActive(true);
        wheelTransform = StateController.Instance.WheelTransform;
        ClaimReward();
    }

    public List<SliceData> GetRewardedSliceDataList()
    {
        return rewardedSliceDataList;
    }

    private void ClaimReward()
    {
        int _sliceIndex = Mathf.FloorToInt((wheelTransform.localRotation.eulerAngles.z + 360) % 360 / (360f / wheelTransform.childCount));
        Transform _sliceTransform = wheelTransform.GetChild(_sliceIndex + 1);
        SliceBehavior _sliceBehavior = _sliceTransform.GetComponent<SliceBehavior>();
        if (_sliceBehavior != null)
        {
            SliceData _sliceData = _sliceBehavior.GetSliceData();
            RewardItemBehavior _existingReward = null;
            foreach (Transform child in rewardPanel)
            {
                RewardItemBehavior _rewardItem = child.GetComponent<RewardItemBehavior>();
                if (_rewardItem != null && _rewardItem.GetSliceData() == _sliceData)
                {
                    _existingReward = _rewardItem;
                    break;
                }
            }

            if (_existingReward != null)
            {
                // Mevcut ödülün counter'ını artır
                int _currentCount = _existingReward.GetCounter();
                _existingReward.ChangeCounter(_currentCount + 1);
            }
            else
            {
                // Yeni ödül oluştur
                GameObject _newReward = Instantiate(rewardPrefab, rewardPanel);
                RewardItemBehavior _rewardItemBehavior = _newReward.GetComponent<RewardItemBehavior>();
                _rewardItemBehavior.Initialize(_sliceData);
                _rewardItemBehavior.ChangeCounter(1); // İlk kez kazanıldığı için 1 olarak ayarla
            }
            Debug.Log("Claimed Reward: " + _sliceData.SliceName);
        }
        else
        {
            Debug.LogError("SliceBehavior not found on the selected slice!");
        }
        Invoke("Exit", 0.5f);
    }

    public void DestroyRewards()
    {
        Debug.Log("Clearing Rewards");
        foreach (Transform child in rewardPanel)
        {
            Destroy(child.gameObject);
        }
        rewardedSliceDataList.Clear();
    }


    public override void Tick()
    {
    }

    public override void Exit()
    {
        StateController.Instance.ChangeState<IdleState>();
    }
}
