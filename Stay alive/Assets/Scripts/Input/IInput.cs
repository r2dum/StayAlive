using System;

public interface IInput
{
    void Update();
    
    event Action<SwipeType> Swiped;
}
