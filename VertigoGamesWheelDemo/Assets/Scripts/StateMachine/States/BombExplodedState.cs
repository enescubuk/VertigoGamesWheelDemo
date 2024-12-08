using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BombExplodedState : IStateCommand
{
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private RectTransform starFlashTransform;

    [SerializeField] private Button reviveWithAdButton;
    [SerializeField] private Button reviveWithGoldButton;
    [SerializeField] private Button giveUpButton;
    public override void Enter()
    {
        Debug.Log("BombExplodedState");
        GetComponent<RewardClaimState>().DestroyRewards();
        SetUI();
        reviveWithAdButton.onClick.AddListener(ReviveWithAdButton);
        reviveWithGoldButton.onClick.AddListener(ReviveWithGoldButton);
        giveUpButton.onClick.AddListener(GiveUpButton);
        setInteractableButtons(true);
    }

    private void setInteractableButtons(bool _value)
    {
        reviveWithAdButton.interactable = _value;
        reviveWithGoldButton.interactable = _value;
        giveUpButton.interactable = _value;
    }

    private void SetUI()
    {
        exitButton.SetActive(false);
        deathPanel.SetActive(true);
        starFlashTransform.DOScale(Vector3.one * 1.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetId("StarFlash");
        starFlashTransform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetId("StarFlash");
    }

    public override void Tick()
    {
        
    }

    public override void Exit()
    {
        DOTween.Kill("StarFlash");
        deathPanel.SetActive(false);
    }

    public void GiveUpButton()
    {
        GetComponent<StateController>().ChangeState<WaitingPurchaseState>();
    }

    public void ReviveWithAdButton()
    {
        GetComponent<StateController>().ChangeState<IdleState>();
    }

    public void ReviveWithGoldButton()
    {
        GetComponent<StateController>().ChangeState<IdleState>();
    }
}