using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : IStateCommand
{
    public override void Enter()
    {
        Debug.Log("GameOverState");
    }

    public override void Tick()
    {
        
    }

    public override void Exit()
    {
        
    }
}