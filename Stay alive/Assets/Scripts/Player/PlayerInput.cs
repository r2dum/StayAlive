using UnityEngine;

public class PlayerInput : ICleanUp
{
    private readonly IMovable _movable;
    private readonly IInput _input;
    
    public PlayerInput(IMovable movable, IInput input)
    {
        _movable = movable;
        _input = input;
        _input.Swiped += OnSwiped;
    }
    
    public void CleanUp()
    {
        _input.Swiped -= OnSwiped;
    }
    
    public void Update()
    {
        _input.Update();
    }

    private void OnSwiped(SwipeType swipeType)
    {
        if (swipeType == SwipeType.Up)
            _movable.Move(new Vector3(0, 0, 1.65f), Direction.North);
        
        if (swipeType == SwipeType.Down)
            _movable.Move(new Vector3(0, 0, -1.65f), Direction.South);
        
        if (swipeType == SwipeType.Left)
            _movable.Move(new Vector3(-1.65f, 0, 0), Direction.East);
        
        if (swipeType == SwipeType.Right)
            _movable.Move(new Vector3(1.65f, 0, 0), Direction.West);
    }
}
