namespace WestWorld.States;

internal sealed class MinersWifeGlobalState : State<MinersWife>
{
    private static readonly Lazy<MinersWifeGlobalState> _instance = new(() => new MinersWifeGlobalState());

    private MinersWifeGlobalState() { }

    internal static MinersWifeGlobalState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(MinersWife wife)
    {

    }

    internal override void Execute(MinersWife wife)
    {
        if (Random.Shared.Next(0, 10) <= 1)
            wife.ChangeState(VisitBathroomState.Instance);
    }

    internal override void Exit(MinersWife wife)
    {

    }
}
