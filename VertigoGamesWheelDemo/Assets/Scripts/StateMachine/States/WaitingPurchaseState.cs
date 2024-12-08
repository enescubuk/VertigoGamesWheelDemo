using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingPurchaseState : IStateCommand
{
    [SerializeField] private GameObject purchaseButton;
    public override void Enter()
    {
        Debug.Log("WaitingPurchaseState");
        purchaseButton.SetActive(true);
        purchaseButton.GetComponent<Button>().onClick.AddListener(PurhcaseButtonEvent);
    }

    public override void Tick()
    {
        
    }

    private void PurhcaseButtonEvent()
    {
        purchaseButton.SetActive(false);
        GetComponent<StateController>().ChangeState<IdleState>();
    }

    public override void Exit()
    {
        
    }
}