using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplodedState : IStateCommand
{
    public override void Enter()
    {
        Debug.Log("BombExplodedState");
    }

    public override void Tick()
    {
        
    }

    public override void Exit()
    {
        
    }
}