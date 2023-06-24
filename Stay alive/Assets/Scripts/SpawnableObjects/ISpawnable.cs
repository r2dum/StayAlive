using System;

public interface ISpawnable : IPauseHandler
{
    event Action<ISpawnable> Disabled;
}
