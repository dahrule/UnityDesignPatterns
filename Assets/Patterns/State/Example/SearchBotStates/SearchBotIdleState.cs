﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SearchBotIdleState : IState
{
    SearchBotSM _searchBotSM;
    TargetAssigner _targetAssigner;

    Material _eyeMat;
    Color _startingEyeColor;
    Color _idleEyeColor = Color.black;

    public SearchBotIdleState(SearchBotSM searchBotSM, Material eyeMat, TargetAssigner targetAssigner)
    {
        _searchBotSM = searchBotSM;
        _eyeMat = eyeMat;
        _targetAssigner = targetAssigner;
    }

    // Observer pattern works really well with the state pattern.
    // we can start listening for events in Enter, and stop listening in Exit
    // This prevents us from having to poll values in our Tick/Update
    public void Enter()
    {
        Debug.Log("STATE CHANGE - Idle");
        // store starting eye color so we can return to it
        _startingEyeColor = _eyeMat.color;
        // show state visual
        _eyeMat.color = _idleEyeColor;

        // start listening for new target mouse click
        _targetAssigner.NewTargetAcquired += OnNewTargetAcquired;
    }

    public void Exit()
    {
        // return eye visual
        _eyeMat.color = _startingEyeColor;
        // stop listening for new target mouse click
        _targetAssigner.NewTargetAcquired -= OnNewTargetAcquired;
    }

    public void FixedTick()
    {
        //
    }

    public void Tick()
    {
        //
    }

    void OnNewTargetAcquired(Vector3 newPosition)
    {
        _searchBotSM.ChangeState(_searchBotSM.SearchState);
    }
}
