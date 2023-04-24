using System;
using UnityEngine;

public class MouseInput : IInput
{
    private Vector2 _startTouch;
    private Vector2 _swipeDelta;
    private bool _isDragging;
    
    public event Action<SwipeType> Swiped;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            Reset();
        }
        
        _swipeDelta = Vector2.zero;
        if (_isDragging)
        { 
            if (Input.GetMouseButton(0)) 
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
        }
        
        if (_swipeDelta.magnitude > 30)
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                Swiped?.Invoke(_swipeDelta.x > 0 ? SwipeType.Right : SwipeType.Left);
            else
                Swiped?.Invoke(_swipeDelta.y > 0 ? SwipeType.Up : SwipeType.Down);

            Reset();
        }
    }

    private void Reset()
    {
        _startTouch = Vector2.zero;
        _swipeDelta = Vector2.zero;
        _isDragging = false;
    }
}
