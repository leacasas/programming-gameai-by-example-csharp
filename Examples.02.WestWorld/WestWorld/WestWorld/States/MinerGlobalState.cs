namespace WestWorld.States;

internal sealed class MinerGlobalState : State<Miner>
{
    private static readonly Lazy<MinerGlobalState> _instance = new(() => new MinerGlobalState());

    private MinerGlobalState() { }

    internal static MinerGlobalState Instance
    {
        get { return _instance.Value; }
    }

    /// <summary>
    /// Miner enters the saloon to quench his thirst.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Enter(Miner miner)
    {

    }

    /// <summary>
    /// If the miner is thirsty, buy a drink and then go back to dig.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Execute(Miner miner)
    {

    }

    /// <summary>
    /// Leave the saloon
    /// </summary>
    /// <param name="miner"></param>
    internal override void Exit(Miner miner)
    {

    }
}
