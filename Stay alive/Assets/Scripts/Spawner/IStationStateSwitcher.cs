public interface IStationStateSwitcher
{
    void SwitchState<T>() where T : BaseSpawnerState;

    void RandomSwitchState<T1, T2>() where T1 : BaseSpawnerState where T2 : BaseSpawnerState;
}
