using System;
using UnityEngine;

public class MobileInput : IInput
{
    private Vector2 _startTouch;
    private Vector2 _swipeDelta;
    private bool _isDragging;
    
    public event Action<SwipeType> Swiped;

    public void Update()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _isDragging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || 
                     Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
        }
        
        _swipeDelta = Vector2.zero;
        if (_isDragging)
        {
            if (Input.touches.Length > 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
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
