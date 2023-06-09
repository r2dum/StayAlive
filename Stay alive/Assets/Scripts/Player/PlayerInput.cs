﻿using UnityEngine;

public class PlayerInput : MonoBehaviour, IPauseHandler
{
    private IMovable _movable;
    private IInput _input;

    private bool _isPaused;

    public void Initialize(IMovable movable, IInput input)
    {
        _movable = movable;
        _input = input;
        _input.Swiped += OnSwiped;
    }

    private void Update()
    {
        if (_isPaused)
            return;
        
        _input.Update();
    }

    private void OnSwiped(SwipeType swipeType)
    {
        if (swipeType == SwipeType.Up)
            _movable.Move(new Vector3(0, 0, 1.65f));
        
        if (swipeType == SwipeType.Down)
            _movable.Move(new Vector3(0, 0, -1.65f));
        
        if (swipeType == SwipeType.Left)
            _movable.Move(new Vector3(-1.65f, 0, 0));
        
        if (swipeType == SwipeType.Right)
            _movable.Move(new Vector3(1.65f, 0, 0));
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
